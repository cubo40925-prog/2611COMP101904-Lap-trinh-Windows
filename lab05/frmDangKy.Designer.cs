namespace CourseRegistrationApp
{
    partial class frmDangKy
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTieuDe = new Label();
            grpHocVien = new GroupBox();
            lblTrangThaiEmail = new Label();
            chkNhanEmail = new CheckBox();
            dtpNgaySinh = new DateTimePicker();
            lblNgaySinh = new Label();
            txtSoDienThoai = new TextBox();
            lblSoDienThoai = new Label();
            lblDemKyTu = new Label();
            txtHoTen = new TextBox();
            lblHoTen = new Label();
            grpKhoaHoc = new GroupBox();
            lblTongTien = new Label();
            lblTongTienText = new Label();
            lblHocPhiThang = new Label();
            lblHocPhi = new Label();
            numSoThang = new NumericUpDown();
            lblSoThang = new Label();
            radOffline = new RadioButton();
            radOnline = new RadioButton();
            lblHinhThuc = new Label();
            cboKhoaHoc = new ComboBox();
            lblKhoaHoc = new Label();
            btnDangKy = new Button();
            btnLamMoi = new Button();
            btnThoat = new Button();
            grpHocVien.SuspendLayout();
            grpKhoaHoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSoThang).BeginInit();
            SuspendLayout();
            // lblTieuDe
            lblTieuDe.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(70, 130, 180);
            lblTieuDe.Location = new Point(0, 25);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(929, 60);
            lblTieuDe.Text = "ĐĂNG KÝ KHÓA HỌC";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // grpHocVien
            grpHocVien.Controls.Add(lblTrangThaiEmail);
            grpHocVien.Controls.Add(chkNhanEmail);
            grpHocVien.Controls.Add(dtpNgaySinh);
            grpHocVien.Controls.Add(lblNgaySinh);
            grpHocVien.Controls.Add(txtSoDienThoai);
            grpHocVien.Controls.Add(lblSoDienThoai);
            grpHocVien.Controls.Add(lblDemKyTu);
            grpHocVien.Controls.Add(txtHoTen);
            grpHocVien.Controls.Add(lblHoTen);
            grpHocVien.Font = new Font("Segoe UI", 11.25F);
            grpHocVien.ForeColor = Color.FromArgb(52, 152, 219);
            grpHocVien.Location = new Point(25, 113);
            grpHocVien.Name = "grpHocVien";
            grpHocVien.Size = new Size(434, 352);
            grpHocVien.TabIndex = 0;
            grpHocVien.TabStop = false;
            grpHocVien.Text = "Thông tin học viên";
            // lblHoTen
            lblHoTen.AutoSize = true;
            lblHoTen.ForeColor = Color.DimGray;
            lblHoTen.Location = new Point(44, 52);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Text = "Họ tên";
            // txtHoTen
            txtHoTen.ForeColor = Color.Black;
            txtHoTen.Location = new Point(114, 47);
            txtHoTen.MaxLength = 50;
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(228, 27);
            txtHoTen.TabIndex = 0;
            txtHoTen.TextChanged += txtHoTen_TextChanged;
            // lblDemKyTu
            lblDemKyTu.AutoSize = true;
            lblDemKyTu.ForeColor = Color.Silver;
            lblDemKyTu.Location = new Point(353, 52);
            lblDemKyTu.Name = "lblDemKyTu";
            lblDemKyTu.Text = "0/50";
            // lblSoDienThoai
            lblSoDienThoai.AutoSize = true;
            lblSoDienThoai.ForeColor = Color.DimGray;
            lblSoDienThoai.Location = new Point(66, 113);
            lblSoDienThoai.Name = "lblSoDienThoai";
            lblSoDienThoai.Text = "SĐT";
            // txtSoDienThoai
            txtSoDienThoai.ForeColor = Color.Black;
            txtSoDienThoai.Location = new Point(114, 108);
            txtSoDienThoai.Name = "txtSoDienThoai";
            txtSoDienThoai.Size = new Size(228, 27);
            txtSoDienThoai.TabIndex = 1;
            // lblNgaySinh
            lblNgaySinh.AutoSize = true;
            lblNgaySinh.ForeColor = Color.DimGray;
            lblNgaySinh.Location = new Point(20, 173);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Text = "Ngày sinh";
            // dtpNgaySinh
            dtpNgaySinh.CustomFormat = "dd- 'Thg'M -yy";
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.ForeColor = Color.Black;
            dtpNgaySinh.Location = new Point(114, 168);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(228, 27);
            dtpNgaySinh.TabIndex = 2;
            // chkNhanEmail
            chkNhanEmail.AutoSize = true;
            chkNhanEmail.ForeColor = Color.DimGray;
            chkNhanEmail.Location = new Point(114, 233);
            chkNhanEmail.Name = "chkNhanEmail";
            chkNhanEmail.TabIndex = 3;
            chkNhanEmail.Text = "Nhận email thông báo";
            chkNhanEmail.UseVisualStyleBackColor = true;
            chkNhanEmail.CheckedChanged += chkNhanEmail_CheckedChanged;
            // lblTrangThaiEmail
            lblTrangThaiEmail.AutoSize = true;
            lblTrangThaiEmail.ForeColor = Color.FromArgb(90, 80, 70);
            lblTrangThaiEmail.Location = new Point(118, 275);
            lblTrangThaiEmail.Name = "lblTrangThaiEmail";
            lblTrangThaiEmail.Text = "Không nhận email thông báo";
            // grpKhoaHoc
            grpKhoaHoc.Controls.Add(lblTongTien);
            grpKhoaHoc.Controls.Add(lblTongTienText);
            grpKhoaHoc.Controls.Add(lblHocPhiThang);
            grpKhoaHoc.Controls.Add(lblHocPhi);
            grpKhoaHoc.Controls.Add(numSoThang);
            grpKhoaHoc.Controls.Add(lblSoThang);
            grpKhoaHoc.Controls.Add(radOffline);
            grpKhoaHoc.Controls.Add(radOnline);
            grpKhoaHoc.Controls.Add(lblHinhThuc);
            grpKhoaHoc.Controls.Add(cboKhoaHoc);
            grpKhoaHoc.Controls.Add(lblKhoaHoc);
            grpKhoaHoc.Font = new Font("Segoe UI", 11.25F);
            grpKhoaHoc.ForeColor = Color.FromArgb(52, 152, 219);
            grpKhoaHoc.Location = new Point(477, 113);
            grpKhoaHoc.Name = "grpKhoaHoc";
            grpKhoaHoc.Size = new Size(428, 352);
            grpKhoaHoc.TabIndex = 1;
            grpKhoaHoc.TabStop = false;
            grpKhoaHoc.Text = "Thông tin khóa học";
            // lblKhoaHoc
            lblKhoaHoc.AutoSize = true;
            lblKhoaHoc.ForeColor = Color.DimGray;
            lblKhoaHoc.Location = new Point(42, 52);
            lblKhoaHoc.Name = "lblKhoaHoc";
            lblKhoaHoc.Text = "Khoá học";
            // cboKhoaHoc
            cboKhoaHoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhoaHoc.FlatStyle = FlatStyle.Flat;
            cboKhoaHoc.ForeColor = Color.Black;
            cboKhoaHoc.FormattingEnabled = true;
            cboKhoaHoc.Location = new Point(130, 47);
            cboKhoaHoc.Name = "cboKhoaHoc";
            cboKhoaHoc.Size = new Size(274, 28);
            cboKhoaHoc.TabIndex = 4;
            cboKhoaHoc.SelectedIndexChanged += cboKhoaHoc_SelectedIndexChanged;
            // lblHinhThuc
            lblHinhThuc.AutoSize = true;
            lblHinhThuc.ForeColor = Color.DimGray;
            lblHinhThuc.Location = new Point(37, 111);
            lblHinhThuc.Name = "lblHinhThuc";
            lblHinhThuc.Text = "Hình thức";
            // radOnline
            radOnline.AutoSize = true;
            radOnline.Checked = true;
            radOnline.ForeColor = Color.DimGray;
            radOnline.Location = new Point(130, 112);
            radOnline.Name = "radOnline";
            radOnline.TabIndex = 5;
            radOnline.TabStop = true;
            radOnline.Text = "Online";
            radOnline.UseVisualStyleBackColor = true;
            // radOffline
            radOffline.AutoSize = true;
            radOffline.ForeColor = Color.DimGray;
            radOffline.Location = new Point(261, 112);
            radOffline.Name = "radOffline";
            radOffline.TabIndex = 6;
            radOffline.Text = "Trực tiếp";
            radOffline.UseVisualStyleBackColor = true;
            // lblSoThang
            lblSoThang.AutoSize = true;
            lblSoThang.ForeColor = Color.DimGray;
            lblSoThang.Location = new Point(44, 173);
            lblSoThang.Name = "lblSoThang";
            lblSoThang.Text = "Số tháng";
            // numSoThang
            numSoThang.ForeColor = Color.Black;
            numSoThang.Location = new Point(130, 168);
            numSoThang.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            numSoThang.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numSoThang.Name = "numSoThang";
            numSoThang.Size = new Size(80, 27);
            numSoThang.TabIndex = 7;
            numSoThang.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numSoThang.ValueChanged += numSoThang_ValueChanged;
            // lblHocPhi
            lblHocPhi.AutoSize = true;
            lblHocPhi.ForeColor = Color.DimGray;
            lblHocPhi.Location = new Point(42, 233);
            lblHocPhi.Name = "lblHocPhi";
            lblHocPhi.Text = "Học phí /";
            // lblHocPhiThang
            lblHocPhiThang.AutoSize = true;
            lblHocPhiThang.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            lblHocPhiThang.ForeColor = Color.DimGray;
            lblHocPhiThang.Location = new Point(134, 233);
            lblHocPhiThang.Name = "lblHocPhiThang";
            lblHocPhiThang.Text = "800.000 VNĐ";
            // lblTongTienText
            lblTongTienText.AutoSize = true;
            lblTongTienText.ForeColor = Color.DimGray;
            lblTongTienText.Location = new Point(40, 288);
            lblTongTienText.Name = "lblTongTienText";
            lblTongTienText.Text = "Tổng tiền";
            // lblTongTien
            lblTongTien.AutoSize = true;
            lblTongTien.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.Firebrick;
            lblTongTien.Location = new Point(134, 288);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Text = "800.000 VNĐ";
            // btnDangKy
            btnDangKy.BackColor = Color.OliveDrab;
            btnDangKy.FlatAppearance.BorderSize = 0;
            btnDangKy.FlatStyle = FlatStyle.Flat;
            btnDangKy.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDangKy.ForeColor = Color.White;
            btnDangKy.Location = new Point(116, 533);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(183, 53);
            btnDangKy.TabIndex = 8;
            btnDangKy.Text = "Đăng ký";
            btnDangKy.UseVisualStyleBackColor = false;
            btnDangKy.Click += btnDangKy_Click;
            // btnLamMoi
            btnLamMoi.BackColor = Color.CornflowerBlue;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLamMoi.ForeColor = Color.White;
            btnLamMoi.Location = new Point(373, 533);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(183, 53);
            btnLamMoi.TabIndex = 9;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            btnLamMoi.Click += btnLamMoi_Click;
            // btnThoat
            btnThoat.BackColor = Color.IndianRed;
            btnThoat.FlatAppearance.BorderSize = 0;
            btnThoat.FlatStyle = FlatStyle.Flat;
            btnThoat.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnThoat.ForeColor = Color.White;
            btnThoat.Location = new Point(630, 533);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(183, 53);
            btnThoat.TabIndex = 10;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = false;
            btnThoat.Click += btnThoat_Click;
            // frmDangKy
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Honeydew;
            ClientSize = new Size(929, 634);
            Controls.Add(btnThoat);
            Controls.Add(btnLamMoi);
            Controls.Add(btnDangKy);
            Controls.Add(grpKhoaHoc);
            Controls.Add(grpHocVien);
            Controls.Add(lblTieuDe);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmDangKy";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ĐĂNG KÝ KHÓA HỌC";
            Load += frmDangKy_Load;
            grpHocVien.ResumeLayout(false);
            grpHocVien.PerformLayout();
            grpKhoaHoc.ResumeLayout(false);
            grpKhoaHoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSoThang).EndInit();
            ResumeLayout(false);
        }

        private Label lblTieuDe;
        private GroupBox grpHocVien;
        private GroupBox grpKhoaHoc;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblDemKyTu;
        private Label lblSoDienThoai;
        private TextBox txtSoDienThoai;
        private Label lblNgaySinh;
        private DateTimePicker dtpNgaySinh;
        private CheckBox chkNhanEmail;
        private Label lblTrangThaiEmail;
        private Label lblKhoaHoc;
        private ComboBox cboKhoaHoc;
        private Label lblHinhThuc;
        private RadioButton radOnline;
        private RadioButton radOffline;
        private Label lblSoThang;
        private NumericUpDown numSoThang;
        private Label lblHocPhi;
        private Label lblHocPhiThang;
        private Label lblTongTienText;
        private Label lblTongTien;
        private Button btnDangKy;
        private Button btnLamMoi;
        private Button btnThoat;
    }
}