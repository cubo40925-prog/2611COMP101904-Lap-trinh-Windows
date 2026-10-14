using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;

namespace StudentCardPrinter;

public partial class FrmStudentCard : Form
{
    // Kích thước "logic" của thẻ (tỉ lệ 520:320, gần giống thẻ ngang). Mọi tọa độ trong
    // DrawStudentCard tính theo hệ này rồi được phóng to/thu nhỏ vừa vùng cần vẽ,
    // nên preview, bản in và ảnh PNG đều có bố cục giống hệt nhau.
    private const int CardWidth = 520;
    private const int CardHeight = 320;

    private static readonly DateTime DefaultBirthDate = new DateTime(2004, 1, 1);
    private static readonly Color DefaultCardColor = Color.FromArgb(238, 247, 247);

    private Color cardColor = DefaultCardColor;

    public FrmStudentCard()
    {
        InitializeComponent();
        InitializeData();
        RegisterEvents();
    }

    // =====================================================================
    // KHỞI TẠO
    // =====================================================================
    private void InitializeData()
    {
        cboClass.Items.AddRange(new object[] { "K49.CNTT.A", "K49.CNTT.B", "K50.CNTT.A", "K51.CNTT.A" });
        cboFaculty.Items.AddRange(new object[] { "Công nghệ thông tin", "Toán - Tin học" });
        cboGender.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });

        cboClass.SelectedIndex = 0;
        cboFaculty.SelectedIndex = 0;
        cboGender.SelectedIndex = 0;

        dtpBirthDate.MaxDate = DateTime.Today;
        dtpBirthDate.Value = DefaultBirthDate;

        picAvatar.SizeMode = PictureBoxSizeMode.Zoom;   // ảnh giữ đúng tỉ lệ, không méo
    }

    /// <summary>Gắn sự kiện một lần duy nhất (đặc biệt là PrintPage, tránh bị vẽ lặp).</summary>
    private void RegisterEvents()
    {
        btnChoosePhoto.Click += btnChoosePhoto_Click;
        btnPreviewPrint.Click += btnPreviewPrint_Click;
        btnPrint.Click += btnPrint_Click;
        btnClear.Click += btnClear_Click;
        btnExit.Click += btnExit_Click;
        btnCardColor.Click += btnCardColor_Click;
        btnSavePng.Click += btnSavePng_Click;

        pnlCardPreview.Paint += pnlCardPreview_Paint;
        printDocument1.PrintPage += printDocument1_PrintPage;

        // Nhập hoặc đổi dữ liệu -> vẽ lại preview.
        txtStudentId.TextChanged += InputChanged;
        txtFullName.TextChanged += InputChanged;
        txtCourse.TextChanged += InputChanged;
        cboClass.TextChanged += InputChanged;
        cboClass.SelectedIndexChanged += InputChanged;
        cboFaculty.TextChanged += InputChanged;
        cboFaculty.SelectedIndexChanged += InputChanged;
        cboGender.SelectedIndexChanged += InputChanged;
        dtpBirthDate.ValueChanged += InputChanged;
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        picAvatar.Image?.Dispose();
        picAvatar.Image = null;
        base.OnFormClosed(e);
    }

    // =====================================================================
    // F1 - CHỌN ẢNH (an toàn: không khóa file, giải phóng ảnh cũ)
    // =====================================================================
    private void btnChoosePhoto_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog dlg = new OpenFileDialog();
        dlg.Title = "Chọn ảnh sinh viên";
        dlg.Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

        if (dlg.ShowDialog() != DialogResult.OK) return;

        try
        {
            // Đọc file rồi sao chép sang Bitmap mới để file gốc được đóng ngay (không bị khóa).
            using Image temp = Image.FromFile(dlg.FileName);
            Image copy = new Bitmap(temp);

            picAvatar.Image?.Dispose();   // giải phóng ảnh cũ
            picAvatar.Image = copy;
            pnlCardPreview.Invalidate();
        }
        catch (Exception)
        {
            MessageBox.Show("File được chọn không phải ảnh hợp lệ.",
                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // =====================================================================
    // LẤY DỮ LIỆU VÀ KIỂM TRA (F3)
    // =====================================================================
    private StudentCardInfo GetStudentInfo()
    {
        return new StudentCardInfo
        {
            StudentId = txtStudentId.Text.Trim(),
            FullName = txtFullName.Text.Trim(),
            ClassName = cboClass.Text.Trim(),
            Faculty = cboFaculty.Text.Trim(),
            Course = txtCourse.Text.Trim(),
            BirthDate = dtpBirthDate.Value,
            Gender = cboGender.Text,
            Photo = picAvatar.Image,
            CardColor = cardColor
        };
    }

    /// <summary>Kiểm tra dữ liệu bắt buộc: mã sinh viên, họ tên, ảnh. Báo MỘT LẦN tất cả mục còn thiếu.</summary>
    private bool ValidateInput()
    {
        List<string> missing = new List<string>();
        Control? firstInvalid = null;   // ô thiếu đầu tiên (theo thứ tự trên form) để đưa con trỏ về

        if (string.IsNullOrWhiteSpace(txtStudentId.Text))
        {
            missing.Add("Mã sinh viên");
            firstInvalid ??= txtStudentId;
        }

        if (string.IsNullOrWhiteSpace(txtFullName.Text))
        {
            missing.Add("Họ tên");
            firstInvalid ??= txtFullName;
        }

        if (picAvatar.Image == null)
        {
            missing.Add("Ảnh đại diện");
            firstInvalid ??= btnChoosePhoto;
        }

        if (missing.Count == 0)
            return true;

        MessageBox.Show(
            "Vui lòng bổ sung các thông tin bắt buộc còn thiếu:\n\n- " + string.Join("\n- ", missing),
            "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        firstInvalid?.Focus();
        return false;
    }

    // =====================================================================
    // F2 - PREVIEW THẺ TRÊN PANEL (vẽ trong Paint, Invalidate khi dữ liệu đổi)
    // =====================================================================
    private void InputChanged(object? sender, EventArgs e)
    {
        pnlCardPreview.Invalidate();
    }

    private void pnlCardPreview_Paint(object? sender, PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        DrawStudentCard(e.Graphics, GetPreviewCardRectangle(), GetStudentInfo());
    }

    /// <summary>Hình chữ nhật thẻ vừa khít Panel, giữ đúng tỉ lệ và nằm giữa.</summary>
    private Rectangle GetPreviewCardRectangle()
    {
        const int margin = 20;
        double aspect = CardWidth / (double)CardHeight;

        int availableWidth = Math.Max(1, pnlCardPreview.ClientSize.Width - 2 * margin);
        int availableHeight = Math.Max(1, pnlCardPreview.ClientSize.Height - 2 * margin);

        int width = availableWidth;
        int height = (int)(width / aspect);
        if (height > availableHeight)
        {
            height = availableHeight;
            width = (int)(height * aspect);
        }

        int x = (pnlCardPreview.ClientSize.Width - width) / 2;
        int y = (pnlCardPreview.ClientSize.Height - height) / 2;
        return new Rectangle(x, y, Math.Max(1, width), Math.Max(1, height));
    }

    // =====================================================================
    // HÀM VẼ THẺ DÙNG CHUNG (preview, in, lưu PNG)
    // =====================================================================
    /// <summary>
    /// Vẽ thẻ vào hình chữ nhật <paramref name="card"/> (phải đúng tỉ lệ 520:320).
    /// Font tính theo Pixel và có ScaleTransform nên kết quả không phụ thuộc DPI màn hình/máy in.
    /// </summary>
    private void DrawStudentCard(Graphics g, Rectangle card, StudentCardInfo s)
    {
        GraphicsState state = g.Save();
        try
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            float scale = card.Width / (float)CardWidth;
            g.TranslateTransform(card.Left, card.Top);
            g.ScaleTransform(scale, scale);

            using Font schoolFont = new Font("Arial", 13, FontStyle.Bold, GraphicsUnit.Pixel);
            using Font titleFont = new Font("Arial", 24, FontStyle.Bold, GraphicsUnit.Pixel);
            using Font labelFont = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel);
            using Font normalFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            using Pen borderPen = new Pen(Color.Navy, 2);
            using Pen accentPen = new Pen(Color.Navy, 1);
            using SolidBrush navyBrush = new SolidBrush(Color.Navy);
            using SolidBrush bgBrush = new SolidBrush(s.CardColor);

            // Nền và khung ngoài
            g.FillRectangle(bgBrush, 0, 0, CardWidth, CardHeight);
            g.DrawRectangle(borderPen, 1, 1, CardWidth - 2, CardHeight - 2);

            // Tên trường, tiêu đề
            g.DrawString("TRƯỜNG ĐẠI HỌC SƯ PHẠM TP. HỒ CHÍ MINH", schoolFont, navyBrush, 20, 14);
            g.DrawString("THẺ SINH VIÊN", titleFont, navyBrush, 20, 38);
            g.DrawLine(accentPen, 20, 76, CardWidth - 20, 76);

            // Ảnh bên trái
            Rectangle photoBox = new Rectangle(25, 92, 115, 150);
            g.FillRectangle(Brushes.White, photoBox);
            g.DrawRectangle(Pens.Gray, photoBox);
            if (s.Photo != null)
                DrawImageZoom(g, s.Photo, photoBox);
            else
                g.DrawString("Ảnh", normalFont, Brushes.Gray, photoBox.Left + 40, photoBox.Top + 65);

            // Thông tin bên phải
            int infoX = 165;
            int infoY = 92;
            int step = 28;
            DrawInfoLine(g, "MSSV:", s.StudentId, labelFont, normalFont, infoX, infoY);
            DrawInfoLine(g, "Họ tên:", s.FullName, labelFont, normalFont, infoX, infoY + step);
            DrawInfoLine(g, "Lớp:", s.ClassName, labelFont, normalFont, infoX, infoY + step * 2);
            DrawInfoLine(g, "Khoa:", s.Faculty, labelFont, normalFont, infoX, infoY + step * 3);
            DrawInfoLine(g, "Khóa:", s.Course, labelFont, normalFont, infoX, infoY + step * 4);
            DrawInfoLine(g, "Ngày sinh:", s.BirthDate.ToString("dd/MM/yyyy"), labelFont, normalFont, infoX, infoY + step * 5);
            DrawInfoLine(g, "Giới tính:", s.Gender, labelFont, normalFont, infoX, infoY + step * 6);
        }
        finally
        {
            g.Restore(state);
        }
    }

    /// <summary>Vẽ "nhãn: giá trị"; giá trị quá dài sẽ bị cắt bằng dấu ... để không tràn ra ngoài thẻ.</summary>
    private void DrawInfoLine(Graphics g, string label, string value,
        Font labelFont, Font normalFont, int x, int y)
    {
        const int valueOffset = 88;
        const int rightPadding = 20;

        g.DrawString(label, labelFont, Brushes.Black, x, y);

        RectangleF valueArea = new RectangleF(x + valueOffset, y, CardWidth - rightPadding - (x + valueOffset), 22);
        using StringFormat format = new StringFormat
        {
            Trimming = StringTrimming.EllipsisCharacter,
            FormatFlags = StringFormatFlags.NoWrap
        };
        g.DrawString(value, normalFont, Brushes.Black, valueArea, format);
    }

    /// <summary>Vẽ ảnh vừa khít khung, giữ đúng tỉ lệ (giống chế độ Zoom của PictureBox) nên không méo.</summary>
    private void DrawImageZoom(Graphics g, Image image, Rectangle box)
    {
        if (image.Width <= 0 || image.Height <= 0) return;

        float ratio = Math.Min(
            (float)box.Width / image.Width,
            (float)box.Height / image.Height);

        int width = (int)(image.Width * ratio);
        int height = (int)(image.Height * ratio);
        int x = box.Left + (box.Width - width) / 2;
        int y = box.Top + (box.Height - height) / 2;

        g.DrawImage(image, new Rectangle(x, y, width, height));
    }

    // =====================================================================
    // F4, F5 - XEM TRƯỚC IN VÀ IN
    // =====================================================================
    private void btnPreviewPrint_Click(object? sender, EventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Width = 900;
            printPreviewDialog1.Height = 700;
            printPreviewDialog1.ShowDialog(this);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Không thể xem trước bản in: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnPrint_Click(object? sender, EventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog(this) == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Không thể in: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>PrintDocument quyết định nội dung in; dùng đúng hàm DrawStudentCard như preview.</summary>
    private void printDocument1_PrintPage(object? sender, PrintPageEventArgs e)
    {
        if (e.Graphics == null) return;

        StudentCardInfo info = GetStudentInfo();

        // Đơn vị trang in là 1/100 inch. Thẻ rộng 520 (khoảng 5,2 inch), thu nhỏ nếu giấy hẹp hơn.
        int width = Math.Min(CardWidth, e.MarginBounds.Width);
        int height = (int)Math.Round(width * (double)CardHeight / CardWidth);
        Rectangle card = new Rectangle(e.MarginBounds.Left, e.MarginBounds.Top, width, height);

        DrawStudentCard(e.Graphics, card, info);
        e.HasMorePages = false;
    }

    // =====================================================================
    // F6 - LÀM MỚI, THOÁT
    // =====================================================================
    private void btnClear_Click(object? sender, EventArgs e)
    {
        txtStudentId.Clear();
        txtFullName.Clear();
        txtCourse.Clear();

        if (cboClass.Items.Count > 0) cboClass.SelectedIndex = 0;
        if (cboFaculty.Items.Count > 0) cboFaculty.SelectedIndex = 0;
        if (cboGender.Items.Count > 0) cboGender.SelectedIndex = 0;

        dtpBirthDate.Value = DefaultBirthDate;
        cardColor = DefaultCardColor;

        picAvatar.Image?.Dispose();
        picAvatar.Image = null;

        pnlCardPreview.Invalidate();
        txtStudentId.Focus();
    }

    private void btnExit_Click(object? sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát chương trình?", "Xác nhận thoát",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
            Close();
    }

    // =====================================================================
    // MỞ RỘNG: màu nền thẻ, lưu PNG
    // =====================================================================
    private void btnCardColor_Click(object? sender, EventArgs e)
    {
        using ColorDialog dialog = new ColorDialog();
        dialog.Color = cardColor;
        dialog.FullOpen = true;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            cardColor = dialog.Color;
            pnlCardPreview.Invalidate();
        }
    }

    private void btnSavePng_Click(object? sender, EventArgs e)
    {
        if (!ValidateInput()) return;

        using SaveFileDialog dialog = new SaveFileDialog();
        dialog.Title = "Lưu thẻ sinh viên";
        dialog.Filter = "PNG image (*.png)|*.png";
        dialog.DefaultExt = "png";
        string safeId = string.Concat(txtStudentId.Text.Trim()
            .Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));
        dialog.FileName = $"TheSinhVien_{safeId}.png";

        if (dialog.ShowDialog() != DialogResult.OK) return;

        try
        {
            // Vẽ ở kích thước gấp đôi để ảnh nét hơn.
            using Bitmap bitmap = new Bitmap(CardWidth * 2, CardHeight * 2);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                DrawStudentCard(g, new Rectangle(0, 0, bitmap.Width, bitmap.Height), GetStudentInfo());
            }
            bitmap.Save(dialog.FileName, ImageFormat.Png);
            MessageBox.Show("Đã lưu thẻ sinh viên.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Không lưu được file: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
