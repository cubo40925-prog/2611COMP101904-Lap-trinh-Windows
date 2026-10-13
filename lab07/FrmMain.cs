using ShopManagerMini.Models;

namespace ShopManagerMini.Forms;

/// <summary>Form chính dạng MDI Parent (IsMdiContainer = true).</summary>
public partial class FrmMain : Form
{
    private readonly User currentUser;
    private bool skipExitConfirm;   // true khi đăng xuất (đã hỏi xác nhận riêng)

    /// <summary>true nếu người dùng chọn Đăng xuất (Program sẽ hiện lại form đăng nhập).</summary>
    public bool LoggedOut { get; private set; }

    /// <summary>Nhận User từ FrmLogin thông qua constructor.</summary>
    public FrmMain(User user)
    {
        InitializeComponent();
        currentUser = user;
    }

    private void FrmMain_Load(object? sender, EventArgs e)
    {
        Text = $"ShopManagerMini - {currentUser.FullName}";
        lblUser.Text = $"Người dùng: {currentUser.FullName}";
        lblRole.Text = $"Vai trò: {currentUser.Role}";
        UpdateClock();
        timerClock.Start();
    }

    private void timerClock_Tick(object? sender, EventArgs e) => UpdateClock();

    private void UpdateClock() => lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

    /// <summary>
    /// Mở form con MDI kiểu T. Nếu đã có form kiểu T đang mở thì chỉ kích hoạt lại, không tạo mới.
    /// </summary>
    private void OpenChildForm<T>() where T : Form, new()
    {
        foreach (Form child in MdiChildren)
        {
            if (child is T)
            {
                if (child.WindowState == FormWindowState.Minimized)
                    child.WindowState = FormWindowState.Normal;
                child.Activate();
                return;
            }
        }

        T form = new T { MdiParent = this };
        form.Show();
    }

    private void mnuQuanLySanPham_Click(object? sender, EventArgs e) => OpenChildForm<FrmProduct>();

    private void mnuQuanLyKhachHang_Click(object? sender, EventArgs e) => OpenChildForm<FrmCustomer>();

    private void mnuGioiThieu_Click(object? sender, EventArgs e)
    {
        using var about = new FrmAbout();
        about.ShowDialog(this);
    }

    private void mnuDangXuat_Click(object? sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Đăng xuất",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result != DialogResult.Yes) return;

        LoggedOut = true;
        skipExitConfirm = true;
        Close();
    }

    private void mnuThoat_Click(object? sender, EventArgs e) => Close();

    /// <summary>Mọi cách thoát (menu Thoát, nút X, Alt+F4) đều đi qua đây để hỏi xác nhận.</summary>
    private void FrmMain_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (skipExitConfirm) return;

        DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát chương trình?", "Xác nhận thoát",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result != DialogResult.Yes)
            e.Cancel = true;
    }
}
