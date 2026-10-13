using System.ComponentModel;
using System.Globalization;
using ShopManagerMini.Data;
using ShopManagerMini.Models;

namespace ShopManagerMini.Forms;

/// <summary>Quản lý sản phẩm (Form con MDI): thêm, sửa, xóa, tìm kiếm, làm mới trong bộ nhớ.</summary>
public partial class FrmProduct : Form
{
    private static readonly string[] Categories = { "Thực phẩm", "Đồ uống", "Gia dụng", "Bánh kẹo", "Khác" };

    private readonly BindingSource productSource = new();
    private string currentKeyword = string.Empty;   // từ khóa tìm kiếm đang áp dụng
    private bool isUpdatingGrid;                    // chặn SelectionChanged khi đang nạp lại lưới

    private static BindingList<Product> Products => DataStore.Products;

    public FrmProduct()
    {
        InitializeComponent();
    }

    // ---------------------------------------------------------------- Load
    private void FrmProduct_Load(object? sender, EventArgs e)
    {
        cboLoaiSP.Items.AddRange(Categories);
        ConfigureGrid();

        productSource.DataSource = Products;
        dgvProducts.DataSource = productSource;

        ClearInput();
        ShowMessage("Sẵn sàng");
    }

    private void ConfigureGrid()
    {
        dgvProducts.AutoGenerateColumns = false;
        dgvProducts.ReadOnly = true;
        dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvProducts.MultiSelect = false;
        dgvProducts.AllowUserToAddRows = false;
        dgvProducts.AllowUserToDeleteRows = false;
        dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        dgvProducts.Columns.Clear();
        AddColumn(nameof(Product.MaSP), "Mã SP", 15);
        AddColumn(nameof(Product.TenSP), "Tên sản phẩm", 35);
        AddColumn(nameof(Product.LoaiSP), "Loại", 18);
        AddColumn(nameof(Product.DonGia), "Đơn giá", 18, "N0", DataGridViewContentAlignment.MiddleRight);
        AddColumn(nameof(Product.SoLuong), "Số lượng", 14, "N0", DataGridViewContentAlignment.MiddleRight);
    }

    private void AddColumn(string property, string header, float weight,
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

    // ---------------------------------------------------------------- Hàm hỗ trợ
    /// <summary>
    /// Kiểm tra dữ liệu nhập. <paramref name="editing"/> là sản phẩm đang sửa (null khi thêm)
    /// để bỏ qua nó khi kiểm tra trùng mã. Trả về đơn giá và số lượng đã đọc được.
    /// </summary>
    private bool ValidateInput(Product? editing, out decimal donGia, out int soLuong)
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
        else if (Products.Any(p => !ReferenceEquals(p, editing)
                                   && string.Equals(p.MaSP, ma, StringComparison.OrdinalIgnoreCase)))
            Fail(txtMaSP, "Mã sản phẩm đã tồn tại");

        if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            Fail(txtTenSP, "Tên sản phẩm không được để trống");

        if (cboLoaiSP.SelectedIndex < 0)
            Fail(cboLoaiSP, "Vui lòng chọn loại sản phẩm");

        if (!decimal.TryParse(txtDonGia.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out donGia))
            Fail(txtDonGia, "Đơn giá phải là một số hợp lệ");
        else if (donGia < 0)
            Fail(txtDonGia, "Đơn giá không được âm");

        if (!int.TryParse(txtSoLuong.Text.Trim(), NumberStyles.Integer | NumberStyles.AllowThousands,
                CultureInfo.CurrentCulture, out soLuong))
            Fail(txtSoLuong, "Số lượng phải là số nguyên hợp lệ");
        else if (soLuong < 0)
            Fail(txtSoLuong, "Số lượng không được âm");

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
        txtDonGia.Clear();
        txtSoLuong.Clear();
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
        txtDonGia.Text = p.DonGia.ToString("0.##", CultureInfo.CurrentCulture);
        txtSoLuong.Text = p.SoLuong.ToString(CultureInfo.CurrentCulture);
    }

    private void ShowMessage(string message)
    {
        string count = string.IsNullOrEmpty(currentKeyword)
            ? $"Tổng số sản phẩm: {Products.Count}"
            : $"Hiển thị {productSource.Count}/{Products.Count} sản phẩm";
        lblThongBao.Text = $"{message}  |  {count}";
    }

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
                ? Products
                : new BindingList<Product>(Products.Where(Matches).ToList());
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

    // ---------------------------------------------------------------- Sự kiện
    private void dgvProducts_SelectionChanged(object? sender, EventArgs e)
    {
        if (isUpdatingGrid || dgvProducts.SelectedRows.Count == 0) return;
        if (dgvProducts.SelectedRows[0].DataBoundItem is Product p)
            LoadProductToForm(p);
    }

    private void btnThem_Click(object? sender, EventArgs e)
    {
        if (!ValidateInput(null, out decimal donGia, out int soLuong)) return;

        var p = new Product
        {
            MaSP = txtMaSP.Text.Trim(),
            TenSP = txtTenSP.Text.Trim(),
            LoaiSP = cboLoaiSP.Text,
            DonGia = donGia,
            SoLuong = soLuong
        };
        Products.Add(p);

        // Bỏ bộ lọc để sản phẩm mới luôn nhìn thấy được.
        currentKeyword = string.Empty;
        txtTimKiem.Clear();
        RefreshGrid();
        ClearInput();
        if (dgvProducts.RowCount > 0)
            dgvProducts.FirstDisplayedScrollingRowIndex = dgvProducts.RowCount - 1;
        ShowMessage($"Đã thêm sản phẩm {p.MaSP}");
    }

    private void btnSua_Click(object? sender, EventArgs e)
    {
        Product? p = GetSelectedProduct();
        if (p == null) return;
        if (!ValidateInput(p, out decimal donGia, out int soLuong)) return;

        p.MaSP = txtMaSP.Text.Trim();
        p.TenSP = txtTenSP.Text.Trim();
        p.LoaiSP = cboLoaiSP.Text;
        p.DonGia = donGia;
        p.SoLuong = soLuong;

        RefreshGrid(p);
        ShowMessage($"Đã cập nhật sản phẩm {p.MaSP}");
    }

    private void btnXoa_Click(object? sender, EventArgs e)
    {
        Product? p = GetSelectedProduct();
        if (p == null) return;

        DialogResult result = MessageBox.Show(
            $"Bạn có chắc muốn xóa sản phẩm \"{p.TenSP}\" ({p.MaSP})?",
            "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result != DialogResult.Yes) return;

        Products.Remove(p);
        RefreshGrid();
        ClearInput();
        ShowMessage($"Đã xóa sản phẩm {p.MaSP}");
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
            : $"Tìm thấy {productSource.Count} sản phẩm với từ khóa \"{currentKeyword}\"");
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
