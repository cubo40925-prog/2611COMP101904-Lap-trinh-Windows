namespace COMP1019_Buoi11_Lab11;

partial class FrmLop
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
        grbLop = new GroupBox();
        lblMaLop = new Label();
        txtMaLop = new TextBox();
        lblTenLop = new Label();
        txtTenLop = new TextBox();
        btnThem = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnLamMoi = new Button();
        lblKhoa = new Label();
        cboKhoa = new ComboBox();
        lblTimKiem = new Label();
        txtTimKiem = new TextBox();
        btnTimKiem = new Button();
        dgvLop = new DataGridView();
        statusStrip1 = new StatusStrip();
        lblStatus = new ToolStripStatusLabel();
        lblCount = new ToolStripStatusLabel();
        grbLop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvLop).BeginInit();
        statusStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // grbLop
        // 
        grbLop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grbLop.Controls.Add(lblMaLop);
        grbLop.Controls.Add(txtMaLop);
        grbLop.Controls.Add(lblTenLop);
        grbLop.Controls.Add(txtTenLop);
        grbLop.Controls.Add(btnThem);
        grbLop.Controls.Add(btnSua);
        grbLop.Controls.Add(btnXoa);
        grbLop.Controls.Add(btnLamMoi);
        grbLop.Controls.Add(lblKhoa);
        grbLop.Controls.Add(cboKhoa);
        grbLop.Location = new Point(12, 12);
        grbLop.Name = "grbLop";
        grbLop.Size = new Size(776, 125);
        grbLop.TabIndex = 0;
        grbLop.TabStop = false;
        grbLop.Text = "Thông tin lớp học";
        // 
        // lblMaLop
        // 
        lblMaLop.AutoSize = true;
        lblMaLop.Location = new Point(20, 33);
        lblMaLop.Name = "lblMaLop";
        lblMaLop.Text = "Mã lớp:";
        // 
        // txtMaLop
        // 
        txtMaLop.Location = new Point(90, 30);
        txtMaLop.MaxLength = 20;
        txtMaLop.Name = "txtMaLop";
        txtMaLop.Size = new Size(200, 23);
        txtMaLop.TabIndex = 0;
        // 
        // lblTenLop
        // 
        lblTenLop.AutoSize = true;
        lblTenLop.Location = new Point(320, 33);
        lblTenLop.Name = "lblTenLop";
        lblTenLop.Text = "Tên lớp:";
        // 
        // txtTenLop
        // 
        txtTenLop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTenLop.Location = new Point(390, 30);
        txtTenLop.MaxLength = 100;
        txtTenLop.Name = "txtTenLop";
        txtTenLop.Size = new Size(365, 23);
        txtTenLop.TabIndex = 1;
        // 
        // btnThem
        // 
        btnThem.Location = new Point(20, 75);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(100, 34);
        btnThem.TabIndex = 2;
        btnThem.Text = "Thêm";
        btnThem.UseVisualStyleBackColor = true;
        btnThem.Click += btnThem_Click;
        // 
        // btnSua
        // 
        btnSua.Location = new Point(130, 75);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(100, 34);
        btnSua.TabIndex = 3;
        btnSua.Text = "Sửa";
        btnSua.UseVisualStyleBackColor = true;
        btnSua.Click += btnSua_Click;
        // 
        // btnXoa
        // 
        btnXoa.Location = new Point(240, 75);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(100, 34);
        btnXoa.TabIndex = 4;
        btnXoa.Text = "Xóa";
        btnXoa.UseVisualStyleBackColor = true;
        btnXoa.Click += btnXoa_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.Location = new Point(350, 75);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(100, 34);
        btnLamMoi.TabIndex = 5;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = true;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // lblKhoa
        // 
        lblKhoa.AutoSize = true;
        lblKhoa.Location = new Point(480, 83);
        lblKhoa.Name = "lblKhoa";
        lblKhoa.Text = "Khoa:";
        lblKhoa.Visible = false;
        // 
        // cboKhoa
        // 
        cboKhoa.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
        cboKhoa.Location = new Point(530, 80);
        cboKhoa.Name = "cboKhoa";
        cboKhoa.Size = new Size(225, 23);
        cboKhoa.TabIndex = 6;
        cboKhoa.Visible = false;
        // 
        // lblTimKiem
        // 
        lblTimKiem.AutoSize = true;
        lblTimKiem.Location = new Point(14, 156);
        lblTimKiem.Name = "lblTimKiem";
        lblTimKiem.Text = "Tìm kiếm:";
        // 
        // txtTimKiem
        // 
        txtTimKiem.Location = new Point(80, 153);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Nhập mã hoặc tên lớp rồi nhấn Enter";
        txtTimKiem.Size = new Size(300, 23);
        txtTimKiem.TabIndex = 7;
        txtTimKiem.KeyDown += txtTimKiem_KeyDown;
        // 
        // btnTimKiem
        // 
        btnTimKiem.Location = new Point(390, 150);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(100, 29);
        btnTimKiem.TabIndex = 8;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = true;
        btnTimKiem.Click += btnTimKiem_Click;
        // 
        // dgvLop
        // 
        dgvLop.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvLop.BackgroundColor = SystemColors.Window;
        dgvLop.Location = new Point(12, 190);
        dgvLop.Name = "dgvLop";
        dgvLop.RowHeadersVisible = false;
        dgvLop.Size = new Size(776, 316);
        dgvLop.TabIndex = 9;
        dgvLop.SelectionChanged += dgvLop_SelectionChanged;
        // 
        // statusStrip1
        // 
        statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, lblCount });
        statusStrip1.Location = new Point(0, 518);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(800, 22);
        statusStrip1.TabIndex = 10;
        // 
        // lblStatus
        // 
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(600, 17);
        lblStatus.Spring = true;
        lblStatus.Text = "Sẵn sàng";
        lblStatus.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblCount
        // 
        lblCount.BorderSides = ToolStripStatusLabelBorderSides.Left;
        lblCount.Name = "lblCount";
        lblCount.Size = new Size(150, 17);
        lblCount.Text = "Số lớp hiển thị: 0";
        // 
        // FrmLop
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 540);
        Controls.Add(statusStrip1);
        Controls.Add(dgvLop);
        Controls.Add(btnTimKiem);
        Controls.Add(txtTimKiem);
        Controls.Add(lblTimKiem);
        Controls.Add(grbLop);
        MinimumSize = new Size(780, 520);
        Name = "FrmLop";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quản lý lớp học - EduManagerDB";
        Load += FrmLop_Load;
        grbLop.ResumeLayout(false);
        grbLop.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvLop).EndInit();
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private GroupBox grbLop = null!;
    private Label lblMaLop = null!;
    private TextBox txtMaLop = null!;
    private Label lblTenLop = null!;
    private TextBox txtTenLop = null!;
    private Button btnThem = null!;
    private Button btnSua = null!;
    private Button btnXoa = null!;
    private Button btnLamMoi = null!;
    private Label lblKhoa = null!;
    private ComboBox cboKhoa = null!;
    private Label lblTimKiem = null!;
    private TextBox txtTimKiem = null!;
    private Button btnTimKiem = null!;
    private DataGridView dgvLop = null!;
    private StatusStrip statusStrip1 = null!;
    private ToolStripStatusLabel lblStatus = null!;
    private ToolStripStatusLabel lblCount = null!;
}
