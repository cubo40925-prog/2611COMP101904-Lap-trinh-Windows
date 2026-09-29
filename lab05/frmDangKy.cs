using System.Globalization;

namespace CourseRegistrationApp
{
    public partial class frmDangKy : Form
    {
        // Dữ liệu khóa học: tên và học phí / tháng
        private readonly (string Ten, decimal HocPhi)[] dsKhoaHoc =
        {
            ("C# WinForms cơ bản",      800000m),
            ("SQL Server cơ bản",       700000m),
            ("Web Frontend cơ bản",     750000m),
            ("Lập trình Python cơ bản", 650000m)
        };

        private static readonly CultureInfo viVN = new CultureInfo("vi-VN");

        public frmDangKy()
        {
            InitializeComponent();
        }

        // ===== 5.1. Form Load =====
        private void frmDangKy_Load(object sender, EventArgs e)
        {
            cboKhoaHoc.Items.Clear();
            foreach (var kh in dsKhoaHoc)
                cboKhoaHoc.Items.Add(kh.Ten);

            cboKhoaHoc.SelectedIndex = 0;
            radOnline.Checked = true;

            numSoThang.Minimum = 1;
            numSoThang.Maximum = 12;
            numSoThang.Value = 1;

            CapNhatHocPhi();
        }

        // ===== Hàm hỗ trợ =====
        private decimal LayHocPhiMotThang()
        {
            int index = cboKhoaHoc.SelectedIndex;
            return index >= 0 ? dsKhoaHoc[index].HocPhi : 0m;
        }

        private static string DinhDangTien(decimal soTien)
        {
            return soTien.ToString("N0", viVN) + " VNĐ";
        }

        private void CapNhatHocPhi()
        {
            decimal hocPhi = LayHocPhiMotThang();
            lblHocPhiThang.Text = DinhDangTien(hocPhi);
            lblTongTien.Text = DinhDangTien(hocPhi * numSoThang.Value);
        }

        // ===== Sự kiện thay đổi =====
        private void cboKhoaHoc_SelectedIndexChanged(object sender, EventArgs e) => CapNhatHocPhi();

        private void numSoThang_ValueChanged(object sender, EventArgs e) => CapNhatHocPhi();

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
            lblDemKyTu.Text = txtHoTen.Text.Length + "/50";
        }

        private void chkNhanEmail_CheckedChanged(object sender, EventArgs e)
        {
            lblTrangThaiEmail.Text = chkNhanEmail.Checked
                ? "Nhận email thông báo"
                : "Không nhận email thông báo";
        }

        // ===== 5.2. Nút Đăng ký =====
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                return;
            }

            if (cboKhoaHoc.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn khóa học!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoaHoc.Focus();
                return;
            }

            string hinhThuc = radOnline.Checked ? "Online" : "Trực tiếp";
            string trangThaiEmail = chkNhanEmail.Checked ? "Có nhận email" : "Không nhận email";
            decimal tongTien = LayHocPhiMotThang() * numSoThang.Value;

            string phieu =
                "PHIẾU ĐĂNG KÝ KHÓA HỌC\n" +
                "-----------------------------------------\n" +
                $"Họ tên: {txtHoTen.Text.Trim()}\n" +
                $"Số điện thoại: {txtSoDienThoai.Text.Trim()}\n" +
                $"Ngày sinh: {dtpNgaySinh.Value:dd/MM/yyyy}\n" +
                $"Khóa học: {cboKhoaHoc.SelectedItem}\n" +
                $"Hình thức học: {hinhThuc}\n" +
                $"Số tháng: {numSoThang.Value}\n" +
                $"Tổng tiền: {DinhDangTien(tongTien)}\n" +
                $"Nhận email thông báo: {trangThaiEmail}";

            MessageBox.Show(phieu, "Đăng ký thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ===== 5.3. Nút Làm mới =====
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            chkNhanEmail.Checked = false;
            cboKhoaHoc.SelectedIndex = 0;
            radOnline.Checked = true;
            numSoThang.Value = 1;
            CapNhatHocPhi();
            txtHoTen.Focus();
        }

        // ===== 5.4. Nút Thoát =====
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?",
                "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
                Close();
        }
    }
}