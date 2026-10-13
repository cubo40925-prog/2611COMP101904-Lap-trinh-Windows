using System.ComponentModel;
using System.Text.RegularExpressions;
using ShopManagerMini.Data;
using ShopManagerMini.Models;

namespace ShopManagerMini.Forms;

/// <summary>Quản lý khách hàng (Form con MDI): thêm, sửa, xóa, tìm kiếm, làm mới trong bộ nhớ.</summary>
public partial class FrmCustomer : Form
{
    private static readonly string[] CustomerTypes = { "Thường", "Thân thiết", "VIP" };

    // Số điện thoại Việt Nam: bắt đầu bằng 0, gồm 10 hoặc 11 chữ số.
    private static readonly Regex PhoneRegex = new(@"^0\d{9,10}$", RegexOptions.Compiled);

    private readonly BindingSource customerSource = new();
    private string currentKeyword = string.Empty;
    private bool isUpdatingGrid;

    private static BindingList<Customer> Customers => DataStore.Customers;

    public FrmCustomer()
    {
        InitializeComponent();
    }

    // ---------------------------------------------------------------- Load
    private void FrmCustomer_Load(object? sender, EventArgs e)
    {
        cboLoaiKH.Items.AddRange(CustomerTypes);
        ConfigureGrid();

        customerSource.DataSource = Customers;
        dgvCustomers.DataSource = customerSource;

        ClearInput();
        ShowMessage("Sẵn sàng");
    }

    private void ConfigureGrid()
    {
        dgvCustomers.AutoGenerateColumns = false;
        dgvCustomers.ReadOnly = true;
        dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCustomers.MultiSelect = false;
        dgvCustomers.AllowUserToAddRows = false;
        dgvCustomers.AllowUserToDeleteRows = false;
        dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        dgvCustomers.Columns.Clear();
        AddColumn(nameof(Customer.MaKH), "Mã KH", 15);
        AddColumn(nameof(Customer.HoTen), "Họ tên", 35);
        AddColumn(nameof(Customer.SoDienThoai), "Số điện thoại", 25);
        AddColumn(nameof(Customer.LoaiKH), "Loại KH", 25);
    }

