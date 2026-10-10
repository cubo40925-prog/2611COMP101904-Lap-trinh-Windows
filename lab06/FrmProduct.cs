using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace ProductManager;

public partial class FrmProduct : Form
{
    // ===== Dữ liệu trong bộ nhớ =====
    private readonly BindingList<Product> products = new();
    private readonly BindingSource productSource = new();

    private string selectedImagePath = string.Empty;   // ảnh đang chọn trên form
    private string currentKeyword = string.Empty;      // từ khóa tìm kiếm đang áp dụng
    private bool isUpdatingGrid;                       // chặn SelectionChanged khi đang nạp lưới
    private bool sortPriceAscending = true;
    private bool sortQuantityAscending = true;

    private static readonly CultureInfo ViCulture = new("vi-VN");

    public FrmProduct()
    {
        InitializeComponent();
        WireErrorClearing();
    }

    // =====================================================================
    // 1. LOAD FORM
    // =====================================================================
    private void FrmProduct_Load(object? sender, EventArgs e)
    {
        cboLoaiSP.Items.AddRange(new object[] { "Điện tử", "Văn phòng", "Gia dụng", "Thời trang" });

        ConfigureGrid();
        SeedSampleData();

        productSource.DataSource = products;
        dgvProducts.DataSource = productSource;

        ClearInput();
        UpdateStatus("Sẵn sàng");
    }

    private void ConfigureGrid()
    {
        dgvProducts.AutoGenerateColumns = false;
        dgvProducts.ReadOnly = true;
        dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvProducts.MultiSelect = false;
        dgvProducts.AllowUserToAddRows = false;
        dgvProducts.AllowUserToDeleteRows = false;
        dgvProducts.AllowUserToResizeRows = false;
        dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 252);

