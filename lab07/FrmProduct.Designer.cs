namespace ShopManagerMini.Forms;

partial class FrmProduct
{
    private System.ComponentModel.IContainer? components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        grbThongTin = new GroupBox();
        lblMaSP = new Label();
        txtMaSP = new TextBox();
        lblTenSP = new Label();
        txtTenSP = new TextBox();
        lblLoaiSP = new Label();
        cboLoaiSP = new ComboBox();
        lblDonGia = new Label();
        txtDonGia = new TextBox();
        lblSoLuong = new Label();
        txtSoLuong = new TextBox();
        pnlTool = new Panel();
        btnThem = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnLamMoi = new Button();
        txtTimKiem = new TextBox();
        btnTimKiem = new Button();
        dgvProducts = new DataGridView();
        lblThongBao = new Label();
        errorProvider1 = new ErrorProvider(components);
        grbThongTin.SuspendLayout();
        pnlTool.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
        ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
        SuspendLayout();
        // 
        // grbThongTin
        // 
        grbThongTin.Controls.Add(lblMaSP);
        grbThongTin.Controls.Add(txtMaSP);
        grbThongTin.Controls.Add(lblTenSP);
        grbThongTin.Controls.Add(txtTenSP);
        grbThongTin.Controls.Add(lblLoaiSP);
        grbThongTin.Controls.Add(cboLoaiSP);
        grbThongTin.Controls.Add(lblDonGia);
        grbThongTin.Controls.Add(txtDonGia);
        grbThongTin.Controls.Add(lblSoLuong);
        grbThongTin.Controls.Add(txtSoLuong);
        grbThongTin.Dock = DockStyle.Top;
        grbThongTin.Location = new Point(0, 0);
        grbThongTin.Name = "grbThongTin";
        grbThongTin.Size = new Size(860, 140);
        grbThongTin.TabIndex = 0;
        grbThongTin.TabStop = false;
        grbThongTin.Text = "Thông tin sản phẩm";
        // 
        // lblMaSP
        // 
        lblMaSP.AutoSize = true;
        lblMaSP.Location = new Point(20, 33);
        lblMaSP.Name = "lblMaSP";
        lblMaSP.Text = "Mã SP:";
        // 
        // txtMaSP
        // 
        txtMaSP.Location = new Point(110, 30);
        txtMaSP.Name = "txtMaSP";
        txtMaSP.Size = new Size(180, 23);
        txtMaSP.TabIndex = 0;
        // 
        // lblTenSP
        // 
        lblTenSP.AutoSize = true;
        lblTenSP.Location = new Point(330, 33);
        lblTenSP.Name = "lblTenSP";
        lblTenSP.Text = "Tên SP:";
        // 
        // txtTenSP
        // 
        txtTenSP.Location = new Point(410, 30);
        txtTenSP.Name = "txtTenSP";
        txtTenSP.Size = new Size(260, 23);
        txtTenSP.TabIndex = 1;
        // 
        // lblLoaiSP
        // 
        lblLoaiSP.AutoSize = true;
        lblLoaiSP.Location = new Point(20, 68);
        lblLoaiSP.Name = "lblLoaiSP";
        lblLoaiSP.Text = "Loại SP:";
        // 
        // cboLoaiSP
        // 
        cboLoaiSP.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLoaiSP.Location = new Point(110, 65);
        cboLoaiSP.Name = "cboLoaiSP";
        cboLoaiSP.Size = new Size(180, 23);
        cboLoaiSP.TabIndex = 2;
        // 
        // lblDonGia
        // 
        lblDonGia.AutoSize = true;
        lblDonGia.Location = new Point(330, 68);
        lblDonGia.Name = "lblDonGia";
        lblDonGia.Text = "Đơn giá:";
        // 
        // txtDonGia
        // 
        txtDonGia.Location = new Point(410, 65);
        txtDonGia.Name = "txtDonGia";
        txtDonGia.Size = new Size(260, 23);
        txtDonGia.TabIndex = 3;
        txtDonGia.TextAlign = HorizontalAlignment.Right;
        // 
        // lblSoLuong
        // 
        lblSoLuong.AutoSize = true;
        lblSoLuong.Location = new Point(20, 103);
        lblSoLuong.Name = "lblSoLuong";
        lblSoLuong.Text = "Số lượng:";
        // 
        // txtSoLuong
        // 
        txtSoLuong.Location = new Point(110, 100);
        txtSoLuong.Name = "txtSoLuong";
        txtSoLuong.Size = new Size(180, 23);
        txtSoLuong.TabIndex = 4;
        txtSoLuong.TextAlign = HorizontalAlignment.Right;
        // 
        // pnlTool
        // 
        pnlTool.Controls.Add(btnThem);
        pnlTool.Controls.Add(btnSua);
        pnlTool.Controls.Add(btnXoa);
        pnlTool.Controls.Add(btnLamMoi);
        pnlTool.Controls.Add(txtTimKiem);
        pnlTool.Controls.Add(btnTimKiem);
        pnlTool.Dock = DockStyle.Top;
        pnlTool.Location = new Point(0, 140);
        pnlTool.Name = "pnlTool";
        pnlTool.Size = new Size(860, 46);
        pnlTool.TabIndex = 1;
        // 
        // btnThem
        // 
        btnThem.Location = new Point(10, 7);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(90, 32);
        btnThem.TabIndex = 5;
        btnThem.Text = "Thêm";
        btnThem.UseVisualStyleBackColor = true;
        btnThem.Click += btnThem_Click;
        // 
        // btnSua
        // 
        btnSua.Location = new Point(106, 7);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(90, 32);
        btnSua.TabIndex = 6;
        btnSua.Text = "Sửa";
        btnSua.UseVisualStyleBackColor = true;
        btnSua.Click += btnSua_Click;
        // 
        // btnXoa
        // 
        btnXoa.Location = new Point(202, 7);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(90, 32);
        btnXoa.TabIndex = 7;
        btnXoa.Text = "Xóa";
        btnXoa.UseVisualStyleBackColor = true;
        btnXoa.Click += btnXoa_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.Location = new Point(298, 7);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(90, 32);
        btnLamMoi.TabIndex = 8;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = true;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // txtTimKiem
        // 
        txtTimKiem.Location = new Point(470, 12);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Nhập mã hoặc tên sản phẩm...";
        txtTimKiem.Size = new Size(230, 23);
        txtTimKiem.TabIndex = 9;
        txtTimKiem.KeyDown += txtTimKiem_KeyDown;
        // 
        // btnTimKiem
        // 
        btnTimKiem.Location = new Point(706, 7);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(90, 32);
        btnTimKiem.TabIndex = 10;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = true;
        btnTimKiem.Click += btnTimKiem_Click;
        // 
        // dgvProducts
        // 
        dgvProducts.BackgroundColor = SystemColors.Window;
        dgvProducts.Dock = DockStyle.Fill;
        dgvProducts.Location = new Point(0, 186);
        dgvProducts.Name = "dgvProducts";
        dgvProducts.RowHeadersVisible = false;
        dgvProducts.Size = new Size(860, 350);
        dgvProducts.TabIndex = 11;
        dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
        // 
        // lblThongBao
        // 
        lblThongBao.BackColor = SystemColors.ControlLight;
        lblThongBao.Dock = DockStyle.Bottom;
        lblThongBao.Location = new Point(0, 536);
        lblThongBao.Name = "lblThongBao";
        lblThongBao.Padding = new Padding(8, 0, 0, 0);
        lblThongBao.Size = new Size(860, 24);
        lblThongBao.Text = "Sẵn sàng";
        lblThongBao.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // errorProvider1
        // 
        errorProvider1.ContainerControl = this;
        // 
        // FrmProduct
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(860, 560);
        Controls.Add(dgvProducts);
        Controls.Add(lblThongBao);
        Controls.Add(pnlTool);
        Controls.Add(grbThongTin);
        Name = "FrmProduct";
        Text = "Quản lý sản phẩm";
        Load += FrmProduct_Load;
        grbThongTin.ResumeLayout(false);
        grbThongTin.PerformLayout();
        pnlTool.ResumeLayout(false);
        pnlTool.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
        ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private GroupBox grbThongTin = null!;
    private Label lblMaSP = null!;
    private TextBox txtMaSP = null!;
    private Label lblTenSP = null!;
    private TextBox txtTenSP = null!;
    private Label lblLoaiSP = null!;
    private ComboBox cboLoaiSP = null!;
    private Label lblDonGia = null!;
    private TextBox txtDonGia = null!;
    private Label lblSoLuong = null!;
    private TextBox txtSoLuong = null!;
    private Panel pnlTool = null!;
    private Button btnThem = null!;
    private Button btnSua = null!;
    private Button btnXoa = null!;
    private Button btnLamMoi = null!;
    private TextBox txtTimKiem = null!;
    private Button btnTimKiem = null!;
    private DataGridView dgvProducts = null!;
    private Label lblThongBao = null!;
    private ErrorProvider errorProvider1 = null!;
}