    private void AddColumn(string property, string header, float weight)
    {
        dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = property,
            HeaderText = header,
            FillWeight = weight
        });
    }

    // ---------------------------------------------------------------- Hàm hỗ trợ
    private bool ValidateInput(Customer? editing)
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

        string ma = txtMaKH.Text.Trim();
        if (ma.Length == 0)
            Fail(txtMaKH, "Mã khách hàng không được để trống");
        else if (Customers.Any(c => !ReferenceEquals(c, editing)
                                    && string.Equals(c.MaKH, ma, StringComparison.OrdinalIgnoreCase)))
            Fail(txtMaKH, "Mã khách hàng đã tồn tại");

        if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            Fail(txtHoTen, "Họ tên không được để trống");

        string sdt = txtSoDienThoai.Text.Trim();
        if (sdt.Length == 0)
            Fail(txtSoDienThoai, "Số điện thoại không được để trống");
        else if (!PhoneRegex.IsMatch(sdt))
            Fail(txtSoDienThoai, "Số điện thoại phải bắt đầu bằng 0 và gồm 10-11 chữ số");

        if (cboLoaiKH.SelectedIndex < 0)
            Fail(cboLoaiKH, "Vui lòng chọn loại khách hàng");

        if (errors.Count == 0) return true;

        MessageBox.Show("Dữ liệu chưa hợp lệ:\n- " + string.Join("\n- ", errors),
            "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        firstInvalid?.Focus();
        return false;
    }

    private void ClearInput()
    {
        txtMaKH.Clear();
        txtHoTen.Clear();
        txtSoDienThoai.Clear();
        cboLoaiKH.SelectedIndex = 0;
        errorProvider1.Clear();
        dgvCustomers.ClearSelection();
        txtMaKH.Focus();
    }

    private void LoadCustomerToForm(Customer c)
    {
        errorProvider1.Clear();
        txtMaKH.Text = c.MaKH;
        txtHoTen.Text = c.HoTen;
        txtSoDienThoai.Text = c.SoDienThoai;
        cboLoaiKH.SelectedItem = c.LoaiKH;
        if (cboLoaiKH.SelectedIndex < 0) cboLoaiKH.SelectedIndex = 0;
    }

    private void ShowMessage(string message)
    {
        string count = string.IsNullOrEmpty(currentKeyword)
            ? $"Tổng số khách hàng: {Customers.Count}"
            : $"Hiển thị {customerSource.Count}/{Customers.Count} khách hàng";
        lblThongBao.Text = $"{message}  |  {count}";
    }

    private Customer? GetSelectedCustomer()
    {
        if (dgvCustomers.SelectedRows.Count == 0
            || dgvCustomers.SelectedRows[0].DataBoundItem is not Customer c)
        {
            MessageBox.Show("Vui lòng chọn một khách hàng trong danh sách.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }
        return c;
    }

    private bool Matches(Customer c) =>
        c.MaKH.Contains(currentKeyword, StringComparison.CurrentCultureIgnoreCase)
        || c.HoTen.Contains(currentKeyword, StringComparison.CurrentCultureIgnoreCase)
        || c.SoDienThoai.Contains(currentKeyword, StringComparison.CurrentCultureIgnoreCase);

    private void RefreshGrid(Customer? reselect = null)
    {
        isUpdatingGrid = true;
        try
        {
            customerSource.DataSource = string.IsNullOrEmpty(currentKeyword)
                ? Customers
                : new BindingList<Customer>(Customers.Where(Matches).ToList());
            customerSource.ResetBindings(false);
            dgvCustomers.ClearSelection();

            if (reselect != null)
            {
                int index = customerSource.IndexOf(reselect);
                if (index >= 0)
                {
                    dgvCustomers.CurrentCell = dgvCustomers.Rows[index].Cells[0];
                    dgvCustomers.Rows[index].Selected = true;
                }
            }
        }
        finally
        {
            isUpdatingGrid = false;
        }
    }

    // ---------------------------------------------------------------- Sự kiện
    private void dgvCustomers_SelectionChanged(object? sender, EventArgs e)
    {
        if (isUpdatingGrid || dgvCustomers.SelectedRows.Count == 0) return;
        if (dgvCustomers.SelectedRows[0].DataBoundItem is Customer c)
            LoadCustomerToForm(c);
    }

    private void btnThem_Click(object? sender, EventArgs e)
    {
        if (!ValidateInput(null)) return;

        var c = new Customer
        {
            MaKH = txtMaKH.Text.Trim(),
            HoTen = txtHoTen.Text.Trim(),
            SoDienThoai = txtSoDienThoai.Text.Trim(),
            LoaiKH = cboLoaiKH.Text
        };
        Customers.Add(c);

        currentKeyword = string.Empty;
        txtTimKiem.Clear();
        RefreshGrid();
        ClearInput();
        if (dgvCustomers.RowCount > 0)
            dgvCustomers.FirstDisplayedScrollingRowIndex = dgvCustomers.RowCount - 1;
        ShowMessage($"Đã thêm khách hàng {c.MaKH}");
    }

    private void btnSua_Click(object? sender, EventArgs e)
    {
        Customer? c = GetSelectedCustomer();
        if (c == null) return;
        if (!ValidateInput(c)) return;

        c.MaKH = txtMaKH.Text.Trim();
        c.HoTen = txtHoTen.Text.Trim();
        c.SoDienThoai = txtSoDienThoai.Text.Trim();
        c.LoaiKH = cboLoaiKH.Text;

        RefreshGrid(c);
        ShowMessage($"Đã cập nhật khách hàng {c.MaKH}");
    }

    private void btnXoa_Click(object? sender, EventArgs e)
    {
        Customer? c = GetSelectedCustomer();
        if (c == null) return;

        DialogResult result = MessageBox.Show(
            $"Bạn có chắc muốn xóa khách hàng \"{c.HoTen}\" ({c.MaKH})?",
            "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result != DialogResult.Yes) return;

        Customers.Remove(c);
        RefreshGrid();
        ClearInput();
        ShowMessage($"Đã xóa khách hàng {c.MaKH}");
    }

    private void btnLamMoi_Click(object? sender, EventArgs e)
    {
        currentKeyword = string.Empty;
        txtTimKiem.Clear();
        RefreshGrid();
        ClearInput();
        ShowMessage("Đã làm mới dữ liệu");
    }

    private void btnTimKiem_Click(object? sender, EventArgs e)
    {
        currentKeyword = txtTimKiem.Text.Trim();
        RefreshGrid();
        ClearInput();
        ShowMessage(string.IsNullOrEmpty(currentKeyword)
            ? "Hiển thị toàn bộ danh sách"
            : $"Tìm thấy {customerSource.Count} khách hàng với từ khóa \"{currentKeyword}\"");
        txtTimKiem.Focus();
    }

    private void txtTimKiem_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnTimKiem.PerformClick();
            e.SuppressKeyPress = true;
        }
    }
}