        dgvProducts.Columns.Clear();
        AddTextColumn(nameof(Product.MaSP), "Mã SP", 10);
        AddTextColumn(nameof(Product.TenSP), "Tên sản phẩm", 24);
        AddTextColumn(nameof(Product.LoaiSP), "Loại", 12);
        AddTextColumn(nameof(Product.DonGia), "Đơn giá", 13, "N0", DataGridViewContentAlignment.MiddleRight);
        AddTextColumn(nameof(Product.SoLuong), "Số lượng", 9, "N0", DataGridViewContentAlignment.MiddleRight);
        AddTextColumn(nameof(Product.NgayNhap), "Ngày nhập", 12, "dd/MM/yyyy");
        dgvProducts.Columns.Add(new DataGridViewCheckBoxColumn
        {
            DataPropertyName = nameof(Product.ConKinhDoanh),
            HeaderText = "Còn KD",
            FillWeight = 8
        });
        AddTextColumn(nameof(Product.ThanhTien), "Thành tiền", 14, "N0", DataGridViewContentAlignment.MiddleRight);
    }

    private void AddTextColumn(string property, string header, float weight,
        string? format = null, DataGridViewContentAlignment? align = null)
    {
        var col = new DataGridViewTextBoxColumn
        {
            DataPropertyName = property,
            HeaderText = header,
            FillWeight = weight
        };
        if (format != null) col.DefaultCellStyle.Format = format;
        if (align != null) col.DefaultCellStyle.Alignment = align.Value;
        dgvProducts.Columns.Add(col);
    }

    private void SeedSampleData()
    {
        products.Add(new Product
        {
            MaSP = "SP01", TenSP = "Chuột không dây", LoaiSP = "Điện tử",
            DonGia = 150000, SoLuong = 20, NgayNhap = DateTime.Today.AddDays(-30), ConKinhDoanh = true
        });
        products.Add(new Product
        {
            MaSP = "SP02", TenSP = "Bút bi Thiên Long", LoaiSP = "Văn phòng",
            DonGia = 5000, SoLuong = 100, NgayNhap = DateTime.Today.AddDays(-15), ConKinhDoanh = true
        });
        products.Add(new Product
        {
            MaSP = "SP03", TenSP = "Nồi cơm điện", LoaiSP = "Gia dụng",
            DonGia = 890000, SoLuong = 8, NgayNhap = DateTime.Today.AddDays(-7), ConKinhDoanh = true
        });
        products.Add(new Product
        {
            MaSP = "SP04", TenSP = "Áo thun nam", LoaiSP = "Thời trang",
            DonGia = 120000, SoLuong = 0, NgayNhap = DateTime.Today.AddDays(-60), ConKinhDoanh = false
        });
    }

    // =====================================================================
    // 2. HÀM HỖ TRỢ: kiểm tra, xóa nhập, nạp form, cập nhật trạng thái
    // =====================================================================

    /// <summary>
    /// Kiểm tra dữ liệu nhập. <paramref name="editing"/> là sản phẩm đang sửa
    /// (null khi thêm mới) để bỏ qua nó khi kiểm tra trùng mã.
    /// </summary>
    private bool ValidateInput(Product? editing)
    {
        errorProvider1.Clear();
        var errors = new List<string>();
        Control? firstInvalid = null;

        void Fail(Control control, string message)
        {
            errorProvider1.SetError(control, message);
            errors.Add(message);
            firstInvalid ??= control;
        }

        string ma = txtMaSP.Text.Trim();
        if (ma.Length == 0)
            Fail(txtMaSP, "Mã sản phẩm không được để trống");
        else if (products.Any(p => !ReferenceEquals(p, editing)
                                   && string.Equals(p.MaSP, ma, StringComparison.OrdinalIgnoreCase)))
            Fail(txtMaSP, "Mã sản phẩm đã tồn tại");

        if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            Fail(txtTenSP, "Tên sản phẩm không được để trống");

        if (cboLoaiSP.SelectedIndex < 0)
            Fail(cboLoaiSP, "Vui lòng chọn loại sản phẩm");

        if (numDonGia.Value <= 0)
            Fail(numDonGia, "Đơn giá phải lớn hơn 0");

        if (numSoLuong.Value < 0)
            Fail(numSoLuong, "Số lượng phải lớn hơn hoặc bằng 0");

        if (dtpNgayNhap.Value.Date > DateTime.Today)
            Fail(dtpNgayNhap, "Ngày nhập không được lớn hơn ngày hiện tại");

        if (errors.Count == 0) return true;

        MessageBox.Show("Dữ liệu chưa hợp lệ:\n- " + string.Join("\n- ", errors),
            "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        firstInvalid?.Focus();
        return false;
    }

    private void ClearInput()
    {
        txtMaSP.Clear();
        txtTenSP.Clear();
        cboLoaiSP.SelectedIndex = 0;
        numDonGia.Value = 0;
        numSoLuong.Value = 0;
        dtpNgayNhap.Value = DateTime.Today;
        chkConKinhDoanh.Checked = true;
        SetImage(string.Empty);
        errorProvider1.Clear();
        dgvProducts.ClearSelection();
        txtMaSP.Focus();
    }

    private void LoadProductToForm(Product p)
    {
        errorProvider1.Clear();
        txtMaSP.Text = p.MaSP;
        txtTenSP.Text = p.TenSP;
        cboLoaiSP.SelectedItem = p.LoaiSP;
        if (cboLoaiSP.SelectedIndex < 0) cboLoaiSP.SelectedIndex = 0;
        numDonGia.Value = Math.Clamp(p.DonGia, numDonGia.Minimum, numDonGia.Maximum);
        numSoLuong.Value = Math.Clamp(p.SoLuong, (int)numSoLuong.Minimum, (int)numSoLuong.Maximum);
        dtpNgayNhap.Value = p.NgayNhap;
        chkConKinhDoanh.Checked = p.ConKinhDoanh;
        SetImage(p.DuongDanAnh);
    }

    private Product BuildProductFromInput() => new()
    {
        MaSP = txtMaSP.Text.Trim(),
        TenSP = txtTenSP.Text.Trim(),
        LoaiSP = cboLoaiSP.Text,
        DonGia = numDonGia.Value,
        SoLuong = (int)numSoLuong.Value,
        NgayNhap = dtpNgayNhap.Value.Date,
        ConKinhDoanh = chkConKinhDoanh.Checked,
        DuongDanAnh = selectedImagePath
    };

    private void UpdateStatus(string message)
    {
        lblStatus.Text = message;

        lblCount.Text = string.IsNullOrEmpty(currentKeyword)
            ? $"Số sản phẩm: {products.Count}"
            : $"Hiển thị {productSource.Count}/{products.Count} sản phẩm";

        decimal tong = products.Sum(p => p.ThanhTien);
        lblTongGiaTri.Text = $"Tổng giá trị tồn kho: {tong.ToString("N0", ViCulture)} đ";
    }

    /// <summary>Lấy sản phẩm đang chọn trên lưới; báo cho người dùng nếu chưa chọn.</summary>
    private Product? GetSelectedProduct()
    {
        if (dgvProducts.SelectedRows.Count == 0
            || dgvProducts.SelectedRows[0].DataBoundItem is not Product p)
        {
            MessageBox.Show("Vui lòng chọn một sản phẩm trong danh sách.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }
        return p;
    }

    private bool Matches(Product p) =>
        p.MaSP.Contains(currentKeyword, StringComparison.CurrentCultureIgnoreCase)
        || p.TenSP.Contains(currentKeyword, StringComparison.CurrentCultureIgnoreCase);

    /// <summary>Nạp lại lưới theo từ khóa hiện tại (rỗng = hiển thị tất cả).</summary>
    private void RefreshGrid(Product? reselect = null)
    {
        isUpdatingGrid = true;
        try
        {
            productSource.DataSource = string.IsNullOrEmpty(currentKeyword)
                ? products
                : new BindingList<Product>(products.Where(Matches).ToList());
            productSource.ResetBindings(false);
            dgvProducts.ClearSelection();

            if (reselect != null)
            {
                int index = productSource.IndexOf(reselect);
                if (index >= 0)
                {
                    dgvProducts.CurrentCell = dgvProducts.Rows[index].Cells[0];
                    dgvProducts.Rows[index].Selected = true;
                }
            }
        }
        finally
        {
            isUpdatingGrid = false;
        }
    }

    private void SetImage(string path)
    {
        selectedImagePath = path ?? string.Empty;

        var old = picSanPham.Image;
        picSanPham.Image = null;
        old?.Dispose();

        if (string.IsNullOrWhiteSpace(selectedImagePath) || !File.Exists(selectedImagePath)) return;
        try
        {
            // Đọc qua stream rồi copy để không khóa file ảnh trên đĩa.
            using var fs = new FileStream(selectedImagePath, FileMode.Open, FileAccess.Read);
            using var img = Image.FromStream(fs);
            picSanPham.Image = new Bitmap(img);
        }
        catch (Exception)
        {
            picSanPham.Image = null;
        }
    }

    /// <summary>Gõ lại vào control nào thì xóa biểu tượng lỗi của control đó.</summary>
    private void WireErrorClearing()
    {
        txtMaSP.TextChanged += (_, _) => errorProvider1.SetError(txtMaSP, "");
        txtTenSP.TextChanged += (_, _) => errorProvider1.SetError(txtTenSP, "");
        cboLoaiSP.SelectedIndexChanged += (_, _) => errorProvider1.SetError(cboLoaiSP, "");
        numDonGia.ValueChanged += (_, _) => errorProvider1.SetError(numDonGia, "");
        numSoLuong.ValueChanged += (_, _) => errorProvider1.SetError(numSoLuong, "");
        dtpNgayNhap.ValueChanged += (_, _) => errorProvider1.SetError(dtpNgayNhap, "");
    }

    // =====================================================================
    // 3. THÊM / SỬA / XÓA / TÌM KIẾM / LÀM MỚI / CHỌN ẢNH
    // =====================================================================
    private void btnThem_Click(object? sender, EventArgs e)
    {
        if (!ValidateInput(null)) return;

        products.Add(BuildProductFromInput());

        // Bỏ bộ lọc để sản phẩm mới luôn nhìn thấy được.
        currentKeyword = string.Empty;
        tstTimKiem.Clear();

        RefreshGrid();
        ClearInput();
        if (dgvProducts.RowCount > 0)
            dgvProducts.FirstDisplayedScrollingRowIndex = dgvProducts.RowCount - 1;
        UpdateStatus("Đã thêm sản phẩm mới");
    }

    private void btnSua_Click(object? sender, EventArgs e)
    {
        Product? p = GetSelectedProduct();
        if (p == null) return;
        if (!ValidateInput(p)) return;

        p.MaSP = txtMaSP.Text.Trim();
        p.TenSP = txtTenSP.Text.Trim();
        p.LoaiSP = cboLoaiSP.Text;
        p.DonGia = numDonGia.Value;
        p.SoLuong = (int)numSoLuong.Value;
        p.NgayNhap = dtpNgayNhap.Value.Date;
        p.ConKinhDoanh = chkConKinhDoanh.Checked;
        p.DuongDanAnh = selectedImagePath;

        RefreshGrid(p);
        UpdateStatus($"Đã cập nhật sản phẩm {p.MaSP}");
    }

    private void btnXoa_Click(object? sender, EventArgs e)
    {
        Product? p = GetSelectedProduct();
        if (p == null) return;

        DialogResult result = MessageBox.Show(
            $"Bạn có chắc muốn xóa sản phẩm \"{p.TenSP}\" ({p.MaSP})?",
            "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result != DialogResult.Yes) return;

        products.Remove(p);
        RefreshGrid();
        ClearInput();
        UpdateStatus($"Đã xóa sản phẩm {p.MaSP}");
    }

    private void DoSearch()
    {
        currentKeyword = tstTimKiem.Text.Trim();
        RefreshGrid();
        ClearInput();
        UpdateStatus(string.IsNullOrEmpty(currentKeyword)
            ? "Hiển thị toàn bộ danh sách"
            : $"Tìm thấy {productSource.Count} sản phẩm với từ khóa \"{currentKeyword}\"");
        tstTimKiem.Focus();
    }

    private void DoRefresh()
    {
        currentKeyword = string.Empty;
        tstTimKiem.Clear();
        RefreshGrid();
        ClearInput();
        UpdateStatus("Đã làm mới dữ liệu");
    }

    private void btnLamMoi_Click(object? sender, EventArgs e) => DoRefresh();

    private void btnChonAnh_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Chọn ảnh sản phẩm",
            Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All files|*.*"
        };

        if (dialog.ShowDialog() != DialogResult.OK) return;

        SetImage(dialog.FileName);
        if (picSanPham.Image == null)
        {
            MessageBox.Show("Không đọc được file ảnh đã chọn.", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        UpdateStatus("Đã chọn ảnh sản phẩm");
    }

    // =====================================================================
    // 4. SỰ KIỆN DATAGRIDVIEW (chuột, bàn phím, context menu)
    // =====================================================================
    private void dgvProducts_SelectionChanged(object? sender, EventArgs e)
    {
        if (isUpdatingGrid) return;
        if (dgvProducts.SelectedRows.Count == 0) return;
        if (dgvProducts.SelectedRows[0].DataBoundItem is Product p)
            LoadProductToForm(p);
    }

    private void dgvProducts_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
    {
        // Nhấn chuột phải: chọn luôn dòng đó để menu thao tác đúng sản phẩm.
        if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
        {
            dgvProducts.CurrentCell = dgvProducts.Rows[e.RowIndex].Cells[0];
            dgvProducts.Rows[e.RowIndex].Selected = true;
        }
    }

    private void dgvProducts_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0) ShowDetail();
    }

    private void dgvProducts_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Delete)
        {
            btnXoa.PerformClick();
            e.Handled = true;
        }
    }

    private void cmsDgv_Opening(object? sender, CancelEventArgs e)
    {
        // Không hiện menu khi chuột phải vào vùng trống (chưa chọn dòng nào).
        e.Cancel = dgvProducts.SelectedRows.Count == 0;
    }

    private void cmsSua_Click(object? sender, EventArgs e)
    {
        Product? p = GetSelectedProduct();
        if (p == null) return;

        LoadProductToForm(p);
        txtTenSP.Focus();
        UpdateStatus($"Đang sửa {p.MaSP}: chỉnh thông tin rồi nhấn nút Sửa để lưu");
    }

    private void cmsXoa_Click(object? sender, EventArgs e) => btnXoa.PerformClick();

    private void cmsChiTiet_Click(object? sender, EventArgs e) => ShowDetail();

    private void ShowDetail()
    {
        Product? p = GetSelectedProduct();
        if (p == null) return;

        string message =
            $"Mã SP: {p.MaSP}\n" +
            $"Tên SP: {p.TenSP}\n" +
            $"Loại: {p.LoaiSP}\n" +
            $"Đơn giá: {p.DonGia.ToString("N0", ViCulture)} đ\n" +
            $"Số lượng: {p.SoLuong}\n" +
            $"Ngày nhập: {p.NgayNhap:dd/MM/yyyy}\n" +
            $"Trạng thái: {(p.ConKinhDoanh ? "Còn kinh doanh" : "Ngừng kinh doanh")}\n" +
            $"Thành tiền (tồn kho): {p.ThanhTien.ToString("N0", ViCulture)} đ\n" +
            $"Ảnh: {(string.IsNullOrWhiteSpace(p.DuongDanAnh) ? "(chưa có)" : p.DuongDanAnh)}";

        MessageBox.Show(message, "Chi tiết sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    // =====================================================================
    // 5. MENU & TOOLBAR (gọi lại các hàm chính, không viết trùng code)
    // =====================================================================
    private void mnuFileLamMoi_Click(object? sender, EventArgs e) => DoRefresh();
    private void mnuFileThoat_Click(object? sender, EventArgs e) => Close();
    private void mnuThem_Click(object? sender, EventArgs e) => btnThem.PerformClick();
    private void mnuSua_Click(object? sender, EventArgs e) => btnSua.PerformClick();
    private void mnuXoa_Click(object? sender, EventArgs e) => btnXoa.PerformClick();

    private void mnuTimKiem_Click(object? sender, EventArgs e)
    {
        tstTimKiem.Focus();
        tstTimKiem.SelectAll();
    }

    private void tsbThem_Click(object? sender, EventArgs e) => btnThem.PerformClick();
    private void tsbSua_Click(object? sender, EventArgs e) => btnSua.PerformClick();
    private void tsbXoa_Click(object? sender, EventArgs e) => btnXoa.PerformClick();
    private void tsbLamMoi_Click(object? sender, EventArgs e) => btnLamMoi.PerformClick();
    private void tsbTimKiem_Click(object? sender, EventArgs e) => DoSearch();

    private void tstTimKiem_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            DoSearch();
            e.SuppressKeyPress = true;
        }
    }

    private void mnuGioiThieu_Click(object? sender, EventArgs e)
    {
        MessageBox.Show(
            "ProductManager - Lab 05 COMP1019\n" +
            "Quản lý sản phẩm bằng Windows Forms, DataGridView, BindingList/BindingSource.\n\n" +
            "Phím tắt:\n" +
            "Ctrl+N: Thêm | Ctrl+E: Sửa | Ctrl+D hoặc Delete (trên lưới): Xóa\n" +
            "Ctrl+F: Tìm kiếm | F5: Làm mới | Ctrl+S: Xuất .txt | F1: Giới thiệu\n" +
            "Chuột phải / nhấp đúp vào dòng: menu và xem chi tiết.",
            "Giới thiệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    // =====================================================================
    // 6. MỞ RỘNG: sắp xếp và xuất file .txt
    // =====================================================================
    private void mnuSapXepGia_Click(object? sender, EventArgs e)
    {
        SortProducts(p => p.DonGia, sortPriceAscending);
        UpdateStatus($"Đã sắp xếp theo đơn giá {(sortPriceAscending ? "tăng" : "giảm")} dần");
        sortPriceAscending = !sortPriceAscending;
    }

    private void mnuSapXepSoLuong_Click(object? sender, EventArgs e)
    {
        SortProducts(p => p.SoLuong, sortQuantityAscending);
        UpdateStatus($"Đã sắp xếp theo số lượng {(sortQuantityAscending ? "tăng" : "giảm")} dần");
        sortQuantityAscending = !sortQuantityAscending;
    }

    private void SortProducts<TKey>(Func<Product, TKey> keySelector, bool ascending)
    {
        var sorted = (ascending ? products.OrderBy(keySelector) : products.OrderByDescending(keySelector)).ToList();

        products.RaiseListChangedEvents = false;
        products.Clear();
        foreach (var p in sorted) products.Add(p);
        products.RaiseListChangedEvents = true;

        RefreshGrid();
        ClearInput();
    }

    private void mnuFileXuat_Click(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog
        {
            Title = "Xuất danh sách sản phẩm",
            Filter = "Text files (*.txt)|*.txt",
            FileName = "DanhSachSanPham.txt"
        };
        if (dialog.ShowDialog() != DialogResult.OK) return;

        var sb = new StringBuilder();
        sb.AppendLine("MaSP\tTenSP\tLoaiSP\tDonGia\tSoLuong\tNgayNhap\tConKinhDoanh\tDuongDanAnh");
        foreach (var p in products)
        {
            sb.AppendLine(string.Join('\t',
                p.MaSP, p.TenSP, p.LoaiSP,
                p.DonGia.ToString("0", CultureInfo.InvariantCulture),
                p.SoLuong,
                p.NgayNhap.ToString("dd/MM/yyyy"),
                p.ConKinhDoanh ? "Còn kinh doanh" : "Ngừng kinh doanh",
                p.DuongDanAnh));
        }

        try
        {
            File.WriteAllText(dialog.FileName, sb.ToString(), new UTF8Encoding(true));
            UpdateStatus($"Đã xuất {products.Count} sản phẩm ra {Path.GetFileName(dialog.FileName)}");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Không ghi được file: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void btnThemLoai_Click(object? sender, EventArgs e) => AddCategory();

    /// <summary>Hỏi tên loại mới, kiểm tra rồi thêm vào ComboBox và chọn luôn loại đó.</summary>
    private void AddCategory()
    {
        string? input = PromptText("Thêm loại sản phẩm", "Nhập tên loại sản phẩm mới:");
        if (input == null) return;   // người dùng bấm Hủy

        string name = input.Trim();
        if (name.Length == 0)
        {
            MessageBox.Show("Tên loại sản phẩm không được để trống.", "Cảnh báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Nếu đã có (không phân biệt hoa/thường) thì chỉ chọn lại loại đó.
        foreach (object item in cboLoaiSP.Items)
        {
            if (string.Equals(item.ToString(), name, StringComparison.CurrentCultureIgnoreCase))
            {
                cboLoaiSP.SelectedItem = item;
                MessageBox.Show($"Loại \"{item}\" đã tồn tại, đã chọn sẵn cho bạn.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        cboLoaiSP.Items.Add(name);
        cboLoaiSP.SelectedItem = name;
        UpdateStatus($"Đã thêm loại sản phẩm \"{name}\"");
    }

    /// <summary>Hộp thoại nhập một dòng văn bản; trả về null nếu bấm Hủy.</summary>
    private static string? PromptText(string title, string prompt)
    {
        using var dialog = new Form
        {
            Text = title,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            MaximizeBox = false,
            MinimizeBox = false,
            ShowInTaskbar = false,
            ClientSize = new Size(360, 130)
        };
        var lbl = new Label { Text = prompt, Location = new Point(15, 15), AutoSize = true };
        var txt = new TextBox { Location = new Point(15, 42), Size = new Size(330, 23) };
        var ok = new Button
        {
            Text = "Thêm",
            DialogResult = DialogResult.OK,
            Location = new Point(165, 85),
            Size = new Size(85, 30)
        };
        var cancel = new Button
        {
            Text = "Hủy",
            DialogResult = DialogResult.Cancel,
            Location = new Point(260, 85),
            Size = new Size(85, 30)
        };
        dialog.Controls.AddRange(new Control[] { lbl, txt, ok, cancel });
        dialog.AcceptButton = ok;
        dialog.CancelButton = cancel;

        return dialog.ShowDialog() == DialogResult.OK ? txt.Text : null;
    }
}
