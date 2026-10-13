using ShopManagerMini.Data;
using ShopManagerMini.Models;

namespace ShopManagerMini.Forms;

/// <summary>Form đăng nhập. Hiển thị bằng ShowDialog() và trả DialogResult.OK khi thành công.</summary>
public partial class FrmLogin : Form
{
    /// <summary>User đã đăng nhập thành công; Program đọc property này để truyền sang FrmMain.</summary>
    public User? LoggedInUser { get; private set; }

    public FrmLogin()
    {
        InitializeComponent();
    }

    private void btnDangNhap_Click(object? sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text;

        // 1. Kiểm tra bỏ trống
        if (username.Length == 0 || password.Length == 0)
        {
            MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu.", "Cảnh báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            (username.Length == 0 ? txtUsername : txtPassword).Focus();
            return;
        }

        // 2. Kiểm tra tài khoản
        User? user = DataStore.Authenticate(username, password);
        if (user == null)
        {
            MessageBox.Show("Sai tài khoản hoặc mật khẩu.", "Đăng nhập thất bại",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtPassword.Clear();
            txtPassword.Focus();
            return;
        }

        // 3. Thành công: lưu user, đặt DialogResult.OK (form tự đóng)
        LoggedInUser = user;
        DialogResult = DialogResult.OK;
    }
}
