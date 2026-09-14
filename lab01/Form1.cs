using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab01
{
    public partial class Form1 : Form
    {
        private TextBox txtHoTen, txtNamSinh, txtEmail, txtKetQua;
        private RadioButton radNam, radNu;
        private ComboBox cboKhoa;

        public Form1()
        {
            Text = "Lab 01 - Ứng dụng thông tin cá nhân";
            Width = 650;
            Height = 780;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;

            Font fontNhan = new Font("Segoe UI", 11, FontStyle.Bold);

            // Panel tieu de mau vang nhat
            Panel panelTieuDe = new Panel { Location = new Point(0, 0), Size = new Size(650, 70), BackColor = Color.FromArgb(255, 224, 130) };
            Label lblTitle = new Label { Text = "THÔNG TIN CÁ NHÂN", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(60, 40, 0), Size = new Size(650, 70), TextAlign = ContentAlignment.MiddleCenter };
            panelTieuDe.Controls.Add(lblTitle);
            Controls.Add(panelTieuDe);

            // Ho ten
            Controls.Add(new Label { Text = "Họ tên:", Font = fontNhan, AutoSize = true, Location = new Point(40, 90) });
            txtHoTen = new TextBox { Location = new Point(150, 87), Width = 300 };
            Controls.Add(txtHoTen);

            // Nam sinh - xep rieng 1 hang, tang do rong o de hien du so
            Controls.Add(new Label { Text = "Năm sinh:", Font = fontNhan, AutoSize = true, Location = new Point(40, 135) });
            txtNamSinh = new TextBox { Location = new Point(150, 132), Width = 150 };
            Controls.Add(txtNamSinh);

            // Email
            Controls.Add(new Label { Text = "Email:", Font = fontNhan, AutoSize = true, Location = new Point(40, 180) });
            txtEmail = new TextBox { Location = new Point(150, 177), Width = 300 };
            Controls.Add(txtEmail);

            // Gioi tinh - khong chon san, bat buoc nguoi dung phai tu chon
            GroupBox grpGioiTinh = new GroupBox { Text = "Giới tính:", Location = new Point(40, 220), Width = 400, Height = 60, ForeColor = Color.FromArgb(80, 60, 0) };
            radNam = new RadioButton { Text = "Nam", Location = new Point(30, 25), AutoSize = true };
            radNu = new RadioButton { Text = "Nữ", Location = new Point(180, 25), AutoSize = true };
            grpGioiTinh.Controls.Add(radNam);
            grpGioiTinh.Controls.Add(radNu);
            Controls.Add(grpGioiTinh);

            // Khoa/Lop - co san 5 lua chon, khong chon san lua chon nao
            Controls.Add(new Label { Text = "Khoa/Lớp:", Font = fontNhan, AutoSize = true, Location = new Point(40, 300) });
            cboKhoa = new ComboBox { Location = new Point(150, 297), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            cboKhoa.Items.AddRange(new object[] { "Công nghệ thông tin", "Toán học", "Vật lý", "Hóa học", "Ngữ văn" });
            cboKhoa.SelectedIndex = -1;
            Controls.Add(cboKhoa);

            // Ket qua
            Controls.Add(new Label { Text = "Kết quả", Font = new Font("Segoe UI", 13, FontStyle.Bold), AutoSize = true, Location = new Point(40, 350) });
            txtKetQua = new TextBox { Location = new Point(40, 385), Width = 500, Height = 150, Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = true, BackColor = Color.White };
            Controls.Add(txtKetQua);

            // Nut Hien thi - mau xanh la
            Button btnHienThi = new Button { Text = "Hiển thị", Location = new Point(80, 560), Width = 100, Height = 35, BackColor = Color.FromArgb(76, 175, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnHienThi.FlatAppearance.BorderSize = 0;
            btnHienThi.Click += BtnHienThi_Click;
            Controls.Add(btnHienThi);

            // Nut Xoa - mau cam
            Button btnXoa = new Button { Text = "Xóa", Location = new Point(240, 560), Width = 100, Height = 35, BackColor = Color.FromArgb(255, 152, 0), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.Click += BtnXoa_Click;
            Controls.Add(btnXoa);

            // Nut Thoat - mau do
            Button btnThoat = new Button { Text = "Thoát", Location = new Point(400, 560), Width = 100, Height = 35, BackColor = Color.FromArgb(244, 67, 54), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnThoat.FlatAppearance.BorderSize = 0;
            btnThoat.Click += BtnThoat_Click;
            Controls.Add(btnThoat);
        }

        // Bam "Hien thi": kiem tra du lieu hop le, tinh tuoi, in ket qua
        private void BtnHienThi_Click(object sender, EventArgs e)
        {
            // 1. Ho ten khong duoc rong
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được để trống!", "Thông báo lỗi");
                txtHoTen.Focus();
                return;
            }

            // 2. Nam sinh khong duoc rong
            if (string.IsNullOrWhiteSpace(txtNamSinh.Text))
            {
                MessageBox.Show("Năm sinh không được để trống!", "Thông báo lỗi");
                txtNamSinh.Focus();
                return;
            }

            // 3. Nam sinh phai la so nguyen
            if (!int.TryParse(txtNamSinh.Text, out int namSinh))
            {
                MessageBox.Show("Năm sinh phải là số nguyên!", "Thông báo lỗi");
                txtNamSinh.Focus();
                return;
            }

            // 4. Nam sinh phai nam trong khoang 1900 den nam hien tai
            int namHienTai = DateTime.Now.Year;
            if (namSinh < 1900 || namSinh > namHienTai)
            {
                MessageBox.Show($"Năm sinh phải từ 1900 đến {namHienTai}!", "Thông báo lỗi");
                txtNamSinh.Focus();
                return;
            }

            // 5. Email khong duoc rong
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email không được để trống!", "Thông báo lỗi");
                txtEmail.Focus();
                return;
            }

            // 6. Phai chon gioi tinh
            if (!radNam.Checked && !radNu.Checked)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo lỗi");
                return;
            }

            // 7. Phai chon khoa hoac lop
            if (cboKhoa.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khoa hoặc lớp!", "Thông báo lỗi");
                cboKhoa.Focus();
                return;
            }

            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";
            int tuoi = namHienTai - namSinh;

            txtKetQua.Text = $"THÔNG TIN SINH VIÊN\r\nHọ tên: {txtHoTen.Text}\r\nTuổi: {tuoi}\r\nEmail: {txtEmail.Text}\r\nGiới tính: {gioiTinh}\r\nKhoa/Lớp: {cboKhoa.SelectedItem}";
        }

        // Bam "Xoa": dua cac control ve trang thai ban dau (rong, chua chon)
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtNamSinh.Clear();
            txtEmail.Clear();
            radNam.Checked = false;
            radNu.Checked = false;
            cboKhoa.SelectedIndex = -1;
            txtKetQua.Clear();
        }

        // Bam "Thoat": hoi xac nhan roi dong chuong trinh
        private void BtnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Application.Exit();
        }
    }
}