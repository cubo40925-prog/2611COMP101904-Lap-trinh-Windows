namespace ShopManagerMini.Forms;

partial class FrmCustomer
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
        lblMaKH = new Label();
        txtMaKH = new TextBox();
        lblHoTen = new Label();
        txtHoTen = new TextBox();
        lblSoDienThoai = new Label();
        txtSoDienThoai = new TextBox();
        lblLoaiKH = new Label();
        cboLoaiKH = new ComboBox();
        pnlTool = new Panel();
        btnThem = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnLamMoi = new Button();
        txtTimKiem = new TextBox();
        btnTimKiem = new Button();
        dgvCustomers = new DataGridView();
        lblThongBao = new Label();
        errorProvider1 = new ErrorProvider(components);
        grbThongTin.SuspendLayout();
        pnlTool.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
        ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
        SuspendLayout();
        // 
        // grbThongTin
        // 
        grbThongTin.Controls.Add(lblMaKH);
        grbThongTin.Controls.Add(txtMaKH);
        grbThongTin.Controls.Add(lblHoTen);
        grbThongTin.Controls.Add(txtHoTen);
        grbThongTin.Controls.Add(lblSoDienThoai);
        grbThongTin.Controls.Add(txtSoDienThoai);
        grbThongTin.Controls.Add(lblLoaiKH);
        grbThongTin.Controls.Add(cboLoaiKH);
        grbThongTin.Dock = DockStyle.Top;
        grbThongTin.Location = new Point(0, 0);
        grbThongTin.Name = "grbThongTin";
        grbThongTin.Size = new Size(860, 110);
        grbThongTin.TabIndex = 0;
        grbThongTin.TabStop = false;
        grbThongTin.Text = "Thông tin khách hàng";
        // 
        // lblMaKH
        // 
        lblMaKH.AutoSize = true;
        lblMaKH.Location = new Point(20, 33);
        lblMaKH.Name = "lblMaKH";
        lblMaKH.Text = "Mã KH:";
        // 
        // txtMaKH
        // 
        txtMaKH.Location = new Point(120, 30);
        txtMaKH.Name = "txtMaKH";
        txtMaKH.Size = new Size(180, 23);
        txtMaKH.TabIndex = 0;
        // 
        // lblHoTen
        // 
        lblHoTen.AutoSize = true;
        lblHoTen.Location = new Point(340, 33);
        lblHoTen.Name = "lblHoTen";
        lblHoTen.Text = "Họ tên:";
        // 
        // txtHoTen
        // 
        txtHoTen.Location = new Point(420, 30);
        txtHoTen.Name = "txtHoTen";
        txtHoTen.Size = new Size(260, 23);
        txtHoTen.TabIndex = 1;
        // 
        // lblSoDienThoai
        // 
        lblSoDienThoai.AutoSize = true;
        lblSoDienThoai.Location = new Point(20, 68);
        lblSoDienThoai.Name = "lblSoDienThoai";
        lblSoDienThoai.Text = "Số điện thoại:";
        // 
        // txtSoDienThoai
        // 
        txtSoDienThoai.Location = new Point(120, 65);
        txtSoDienThoai.Name = "txtSoDienThoai";
        txtSoDienThoai.Size = new Size(180, 23);
        txtSoDienThoai.TabIndex = 2;
        // 
        // lblLoaiKH
        // 
        lblLoaiKH.AutoSize = true;
        lblLoaiKH.Location = new Point(340, 68);
        lblLoaiKH.Name = "lblLoaiKH";
        lblLoaiKH.Text = "Loại KH:";
        // 
        // cboLoaiKH
        // 
        cboLoaiKH.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLoaiKH.Location = new Point(420, 65);
        cboLoaiKH.Name = "cboLoaiKH";
        cboLoaiKH.Size = new Size(260, 23);
        cboLoaiKH.TabIndex = 3;
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
        pnlTool.Location = new Point(0, 110);
        pnlTool.Name = "pnlTool";
        pnlTool.Size = new Size(860, 46);
        pnlTool.TabIndex = 1;
        // 
        // btnThem
        // 
        btnThem.Location = new Point(10, 7);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(90, 32);
        btnThem.TabIndex = 4;
        btnThem.Text = "Thêm";
        btnThem.UseVisualStyleBackColor = true;
        btnThem.Click += btnThem_Click;
        // 
        // btnSua
        // 
        btnSua.Location = new Point(106, 7);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(90, 32);
        btnSua.TabIndex = 5;
        btnSua.Text = "Sửa";
        btnSua.UseVisualStyleBackColor = true;
        btnSua.Click += btnSua_Click;
        // 
        // btnXoa
        // 
        btnXoa.Location = new Point(202, 7);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(90, 32);
        btnXoa.TabIndex = 6;
        btnXoa.Text = "Xóa";
        btnXoa.UseVisualStyleBackColor = true;
        btnXoa.Click += btnXoa_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.Location = new Point(298, 7);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(90, 32);
        btnLamMoi.TabIndex = 7;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = true;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // txtTimKiem
        // 
        txtTimKiem.Location = new Point(470, 12);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Nhập mã, tên hoặc số điện thoại...";
        txtTimKiem.Size = new Size(230, 23);
        txtTimKiem.TabIndex = 8;
        txtTimKiem.KeyDown += txtTimKiem_KeyDown;
        // 
        // btnTimKiem
        // 
        btnTimKiem.Location = new Point(706, 7);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(90, 32);
        btnTimKiem.TabIndex = 9;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = true;
        btnTimKiem.Click += btnTimKiem_Click;
        // 
        // dgvCustomers
        // 
        dgvCustomers.BackgroundColor = SystemColors.Window;
        dgvCustomers.Dock = DockStyle.Fill;
        dgvCustomers.Location = new Point(0, 156);
        dgvCustomers.Name = "dgvCustomers";
        dgvCustomers.RowHeadersVisible = false;
        dgvCustomers.Size = new Size(860, 380);
        dgvCustomers.TabIndex = 10;
        dgvCustomers.SelectionChanged += dgvCustomers_SelectionChanged;
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
        // FrmCustomer
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(860, 560);
        Controls.Add(dgvCustomers);
        Controls.Add(lblThongBao);
        Controls.Add(pnlTool);
        Controls.Add(grbThongTin);
        Name = "FrmCustomer";
        Text = "Quản lý khách hàng";
        Load += FrmCustomer_Load;
        grbThongTin.ResumeLayout(false);
        grbThongTin.PerformLayout();
        pnlTool.ResumeLayout(false);
        pnlTool.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
        ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private GroupBox grbThongTin = null!;
    private Label lblMaKH = null!;
    private TextBox txtMaKH = null!;
    private Label lblHoTen = null!;
    private TextBox txtHoTen = null!;
    private Label lblSoDienThoai = null!;
    private TextBox txtSoDienThoai = null!;
    private Label lblLoaiKH = null!;
    private ComboBox cboLoaiKH = null!;
    private Panel pnlTool = null!;
    private Button btnThem = null!;
    private Button btnSua = null!;
    private Button btnXoa = null!;
    private Button btnLamMoi = null!;
    private TextBox txtTimKiem = null!;
    private Button btnTimKiem = null!;
    private DataGridView dgvCustomers = null!;
    private Label lblThongBao = null!;
    private ErrorProvider errorProvider1 = null!;
}
