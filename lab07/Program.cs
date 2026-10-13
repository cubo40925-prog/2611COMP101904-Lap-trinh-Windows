using ShopManagerMini.Forms;

namespace ShopManagerMini;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Vòng lặp cho phép "Đăng xuất" quay lại màn hình đăng nhập.
        while (true)
        {
            using var login = new FrmLogin();

            // ShowDialog: hiển thị dạng modal, chương trình dừng tại đây đến khi đóng form.
            if (login.ShowDialog() != DialogResult.OK || login.LoggedInUser == null)
                break;   // bấm Thoát hoặc đóng form đăng nhập => kết thúc chương trình

            // Truyền user sang form chính bằng constructor.
            using var main = new FrmMain(login.LoggedInUser);
            Application.Run(main);

            if (!main.LoggedOut)
                break;   // thoát chương trình, không phải đăng xuất
        }
    }
}
