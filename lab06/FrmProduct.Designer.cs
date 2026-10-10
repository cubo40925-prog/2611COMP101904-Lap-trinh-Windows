namespace ProductManager;

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
        menuStrip1 = new MenuStrip();
        mnuFile = new ToolStripMenuItem();
        mnuFileLamMoi = new ToolStripMenuItem();
        mnuFileXuat = new ToolStripMenuItem();
        toolStripSeparator1 = new ToolStripSeparator();
        mnuFileThoat = new ToolStripMenuItem();
        mnuQuanLy = new ToolStripMenuItem();
        mnuThem = new ToolStripMenuItem();
        mnuSua = new ToolStripMenuItem();
        mnuXoa = new ToolStripMenuItem();
        mnuTimKiem = new ToolStripMenuItem();
        toolStripSeparator2 = new ToolStripSeparator();
        mnuSapXepGia = new ToolStripMenuItem();
        mnuSapXepSoLuong = new ToolStripMenuItem();
        mnuTroGiup = new ToolStripMenuItem();
        mnuGioiThieu = new ToolStripMenuItem();
        toolStrip1 = new ToolStrip();
        tsbThem = new ToolStripButton();
        tsbSua = new ToolStripButton();
        tsbXoa = new ToolStripButton();
        tsbLamMoi = new ToolStripButton();
        toolStripSeparator3 = new ToolStripSeparator();
        tstTimKiem = new ToolStripTextBox();
        tsbTimKiem = new ToolStripButton();
        statusStrip1 = new StatusStrip();
        lblStatus = new ToolStripStatusLabel();
        lblCount = new ToolStripStatusLabel();
        lblTongGiaTri = new ToolStripStatusLabel();
        grbThongTin = new GroupBox();
        lblMaSP = new Label();
        txtMaSP = new TextBox();
        lblTenSP = new Label();
        txtTenSP = new TextBox();
        lblLoaiSP = new Label();
        cboLoaiSP = new ComboBox();
        btnThemLoai = new Button();
        lblNgayNhap = new Label();
        dtpNgayNhap = new DateTimePicker();
        lblDonGia = new Label();
        numDonGia = new NumericUpDown();
        chkConKinhDoanh = new CheckBox();
        lblSoLuong = new Label();
        numSoLuong = new NumericUpDown();
        btnThem = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnLamMoi = new Button();
        picSanPham = new PictureBox();
        btnChonAnh = new Button();
        dgvProducts = new DataGridView();
        cmsDgv = new ContextMenuStrip(components);
        cmsSua = new ToolStripMenuItem();
        cmsXoa = new ToolStripMenuItem();
        cmsChiTiet = new ToolStripMenuItem();
        errorProvider1 = new ErrorProvider(components);
        menuStrip1.SuspendLayout();
        toolStrip1.SuspendLayout();
        statusStrip1.SuspendLayout();
        grbThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numDonGia).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numSoLuong).BeginInit();
        ((System.ComponentModel.ISupportInitialize)picSanPham).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
        cmsDgv.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { mnuFile, mnuQuanLy, mnuTroGiup });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(1000, 24);
        menuStrip1.TabIndex = 0;
        // 
        // mnuFile
        // 
        mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuFileLamMoi, mnuFileXuat, toolStripSeparator1, mnuFileThoat });
        mnuFile.Name = "mnuFile";
        mnuFile.Text = "File";
        // 
        // mnuFileLamMoi
        // 
        mnuFileLamMoi.Name = "mnuFileLamMoi";
        mnuFileLamMoi.ShortcutKeys = Keys.F5;
        mnuFileLamMoi.Text = "Làm mới";
        mnuFileLamMoi.Click += mnuFileLamMoi_Click;
        // 
        // mnuFileXuat
        // 
        mnuFileXuat.Name = "mnuFileXuat";
        mnuFileXuat.ShortcutKeys = Keys.Control | Keys.S;
        mnuFileXuat.Text = "Xuất danh sách ra file .txt...";
        mnuFileXuat.Click += mnuFileXuat_Click;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        // 
        // mnuFileThoat
        // 
        mnuFileThoat.Name = "mnuFileThoat";
        mnuFileThoat.ShortcutKeys = Keys.Alt | Keys.F4;
        mnuFileThoat.Text = "Thoát";
        mnuFileThoat.Click += mnuFileThoat_Click;
        // 
        // mnuQuanLy
        // 
        mnuQuanLy.DropDownItems.AddRange(new ToolStripItem[] { mnuThem, mnuSua, mnuXoa, mnuTimKiem, toolStripSeparator2, mnuSapXepGia, mnuSapXepSoLuong });
        mnuQuanLy.Name = "mnuQuanLy";
        mnuQuanLy.Text = "Quản lý";
        // 
        // mnuThem
        // 
        mnuThem.Name = "mnuThem";
        mnuThem.ShortcutKeys = Keys.Control | Keys.N;
        mnuThem.Text = "Thêm sản phẩm";
        mnuThem.Click += mnuThem_Click;
        // 
        // mnuSua
        // 
        mnuSua.Name = "mnuSua";
        mnuSua.ShortcutKeys = Keys.Control | Keys.E;
        mnuSua.Text = "Sửa sản phẩm";
        mnuSua.Click += mnuSua_Click;
        // 
        // mnuXoa
        // 
        mnuXoa.Name = "mnuXoa";
        mnuXoa.ShortcutKeys = Keys.Control | Keys.D;
        mnuXoa.Text = "Xóa sản phẩm";
        mnuXoa.Click += mnuXoa_Click;
        // 
        // mnuTimKiem
        // 
        mnuTimKiem.Name = "mnuTimKiem";
        mnuTimKiem.ShortcutKeys = Keys.Control | Keys.F;
        mnuTimKiem.Text = "Tìm kiếm";
        mnuTimKiem.Click += mnuTimKiem_Click;
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        // 
        // mnuSapXepGia
        // 
        mnuSapXepGia.Name = "mnuSapXepGia";
        mnuSapXepGia.Text = "Sắp xếp theo đơn giá (đảo chiều mỗi lần nhấn)";
        mnuSapXepGia.Click += mnuSapXepGia_Click;
        // 
        // mnuSapXepSoLuong
        // 
        mnuSapXepSoLuong.Name = "mnuSapXepSoLuong";
        mnuSapXepSoLuong.Text = "Sắp xếp theo số lượng (đảo chiều mỗi lần nhấn)";
        mnuSapXepSoLuong.Click += mnuSapXepSoLuong_Click;
        // 
        // mnuTroGiup
        // 
        mnuTroGiup.DropDownItems.AddRange(new ToolStripItem[] { mnuGioiThieu });
        mnuTroGiup.Name = "mnuTroGiup";
        mnuTroGiup.Text = "Trợ giúp";
        // 
        // mnuGioiThieu
        // 
        mnuGioiThieu.Name = "mnuGioiThieu";
        mnuGioiThieu.ShortcutKeys = Keys.F1;
        mnuGioiThieu.Text = "Giới thiệu";
        mnuGioiThieu.Click += mnuGioiThieu_Click;
        // 
        // toolStrip1
        // 
        toolStrip1.Items.AddRange(new ToolStripItem[] { tstTimKiem, tsbTimKiem });
        toolStrip1.Location = new Point(0, 24);
        toolStrip1.Name = "toolStrip1";
        toolStrip1.Size = new Size(1000, 27);
        toolStrip1.TabIndex = 1;
        // 
        // tsbThem
        // 
        tsbThem.DisplayStyle = ToolStripItemDisplayStyle.Text;
        tsbThem.Name = "tsbThem";
        tsbThem.Text = "Thêm";
        tsbThem.ToolTipText = "Thêm sản phẩm (Ctrl+N)";
        tsbThem.Click += tsbThem_Click;
        // 
        // tsbSua
        // 
        tsbSua.DisplayStyle = ToolStripItemDisplayStyle.Text;
        tsbSua.Name = "tsbSua";
        tsbSua.Text = "Sửa";
        tsbSua.ToolTipText = "Sửa sản phẩm (Ctrl+E)";
        tsbSua.Click += tsbSua_Click;
        // 
        // tsbXoa
        // 
        tsbXoa.DisplayStyle = ToolStripItemDisplayStyle.Text;
        tsbXoa.Name = "tsbXoa";
        tsbXoa.Text = "Xóa";
        tsbXoa.ToolTipText = "Xóa sản phẩm (Ctrl+D hoặc phím Delete trên lưới)";
        tsbXoa.Click += tsbXoa_Click;
        // 
        // tsbLamMoi
        // 
        tsbLamMoi.DisplayStyle = ToolStripItemDisplayStyle.Text;
        tsbLamMoi.Name = "tsbLamMoi";
        tsbLamMoi.Text = "Làm mới";
        tsbLamMoi.ToolTipText = "Làm mới (F5)";
        tsbLamMoi.Click += tsbLamMoi_Click;
        // 
        // toolStripSeparator3
        // 
        toolStripSeparator3.Name = "toolStripSeparator3";
        // 
        // tstTimKiem
        // 
        tstTimKiem.Name = "tstTimKiem";
        tstTimKiem.Size = new Size(220, 27);
        tstTimKiem.ToolTipText = "Nhập mã hoặc tên sản phẩm rồi nhấn Enter";
        tstTimKiem.KeyDown += tstTimKiem_KeyDown;
        // 
        // tsbTimKiem
        // 
        tsbTimKiem.DisplayStyle = ToolStripItemDisplayStyle.Text;
        tsbTimKiem.Name = "tsbTimKiem";
        tsbTimKiem.Text = "Tìm kiếm";
        tsbTimKiem.ToolTipText = "Tìm theo mã hoặc tên (Ctrl+F)";
        tsbTimKiem.Click += tsbTimKiem_Click;
        // 
        // statusStrip1
        // 
        statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, lblCount, lblTongGiaTri });
        statusStrip1.Location = new Point(0, 658);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(1000, 22);
        statusStrip1.TabIndex = 2;
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
        lblCount.Size = new Size(100, 17);
        lblCount.Text = "Số sản phẩm: 0";
        // 
        // lblTongGiaTri
        // 
        lblTongGiaTri.BorderSides = ToolStripStatusLabelBorderSides.Left;
        lblTongGiaTri.Name = "lblTongGiaTri";
        lblTongGiaTri.Size = new Size(200, 17);
        lblTongGiaTri.Text = "Tổng giá trị tồn kho: 0 đ";
        // 
        // grbThongTin
        // 
        grbThongTin.Controls.Add(lblMaSP);
        grbThongTin.Controls.Add(txtMaSP);
        grbThongTin.Controls.Add(lblTenSP);
        grbThongTin.Controls.Add(txtTenSP);
        grbThongTin.Controls.Add(lblLoaiSP);
        grbThongTin.Controls.Add(cboLoaiSP);
        grbThongTin.Controls.Add(btnThemLoai);
        grbThongTin.Controls.Add(lblNgayNhap);
        grbThongTin.Controls.Add(dtpNgayNhap);
        grbThongTin.Controls.Add(lblDonGia);
        grbThongTin.Controls.Add(numDonGia);
        grbThongTin.Controls.Add(chkConKinhDoanh);
        grbThongTin.Controls.Add(lblSoLuong);
        grbThongTin.Controls.Add(numSoLuong);
        grbThongTin.Controls.Add(btnThem);
        grbThongTin.Controls.Add(btnSua);
        grbThongTin.Controls.Add(btnXoa);
        grbThongTin.Controls.Add(btnLamMoi);
        grbThongTin.Controls.Add(picSanPham);
        grbThongTin.Controls.Add(btnChonAnh);
        grbThongTin.Dock = DockStyle.Top;
        grbThongTin.Location = new Point(0, 51);
        grbThongTin.Name = "grbThongTin";
        grbThongTin.Padding = new Padding(10);
        grbThongTin.Size = new Size(1000, 235);
        grbThongTin.TabIndex = 3;
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
        txtMaSP.Location = new Point(120, 30);
        txtMaSP.Name = "txtMaSP";
        txtMaSP.Size = new Size(200, 23);
        txtMaSP.TabIndex = 0;
        // 
        // lblTenSP
        // 
        lblTenSP.AutoSize = true;
        lblTenSP.Location = new Point(350, 33);
        lblTenSP.Name = "lblTenSP";
        lblTenSP.Text = "Tên SP:";
        // 
        // txtTenSP
        // 
        txtTenSP.Location = new Point(430, 30);
        txtTenSP.Name = "txtTenSP";
        txtTenSP.Size = new Size(230, 23);
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
        cboLoaiSP.Location = new Point(120, 65);
        cboLoaiSP.Name = "cboLoaiSP";
        cboLoaiSP.Size = new Size(165, 23);
        cboLoaiSP.TabIndex = 2;
        // 
        // btnThemLoai
        // 
        btnThemLoai.Location = new Point(290, 64);
        btnThemLoai.Name = "btnThemLoai";
        btnThemLoai.Size = new Size(30, 25);
        btnThemLoai.TabIndex = 13;
        btnThemLoai.Text = "+";
        btnThemLoai.UseVisualStyleBackColor = true;
        btnThemLoai.Click += btnThemLoai_Click;
        // 
        // lblNgayNhap
        // 
        lblNgayNhap.AutoSize = true;
        lblNgayNhap.Location = new Point(350, 68);
        lblNgayNhap.Name = "lblNgayNhap";
        lblNgayNhap.Text = "Ngày nhập:";
        // 
        // dtpNgayNhap
        // 
        dtpNgayNhap.Format = DateTimePickerFormat.Short;
        dtpNgayNhap.Location = new Point(430, 65);
        dtpNgayNhap.Name = "dtpNgayNhap";
        dtpNgayNhap.Size = new Size(230, 23);
        dtpNgayNhap.TabIndex = 3;
        // 
        // lblDonGia
        // 
        lblDonGia.AutoSize = true;
        lblDonGia.Location = new Point(20, 103);
        lblDonGia.Name = "lblDonGia";
        lblDonGia.Text = "Đơn giá:";
        // 
        // numDonGia
        // 
        numDonGia.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
        numDonGia.Location = new Point(120, 100);
        numDonGia.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        numDonGia.Name = "numDonGia";
        numDonGia.Size = new Size(200, 23);
        numDonGia.TabIndex = 4;
        numDonGia.TextAlign = HorizontalAlignment.Right;
        numDonGia.ThousandsSeparator = true;
        // 
        // chkConKinhDoanh
        // 
        chkConKinhDoanh.AutoSize = true;
        chkConKinhDoanh.Checked = true;
        chkConKinhDoanh.CheckState = CheckState.Checked;
        chkConKinhDoanh.Location = new Point(430, 102);
        chkConKinhDoanh.Name = "chkConKinhDoanh";
        chkConKinhDoanh.TabIndex = 5;
        chkConKinhDoanh.Text = "Còn kinh doanh";
        chkConKinhDoanh.UseVisualStyleBackColor = true;
        // 
        // lblSoLuong
        // 
        lblSoLuong.AutoSize = true;
        lblSoLuong.Location = new Point(20, 138);
        lblSoLuong.Name = "lblSoLuong";
        lblSoLuong.Text = "Số lượng:";
        // 
        // numSoLuong
        // 
        numSoLuong.Location = new Point(120, 135);
        numSoLuong.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        numSoLuong.Name = "numSoLuong";
        numSoLuong.Size = new Size(200, 23);
        numSoLuong.TabIndex = 6;
        numSoLuong.TextAlign = HorizontalAlignment.Right;
        // 
        // btnThem
        // 
        btnThem.Location = new Point(20, 182);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(110, 34);
        btnThem.TabIndex = 7;
        btnThem.Text = "Thêm";
        btnThem.UseVisualStyleBackColor = true;
        btnThem.Click += btnThem_Click;
        // 
        // btnSua
        // 
        btnSua.Location = new Point(140, 182);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(110, 34);
        btnSua.TabIndex = 8;
        btnSua.Text = "Sửa";
        btnSua.UseVisualStyleBackColor = true;
        btnSua.Click += btnSua_Click;
        // 
        // btnXoa
        // 
        btnXoa.Location = new Point(260, 182);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(110, 34);
        btnXoa.TabIndex = 9;
        btnXoa.Text = "Xóa";
        btnXoa.UseVisualStyleBackColor = true;
        btnXoa.Click += btnXoa_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.Location = new Point(380, 182);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(110, 34);
        btnLamMoi.TabIndex = 10;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = true;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // picSanPham
        // 
        picSanPham.BackColor = Color.WhiteSmoke;
        picSanPham.BorderStyle = BorderStyle.FixedSingle;
        picSanPham.Location = new Point(720, 25);
        picSanPham.Name = "picSanPham";
        picSanPham.Size = new Size(160, 150);
        picSanPham.SizeMode = PictureBoxSizeMode.Zoom;
        picSanPham.TabIndex = 11;
        picSanPham.TabStop = false;
        // 
        // btnChonAnh
        // 
        btnChonAnh.Location = new Point(720, 182);
        btnChonAnh.Name = "btnChonAnh";
        btnChonAnh.Size = new Size(160, 34);
        btnChonAnh.TabIndex = 12;
        btnChonAnh.Text = "Chọn ảnh";
        btnChonAnh.UseVisualStyleBackColor = true;
        btnChonAnh.Click += btnChonAnh_Click;
        // 
        // dgvProducts
        // 
        dgvProducts.BackgroundColor = SystemColors.Window;
        dgvProducts.ContextMenuStrip = cmsDgv;
        dgvProducts.Dock = DockStyle.Fill;
        dgvProducts.Location = new Point(0, 286);
        dgvProducts.Name = "dgvProducts";
        dgvProducts.RowHeadersVisible = false;
        dgvProducts.Size = new Size(1000, 372);
        dgvProducts.TabIndex = 4;
        dgvProducts.CellDoubleClick += dgvProducts_CellDoubleClick;
        dgvProducts.CellMouseDown += dgvProducts_CellMouseDown;
        dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
        dgvProducts.KeyDown += dgvProducts_KeyDown;
        // 
        // cmsDgv
        // 
        cmsDgv.Items.AddRange(new ToolStripItem[] { cmsSua, cmsXoa, cmsChiTiet });
        cmsDgv.Name = "cmsDgv";
        cmsDgv.Opening += cmsDgv_Opening;
        // 
        // cmsSua
        // 
        cmsSua.Name = "cmsSua";
        cmsSua.Text = "Sửa";
        cmsSua.Click += cmsSua_Click;
        // 
        // cmsXoa
        // 
        cmsXoa.Name = "cmsXoa";
        cmsXoa.Text = "Xóa";
        cmsXoa.Click += cmsXoa_Click;
        // 
        // cmsChiTiet
        // 
        cmsChiTiet.Name = "cmsChiTiet";
        cmsChiTiet.Text = "Xem chi tiết";
        cmsChiTiet.Click += cmsChiTiet_Click;
        // 
        // errorProvider1
        // 
        errorProvider1.ContainerControl = this;
        // 
        // FrmProduct
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1000, 680);
        Controls.Add(dgvProducts);
        Controls.Add(grbThongTin);
        Controls.Add(toolStrip1);
        Controls.Add(statusStrip1);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        MinimumSize = new Size(940, 600);
        Name = "FrmProduct";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quản lý sản phẩm";
        Load += FrmProduct_Load;
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        grbThongTin.ResumeLayout(false);
        grbThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numDonGia).EndInit();
        ((System.ComponentModel.ISupportInitialize)numSoLuong).EndInit();
        ((System.ComponentModel.ISupportInitialize)picSanPham).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
        cmsDgv.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1 = null!;
    private ToolStripMenuItem mnuFile = null!;
    private ToolStripMenuItem mnuFileLamMoi = null!;
    private ToolStripMenuItem mnuFileXuat = null!;
    private ToolStripSeparator toolStripSeparator1 = null!;
    private ToolStripMenuItem mnuFileThoat = null!;
    private ToolStripMenuItem mnuQuanLy = null!;
    private ToolStripMenuItem mnuThem = null!;
    private ToolStripMenuItem mnuSua = null!;
    private ToolStripMenuItem mnuXoa = null!;
    private ToolStripMenuItem mnuTimKiem = null!;
    private ToolStripSeparator toolStripSeparator2 = null!;
    private ToolStripMenuItem mnuSapXepGia = null!;
    private ToolStripMenuItem mnuSapXepSoLuong = null!;
    private ToolStripMenuItem mnuTroGiup = null!;
    private ToolStripMenuItem mnuGioiThieu = null!;
    private ToolStrip toolStrip1 = null!;
    private ToolStripButton tsbThem = null!;
    private ToolStripButton tsbSua = null!;
    private ToolStripButton tsbXoa = null!;
    private ToolStripButton tsbLamMoi = null!;
    private ToolStripSeparator toolStripSeparator3 = null!;
    private ToolStripTextBox tstTimKiem = null!;
    private ToolStripButton tsbTimKiem = null!;
    private StatusStrip statusStrip1 = null!;
    private ToolStripStatusLabel lblStatus = null!;
    private ToolStripStatusLabel lblCount = null!;
    private ToolStripStatusLabel lblTongGiaTri = null!;
    private GroupBox grbThongTin = null!;
    private Label lblMaSP = null!;
    private TextBox txtMaSP = null!;
    private Label lblTenSP = null!;
    private TextBox txtTenSP = null!;
    private Label lblLoaiSP = null!;
    private ComboBox cboLoaiSP = null!;
    private Button btnThemLoai = null!;
    private Label lblNgayNhap = null!;
    private DateTimePicker dtpNgayNhap = null!;
    private Label lblDonGia = null!;
    private NumericUpDown numDonGia = null!;
    private CheckBox chkConKinhDoanh = null!;
    private Label lblSoLuong = null!;
    private NumericUpDown numSoLuong = null!;
    private Button btnThem = null!;
    private Button btnSua = null!;
    private Button btnXoa = null!;
    private Button btnLamMoi = null!;
    private PictureBox picSanPham = null!;
    private Button btnChonAnh = null!;
    private DataGridView dgvProducts = null!;
    private ContextMenuStrip cmsDgv = null!;
    private ToolStripMenuItem cmsSua = null!;
    private ToolStripMenuItem cmsXoa = null!;
    private ToolStripMenuItem cmsChiTiet = null!;
    private ErrorProvider errorProvider1 = null!;
}