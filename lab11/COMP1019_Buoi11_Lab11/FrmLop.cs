using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace COMP1019_Buoi11_Lab11;

/// <summary>
/// Quản lý lớp học bằng ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter) với SQL Server.
/// Ở buổi 11 toàn bộ xử lý nằm trực tiếp trong Form để thấy rõ luồng:
/// Button Click -> kiểm tra dữ liệu -> câu SQL có tham số -> nạp lại DataGridView.
/// Sang các buổi sau sẽ tách thành các lớp DAL/BLL.
/// </summary>
public partial class FrmLop : Form
{
    // Mã lớp lưu bằng VARCHAR nên chỉ nhận chữ không dấu, số, gạch dưới, gạch ngang.
    private static readonly Regex MaLopRegex = new Regex(@"^[A-Za-z0-9_\-]+$", RegexOptions.Compiled);

    private bool schemaDetected;     // đã kiểm tra cấu trúc bảng LOP chưa
    private bool hasKhoaColumn;      // bảng LOP có cột MaKhoa không (database của buổi 10 thì có)
    private bool isLoadingGrid;      // true khi đang nạp lại lưới, để SelectionChanged không chạy
    private string? selectedMaLop;   // mã lớp đang chọn (null = chưa chọn lớp nào)

    public FrmLop()
    {
        InitializeComponent();
    }

    // =====================================================================
    // F1 - LOAD FORM
    // =====================================================================
    private void FrmLop_Load(object? sender, EventArgs e)
    {
        ConfigureGrid();
        if (LoadData(null))
            SetStatus("Đã tải danh sách lớp từ SQL Server");
    }

    private void ConfigureGrid()
    {
        dgvLop.AutoGenerateColumns = false;
        dgvLop.ReadOnly = true;
        dgvLop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLop.MultiSelect = false;
        dgvLop.AllowUserToAddRows = false;
        dgvLop.AllowUserToDeleteRows = false;
        dgvLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        dgvLop.Columns.Clear();
        dgvLop.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colMaLop", DataPropertyName = "MaLop", HeaderText = "Mã lớp", FillWeight = 20 });
        dgvLop.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colTenLop", DataPropertyName = "TenLop", HeaderText = "Tên lớp", FillWeight = 45 });
        dgvLop.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colMaKhoa", DataPropertyName = "MaKhoa", HeaderText = "Mã khoa", FillWeight = 15, Visible = false });
        var soSv = new DataGridViewTextBoxColumn
            { Name = "colSoSinhVien", DataPropertyName = "SoSinhVien", HeaderText = "Số sinh viên", FillWeight = 20 };
        soSv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvLop.Columns.Add(soSv);
    }

    // =====================================================================
    // TRUY CẬP DỮ LIỆU (tất cả câu SQL nhận dữ liệu người dùng đều dùng SqlParameter)
    // =====================================================================
    private static SqlConnection CreateConnection() => new SqlConnection(DbConfig.ConnectionString);

