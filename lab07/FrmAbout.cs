namespace ShopManagerMini.Forms;

/// <summary>Form giới thiệu ứng dụng.</summary>
public partial class FrmAbout : Form
{
    // TODO: sửa thành thông tin của bạn trước khi nộp bài.
    private const string StudentName = "Lê Quốc Tuấn - 51.01.104.115";
    private const string StudentClass = "51.01.CNTT.A";

    public FrmAbout()
    {
        InitializeComponent();
        lblSinhVien.Text = $"Sinh viên: {StudentName}";
        lblLop.Text = $"Lớp: {StudentClass}";
    }
}