    /// <summary>
    /// Kiểm tra bảng LOP có cột MaKhoa không. Database tối thiểu của đề Lab 11 không có,
    /// còn database đầy đủ của buổi 10 thì có (MaKhoa bắt buộc) nên khi đó hiện thêm ô chọn Khoa.
    /// </summary>
    private void DetectSchema()
    {
        using SqlConnection conn = CreateConnection();
        conn.Open();

        const string checkSql =
            "SELECT COUNT(*) FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.LOP') AND name = N'MaKhoa'";
        using (SqlCommand cmd = new SqlCommand(checkSql, conn))
        {
            hasKhoaColumn = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        if (hasKhoaColumn)
        {
            using SqlCommand cmdKhoa = new SqlCommand("SELECT MaKhoa, TenKhoa FROM KHOA ORDER BY TenKhoa", conn);
            using SqlDataAdapter adapter = new SqlDataAdapter(cmdKhoa);
            DataTable khoaTable = new DataTable();
            adapter.Fill(khoaTable);

            cboKhoa.DisplayMember = "TenKhoa";
            cboKhoa.ValueMember = "MaKhoa";
            cboKhoa.DataSource = khoaTable;
            cboKhoa.SelectedIndex = -1;
        }

        lblKhoa.Visible = hasKhoaColumn;
        cboKhoa.Visible = hasKhoaColumn;
        dgvLop.Columns["colMaKhoa"]!.Visible = hasKhoaColumn;
        schemaDetected = true;
    }

    /// <summary>Lấy danh sách lớp kèm số sinh viên. keyword = null thì lấy tất cả.</summary>
    private DataTable GetLopTable(string? keyword)
    {
        // Chỉ ghép tên cột cố định (không phải dữ liệu người dùng) nên không có nguy cơ SQL injection.
        string khoaColumn = hasKhoaColumn ? "l.MaKhoa" : "CAST(NULL AS VARCHAR(20))";
        string sql = $@"SELECT l.MaLop, l.TenLop, {khoaColumn} AS MaKhoa,
                               (SELECT COUNT(*) FROM SINHVIEN sv WHERE sv.MaLop = l.MaLop) AS SoSinhVien
                        FROM LOP l
                        WHERE @kw IS NULL OR l.MaLop LIKE @kw ESCAPE '\' OR l.TenLop LIKE @kw ESCAPE '\'
                        ORDER BY l.MaLop";

        using SqlConnection conn = CreateConnection();
        using SqlCommand cmd = new SqlCommand(sql, conn);
        SqlParameter kw = cmd.Parameters.Add("@kw", SqlDbType.NVarChar, 250);
        kw.Value = keyword == null ? DBNull.Value : "%" + EscapeLike(keyword) + "%";

        using SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable table = new DataTable();
        adapter.Fill(table);   // Fill tự mở và đóng kết nối
        return table;
    }

    /// <summary>Thoát các ký tự đặc biệt của LIKE (% _ [) để người dùng tìm được đúng ký tự đó.</summary>
    private static string EscapeLike(string text) =>
        text.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[");

    private bool IsMaLopExists(string maLop)
    {
        using SqlConnection conn = CreateConnection();
        using SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM LOP WHERE MaLop = @ma", conn);
        cmd.Parameters.Add("@ma", SqlDbType.VarChar, 20).Value = maLop;

        conn.Open();
        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }

    private int CountSinhVienByLop(string maLop)
    {
        using SqlConnection conn = CreateConnection();
        using SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM SINHVIEN WHERE MaLop = @ma", conn);
        cmd.Parameters.Add("@ma", SqlDbType.VarChar, 20).Value = maLop;

        conn.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    private int InsertLop(string maLop, string tenLop, string? maKhoa)
    {
        string sql = hasKhoaColumn
            ? "INSERT INTO LOP (MaLop, TenLop, MaKhoa) VALUES (@ma, @ten, @khoa)"
            : "INSERT INTO LOP (MaLop, TenLop) VALUES (@ma, @ten)";

        using SqlConnection conn = CreateConnection();
        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@ma", SqlDbType.VarChar, 20).Value = maLop;
        cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 100).Value = tenLop;
        if (hasKhoaColumn)
            cmd.Parameters.Add("@khoa", SqlDbType.VarChar, 20).Value = (object?)maKhoa ?? DBNull.Value;

        conn.Open();
        return cmd.ExecuteNonQuery();
    }

    private int UpdateLop(string maLop, string tenLop, string? maKhoa)
    {
        string sql = hasKhoaColumn
            ? "UPDATE LOP SET TenLop = @ten, MaKhoa = @khoa WHERE MaLop = @ma"
            : "UPDATE LOP SET TenLop = @ten WHERE MaLop = @ma";

        using SqlConnection conn = CreateConnection();
        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@ma", SqlDbType.VarChar, 20).Value = maLop;
        cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 100).Value = tenLop;
        if (hasKhoaColumn)
            cmd.Parameters.Add("@khoa", SqlDbType.VarChar, 20).Value = (object?)maKhoa ?? DBNull.Value;

        conn.Open();
        return cmd.ExecuteNonQuery();
    }

    private int DeleteLop(string maLop)
    {
        using SqlConnection conn = CreateConnection();
        using SqlCommand cmd = new SqlCommand("DELETE FROM LOP WHERE MaLop = @ma", conn);
        cmd.Parameters.Add("@ma", SqlDbType.VarChar, 20).Value = maLop;

        conn.Open();
        return cmd.ExecuteNonQuery();
    }

    // =====================================================================
    // HÀM HỖ TRỢ GIAO DIỆN
    // =====================================================================
    /// <summary>Nạp lại DataGridView. Trả về false nếu lỗi (đã báo lỗi cho người dùng).</summary>
    private bool LoadData(string? keyword)
    {
        try
        {
            if (!schemaDetected)
                DetectSchema();

            DataTable table = GetLopTable(keyword);

            isLoadingGrid = true;
            try
            {
                dgvLop.DataSource = table;
                dgvLop.ClearSelection();   // mặc định DataGridView tự chọn dòng đầu, bỏ chọn đi
            }
            finally
            {
                isLoadingGrid = false;
            }

            lblCount.Text = $"Số lớp hiển thị: {table.Rows.Count}";
            return true;
        }
        catch (SqlException ex)
        {
            ShowDbError("tải dữ liệu", ex);
            SetStatus("Không tải được dữ liệu từ SQL Server");
            return false;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    private bool ValidateInput()
    {
        string ma = txtMaLop.Text.Trim();

        if (ma.Length == 0)
        {
            Warn("Vui lòng nhập mã lớp.");
            txtMaLop.Focus();
            return false;
        }

        // Khi sửa, mã lớp bị khóa (lấy từ dòng đã chọn) nên chỉ kiểm tra định dạng khi nhập mới.
        if (!txtMaLop.ReadOnly && !MaLopRegex.IsMatch(ma))
        {
            Warn("Mã lớp chỉ được gồm chữ không dấu, số, dấu gạch dưới hoặc gạch ngang (không có khoảng trắng).");
            txtMaLop.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtTenLop.Text))
        {
            Warn("Vui lòng nhập tên lớp.");
            txtTenLop.Focus();
            return false;
        }

        if (hasKhoaColumn && cboKhoa.SelectedValue == null)
        {
            Warn("Vui lòng chọn khoa của lớp.");
            cboKhoa.Focus();
            return false;
        }

        return true;
    }

    /// <summary>Xóa nội dung nhập, mở khóa mã lớp, bỏ chọn dòng.</summary>
    private void ClearInput()
    {
        txtMaLop.Clear();
        txtMaLop.ReadOnly = false;
        txtTenLop.Clear();
        cboKhoa.SelectedIndex = -1;
        selectedMaLop = null;
        dgvLop.ClearSelection();
        txtMaLop.Focus();
    }

    /// <summary>Sau khi thêm/sửa/xóa: xóa ô nhập và ô tìm kiếm rồi tải lại toàn bộ danh sách.</summary>
    private void ResetAndReload()
    {
        txtTimKiem.Clear();
        ClearInput();
        LoadData(null);
    }

    private void SetStatus(string message) => lblStatus.Text = message;

    private static void Warn(string message) =>
        MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

    /// <summary>Hiện lỗi SQL kèm gợi ý cách khắc phục cho các lỗi kết nối thường gặp.</summary>
    private static void ShowDbError(string action, SqlException ex)
    {
        string hint = ex.Number switch
        {
            -1 or 2 or 53 or 233 =>
                "\n\nKhông kết nối được SQL Server. Kiểm tra: dịch vụ SQL Server đang chạy, " +
                "tên server trong chuỗi kết nối (file DbConfig.cs hoặc biến môi trường EDUMANAGER_CONNSTR).",
            4060 or 18456 =>
                "\n\nKhông đăng nhập được database EduManagerDB. Kiểm tra database đã được tạo (chạy EduManagerDB.sql) " +
                "và tài khoản có quyền truy cập.",
            208 =>
                "\n\nChưa có bảng LOP hoặc SINHVIEN. Hãy chạy file script EduManagerDB.sql trước.",
            _ => string.Empty
        };

        MessageBox.Show($"Lỗi khi {action}: {ex.Message}{hint}", "Lỗi cơ sở dữ liệu",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    // =====================================================================
    // F2 - CHỌN DÒNG TRONG DATAGRIDVIEW -> ĐƯA LÊN Ô NHẬP
    // =====================================================================
    private void dgvLop_SelectionChanged(object? sender, EventArgs e)
    {
        if (isLoadingGrid || dgvLop.SelectedRows.Count == 0) return;
        if (dgvLop.SelectedRows[0].DataBoundItem is not DataRowView row) return;

        selectedMaLop = Convert.ToString(row["MaLop"]);
        txtMaLop.Text = selectedMaLop;
        txtMaLop.ReadOnly = true;                    // mã lớp là khóa chính nên không cho sửa
        txtTenLop.Text = Convert.ToString(row["TenLop"]);

        if (hasKhoaColumn)
        {
            object khoa = row["MaKhoa"];
            cboKhoa.SelectedValue = khoa == DBNull.Value ? (object)string.Empty : khoa;
        }

        SetStatus($"Đang chọn lớp {selectedMaLop}. Sửa tên lớp rồi nhấn Sửa, hoặc nhấn Xóa.");
    }

    // =====================================================================
    // F3 - THÊM
    // =====================================================================
    private void btnThem_Click(object? sender, EventArgs e)
    {
        if (txtMaLop.ReadOnly)
        {
            Warn("Bạn đang chọn một lớp có sẵn. Hãy nhấn Làm mới trước khi thêm lớp mới.");
            return;
        }

        if (!ValidateInput()) return;

        string ma = txtMaLop.Text.Trim();
        string ten = txtTenLop.Text.Trim();
        string? khoa = cboKhoa.SelectedValue?.ToString();

        try
        {
            if (IsMaLopExists(ma))
            {
                Warn($"Mã lớp \"{ma}\" đã tồn tại. Vui lòng nhập mã khác.");
                txtMaLop.Focus();
                txtMaLop.SelectAll();
                return;
            }

            if (InsertLop(ma, ten, khoa) > 0)
            {
                ResetAndReload();
                SetStatus($"Đã thêm lớp {ma}");
            }
        }
        catch (SqlException ex) when (ex.Number is 2627 or 2601)
        {
            // Phòng trường hợp hai người cùng thêm một mã vào cùng lúc.
            Warn($"Mã lớp \"{ma}\" đã tồn tại.");
        }
        catch (SqlException ex)
        {
            ShowDbError("thêm lớp", ex);
        }
    }

    // =====================================================================
    // F4 - SỬA (chỉ sửa tên lớp, và khoa nếu có; không sửa mã lớp)
    // =====================================================================
    private void btnSua_Click(object? sender, EventArgs e)
    {
        if (selectedMaLop == null)
        {
            Warn("Vui lòng chọn lớp cần sửa trong danh sách.");
            return;
        }

        if (!ValidateInput()) return;

        string ten = txtTenLop.Text.Trim();
        string? khoa = cboKhoa.SelectedValue?.ToString();

        try
        {
            if (UpdateLop(selectedMaLop, ten, khoa) > 0)
            {
                string ma = selectedMaLop;
                ResetAndReload();
                SetStatus($"Đã cập nhật lớp {ma}");
            }
            else
            {
                Warn("Không tìm thấy lớp cần sửa (có thể đã bị xóa). Danh sách sẽ được tải lại.");
                ResetAndReload();
            }
        }
        catch (SqlException ex)
        {
            ShowDbError("cập nhật lớp", ex);
        }
    }

    // =====================================================================
    // F5 - XÓA (hỏi xác nhận, không cho xóa lớp đang có sinh viên)
    // =====================================================================
    private void btnXoa_Click(object? sender, EventArgs e)
    {
        if (selectedMaLop == null)
        {
            Warn("Vui lòng chọn lớp cần xóa trong danh sách.");
            return;
        }

        string ma = selectedMaLop;

        try
        {
            int soSinhVien = CountSinhVienByLop(ma);
            if (soSinhVien > 0)
            {
                MessageBox.Show(
                    $"Không thể xóa lớp \"{ma}\" vì lớp đang có {soSinhVien} sinh viên.",
                    "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa lớp \"{ma}\" - {txtTenLop.Text.Trim()}?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            if (DeleteLop(ma) > 0)
            {
                ResetAndReload();
                SetStatus($"Đã xóa lớp {ma}");
            }
            else
            {
                Warn("Không tìm thấy lớp cần xóa (có thể đã bị xóa). Danh sách sẽ được tải lại.");
                ResetAndReload();
            }
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            // Lỗi khóa ngoại: phòng trường hợp có sinh viên được thêm vào lớp ngay sau lúc kiểm tra.
            MessageBox.Show(
                $"Không thể xóa lớp \"{ma}\" vì còn dữ liệu liên quan (sinh viên) đang tham chiếu đến lớp này.",
                "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (SqlException ex)
        {
            ShowDbError("xóa lớp", ex);
        }
    }

    // =====================================================================
    // F6 - TÌM KIẾM (LIKE + parameter)
    // =====================================================================
    private void btnTimKiem_Click(object? sender, EventArgs e) => DoSearch();

    private void txtTimKiem_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            DoSearch();
            e.SuppressKeyPress = true;   // bỏ tiếng "ding" của Windows
        }
    }

    private void DoSearch()
    {
        string keyword = txtTimKiem.Text.Trim();

        ClearInput();
        if (!LoadData(keyword.Length == 0 ? null : keyword)) return;

        SetStatus(keyword.Length == 0
            ? "Hiển thị toàn bộ danh sách lớp"
            : $"Tìm thấy {dgvLop.RowCount} lớp với từ khóa \"{keyword}\"");
        txtTimKiem.Focus();
    }

    // =====================================================================
    // F7 - LÀM MỚI
    // =====================================================================
    private void btnLamMoi_Click(object? sender, EventArgs e)
    {
        ResetAndReload();
        SetStatus("Đã làm mới dữ liệu");
    }
}
