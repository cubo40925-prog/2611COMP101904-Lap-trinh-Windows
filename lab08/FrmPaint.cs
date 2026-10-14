using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace COMP1019_Lab08_SimplePaint;

/// <summary>
/// Form chính của ứng dụng Simple Paint. Giao diện được dựng bằng code
/// (BuildUserInterface), sự kiện được đăng ký trong RegisterEvents.
/// </summary>
public class FrmPaint : Form
{
    // ----- Control giao diện (tên theo gợi ý của đề) -----
    private readonly DrawSurface canvas = new DrawSurface();
    private readonly ComboBox cboMode = new ComboBox();
    private readonly NumericUpDown nudWidth = new NumericUpDown();
    private readonly Button btnColor = new Button();
    private readonly Panel pnlColorPreview = new Panel();
    private readonly Button btnUndo = new Button();
    private readonly Button btnClear = new Button();
    private readonly Label lblStatus = new Label();

    // ----- Control chức năng nâng cao -----
    private readonly CheckBox chkFill = new CheckBox();
    private readonly Button btnBackColor = new Button();
    private readonly Button btnOpenImage = new Button();
    private readonly Button btnRemoveImage = new Button();
    private readonly Button btnSave = new Button();

    // ----- Dữ liệu -----
    private readonly List<DrawItem> items = new List<DrawItem>();   // dữ liệu gốc của các nét đã vẽ xong
    private DrawItem? currentItem;                                   // nét đang kéo chuột (tạm)
    private bool isDrawing = false;
    private Color currentColor = Color.Black;
    private Bitmap? backgroundImage;                                 // ảnh nền (tùy chọn)
    private string lastMessage = "Sẵn sàng";

    public FrmPaint()
    {
        Text = "COMP1019 - Simple Paint";
        Width = 1100;
        Height = 700;
        MinimumSize = new Size(720, 450);
        StartPosition = FormStartPosition.CenterScreen;

        BuildUserInterface();
        RegisterEvents();
        UpdateStatus(null, "Chọn chế độ, màu, độ dày rồi kéo chuột trên vùng trắng để vẽ");
    }

    // =====================================================================
    // DỰNG GIAO DIỆN
    // =====================================================================
    private void BuildUserInterface()
    {
        // FlowLayoutPanel tự xuống dòng khi cửa sổ hẹp nên các control không bị che.
        var topPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = true,
            Padding = new Padding(6, 4, 6, 4),
            BackColor = Color.FromArgb(238, 247, 247)
        };

        var lblMode = new Label { Text = "Mode", AutoSize = true, Margin = new Padding(6, 10, 0, 3) };
        cboMode.DropDownStyle = ComboBoxStyle.DropDownList;
        cboMode.Items.AddRange(new object[] { "Pen", "Line", "Rectangle", "Ellipse" });
        cboMode.SelectedIndex = 0;
        cboMode.Width = 110;
        cboMode.Margin = new Padding(3, 6, 12, 3);

        var lblWidth = new Label { Text = "Width", AutoSize = true, Margin = new Padding(6, 10, 0, 3) };
        nudWidth.Minimum = 1;
        nudWidth.Maximum = 20;
        nudWidth.Value = 3;
        nudWidth.Width = 60;
        nudWidth.Margin = new Padding(3, 6, 12, 3);

        btnColor.Text = "Color";
        btnColor.Size = new Size(80, 30);
        pnlColorPreview.Size = new Size(26, 26);
        pnlColorPreview.BorderStyle = BorderStyle.FixedSingle;
        pnlColorPreview.BackColor = currentColor;
        pnlColorPreview.Margin = new Padding(3, 5, 12, 3);

        chkFill.Text = "Fill";
        chkFill.AutoSize = true;
        chkFill.Enabled = false;   // chỉ bật khi chọn Rectangle hoặc Ellipse
        chkFill.Margin = new Padding(3, 9, 12, 3);

        btnBackColor.Text = "Back color";
        btnBackColor.Size = new Size(90, 30);

        btnUndo.Text = "Undo";
        btnUndo.Size = new Size(80, 30);
        btnUndo.Margin = new Padding(12, 3, 3, 3);

        btnClear.Text = "Clear";
        btnClear.Size = new Size(80, 30);

        btnOpenImage.Text = "Open image";
        btnOpenImage.Size = new Size(95, 30);
        btnOpenImage.Margin = new Padding(12, 3, 3, 3);

        btnRemoveImage.Text = "Remove image";
        btnRemoveImage.Size = new Size(105, 30);

        btnSave.Text = "Save PNG";
        btnSave.Size = new Size(90, 30);

        topPanel.Controls.Add(lblMode);
        topPanel.Controls.Add(cboMode);
        topPanel.Controls.Add(lblWidth);
        topPanel.Controls.Add(nudWidth);
        topPanel.Controls.Add(btnColor);
        topPanel.Controls.Add(pnlColorPreview);
        topPanel.Controls.Add(chkFill);
        topPanel.Controls.Add(btnBackColor);
        topPanel.Controls.Add(btnUndo);
        topPanel.Controls.Add(btnClear);
        topPanel.Controls.Add(btnOpenImage);
        topPanel.Controls.Add(btnRemoveImage);
        topPanel.Controls.Add(btnSave);

        lblStatus.Dock = DockStyle.Bottom;
        lblStatus.Height = 26;
        lblStatus.Text = "Ready";
        lblStatus.TextAlign = ContentAlignment.MiddleLeft;
        lblStatus.BackColor = SystemColors.ControlLight;
        lblStatus.Padding = new Padding(8, 0, 0, 0);

        // Thứ tự Add: canvas (Fill) trước, rồi Bottom, rồi Top để dock đúng vị trí.
        Controls.Add(canvas);
        Controls.Add(lblStatus);
        Controls.Add(topPanel);
    }

    private void RegisterEvents()
    {
        canvas.Paint += Canvas_Paint;
        canvas.MouseDown += Canvas_MouseDown;
        canvas.MouseMove += Canvas_MouseMove;
        canvas.MouseUp += Canvas_MouseUp;
        canvas.MouseLeave += Canvas_MouseLeave;

        cboMode.SelectedIndexChanged += CboMode_SelectedIndexChanged;
        btnColor.Click += BtnColor_Click;
        btnUndo.Click += BtnUndo_Click;
        btnClear.Click += BtnClear_Click;
        btnBackColor.Click += BtnBackColor_Click;
        btnOpenImage.Click += BtnOpenImage_Click;
        btnRemoveImage.Click += BtnRemoveImage_Click;
        btnSave.Click += BtnSave_Click;
    }

    /// <summary>Chế độ vẽ hiện tại, đổi từ chỉ số đang chọn trong ComboBox.</summary>
    private DrawMode CurrentMode =>
        cboMode.SelectedIndex >= 0 ? (DrawMode)cboMode.SelectedIndex : DrawMode.Pen;

    private void CboMode_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // Chỉ Rectangle và Ellipse mới có thể tô màu.
        chkFill.Enabled = CurrentMode is DrawMode.Rectangle or DrawMode.Ellipse;
    }

    // =====================================================================
    // CHUỘT: MouseDown -> MouseMove -> MouseUp
    // =====================================================================
    private void Canvas_MouseDown(object? sender, MouseEventArgs e)
    {
        // Chỉ chuột trái mới vẽ; chuột phải không tạo nét vẽ mới.
        if (e.Button != MouseButtons.Left || isDrawing) return;

        isDrawing = true;

        currentItem = new DrawItem
        {
            Mode = CurrentMode,
            Start = e.Location,
            End = e.Location,
            Color = currentColor,
            Width = (float)nudWidth.Value,
            Filled = chkFill.Checked && (CurrentMode is DrawMode.Rectangle or DrawMode.Ellipse)
        };

        if (currentItem.Mode == DrawMode.Pen)
            currentItem.Points.Add(e.Location);
    }

    private void Canvas_MouseMove(object? sender, MouseEventArgs e)
    {
        UpdateStatus(e.Location, null);

        if (!isDrawing || currentItem == null) return;

        currentItem.End = e.Location;

        if (currentItem.Mode == DrawMode.Pen)
            currentItem.Points.Add(e.Location);

        canvas.Invalidate();   // chỉ yêu cầu vẽ lại, Paint sẽ được hệ thống gọi
    }

    private void Canvas_MouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left || !isDrawing || currentItem == null) return;

        currentItem.End = e.Location;

        // Hình (không phải Pen) có hai đầu trùng nhau thì không có kích thước, bỏ đi.
        bool isEmptyShape = currentItem.Mode != DrawMode.Pen && currentItem.Start == currentItem.End;
        if (!isEmptyShape)
            items.Add(currentItem);   // thả chuột: đưa nét tạm vào danh sách chính

        currentItem = null;
        isDrawing = false;
        canvas.Invalidate();
        UpdateStatus(e.Location, null);
    }

    private void Canvas_MouseLeave(object? sender, EventArgs e)
    {
        if (!isDrawing)
            UpdateStatus(null, null);
    }

    // =====================================================================
    // VẼ: toàn bộ việc vẽ nằm trong Paint, dữ liệu lấy từ danh sách items
    // =====================================================================
    private void Canvas_Paint(object? sender, PaintEventArgs e)
    {
        RenderScene(e.Graphics, includeCurrentItem: true);
    }

    /// <summary>Vẽ ảnh nền, các nét đã lưu và (nếu cần) nét đang kéo. Dùng chung cho Paint và Save PNG.</summary>
    private void RenderScene(Graphics g, bool includeCurrentItem)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;

        if (backgroundImage != null)
            g.DrawImage(backgroundImage, 0, 0, backgroundImage.Width, backgroundImage.Height);

        foreach (DrawItem item in items)
            DrawOneItem(g, item);

        if (includeCurrentItem && currentItem != null)
            DrawOneItem(g, currentItem);
    }

    private void DrawOneItem(Graphics g, DrawItem item)
    {
        using Pen pen = new Pen(item.Color, item.Width);
        pen.StartCap = LineCap.Round;
        pen.EndCap = LineCap.Round;
        pen.LineJoin = LineJoin.Round;

        Rectangle rect = GetRectangle(item.Start, item.End);

        switch (item.Mode)
        {
            case DrawMode.Pen:
                DrawFreePen(g, pen, item);
                break;

            case DrawMode.Line:
                g.DrawLine(pen, item.Start, item.End);
                break;

            case DrawMode.Rectangle:
                if (item.Filled)
                {
                    using SolidBrush brush = new SolidBrush(item.Color);
                    g.FillRectangle(brush, rect);
                }
                g.DrawRectangle(pen, rect);
                break;

            case DrawMode.Ellipse:
                if (item.Filled)
                {
                    using SolidBrush brush = new SolidBrush(item.Color);
                    g.FillEllipse(brush, rect);
                }
                g.DrawEllipse(pen, rect);
                break;
        }
    }

    private void DrawFreePen(Graphics g, Pen pen, DrawItem item)
    {
        List<Point> points = item.Points;

        if (points.Count == 1)
        {
            // Chỉ chấm một điểm: vẽ chấm tròn để nhìn thấy được.
            using SolidBrush brush = new SolidBrush(item.Color);
            float r = item.Width / 2f;
            g.FillEllipse(brush, points[0].X - r, points[0].Y - r, item.Width, item.Width);
        }
        else if (points.Count > 1)
        {
            g.DrawLines(pen, points.ToArray());
        }
    }

    /// <summary>
    /// Tạo hình chữ nhật hợp lệ (rộng, cao không âm) từ hai điểm bất kỳ,
    /// nên kéo chuột theo mọi hướng (kể cả phải-trái, dưới-lên) đều đúng.
    /// </summary>
    private Rectangle GetRectangle(Point start, Point end)
    {
        int x = Math.Min(start.X, end.X);
        int y = Math.Min(start.Y, end.Y);
        int width = Math.Abs(start.X - end.X);
        int height = Math.Abs(start.Y - end.Y);

        return new Rectangle(x, y, width, height);
    }

    // =====================================================================
    // NÚT: màu, undo, clear
    // =====================================================================
    private void BtnColor_Click(object? sender, EventArgs e)
    {
        using ColorDialog dialog = new ColorDialog();
        dialog.Color = currentColor;
        dialog.FullOpen = true;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            currentColor = dialog.Color;
            pnlColorPreview.BackColor = currentColor;
            UpdateStatus(null, "Đã đổi màu vẽ");
        }
    }

    private void BtnUndo_Click(object? sender, EventArgs e) => Undo();

    private void Undo()
    {
        if (isDrawing) return;   // đang kéo chuột thì chưa Undo

        if (items.Count == 0)
        {
            UpdateStatus(null, "Không còn nét vẽ nào để Undo");
            return;
        }

        items.RemoveAt(items.Count - 1);
        canvas.Invalidate();
        UpdateStatus(null, "Đã xóa nét vẽ gần nhất");
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        if (items.Count == 0)
        {
            UpdateStatus(null, "Canvas đang trống");
            return;
        }

        DialogResult result = MessageBox.Show(
            "Xóa toàn bộ vùng vẽ?",
            "Xác nhận",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            items.Clear();
            canvas.Invalidate();
            UpdateStatus(null, "Đã xóa toàn bộ nét vẽ");
        }
    }

    // =====================================================================
    // NÂNG CAO: màu nền, ảnh nền, lưu PNG, phím tắt
    // =====================================================================
    private void BtnBackColor_Click(object? sender, EventArgs e)
    {
        using ColorDialog dialog = new ColorDialog();
        dialog.Color = canvas.BackColor;
        dialog.FullOpen = true;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            canvas.BackColor = dialog.Color;
            canvas.Invalidate();
            UpdateStatus(null, "Đã đổi màu nền canvas");
        }
    }

    private void BtnOpenImage_Click(object? sender, EventArgs e) => OpenBackgroundImage();

    private void BtnRemoveImage_Click(object? sender, EventArgs e)
    {
        if (backgroundImage == null)
        {
            UpdateStatus(null, "Chưa có ảnh nền");
            return;
        }

        backgroundImage.Dispose();
        backgroundImage = null;
        canvas.Invalidate();
        UpdateStatus(null, "Đã xóa ảnh nền");
    }

    private void OpenBackgroundImage()
    {
        using OpenFileDialog dialog = new OpenFileDialog();
        dialog.Title = "Chọn ảnh nền";
        dialog.Filter = "Image files|*.png;*.jpg;*.jpeg;*.bmp;*.gif|All files|*.*";

        if (dialog.ShowDialog() != DialogResult.OK) return;

        try
        {
            Bitmap scaled = LoadScaledBitmap(dialog.FileName, canvas.ClientSize);
            backgroundImage?.Dispose();
            backgroundImage = scaled;
            canvas.Invalidate();
            UpdateStatus(null, $"Đã mở ảnh nền: {Path.GetFileName(dialog.FileName)}");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Không mở được ảnh: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Đọc ảnh (không khóa file), thu nhỏ vừa canvas nếu ảnh lớn hơn, giữ nguyên tỉ lệ.
    /// Ảnh chỉ co một lần khi mở nên các nét vẽ luôn khớp vị trí khi resize Form.
    /// </summary>
    private static Bitmap LoadScaledBitmap(string path, Size maxSize)
    {
        using FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        using Image source = Image.FromStream(stream);

        double scale = Math.Min(1.0, Math.Min(
            (double)Math.Max(1, maxSize.Width) / source.Width,
            (double)Math.Max(1, maxSize.Height) / source.Height));
        int w = Math.Max(1, (int)(source.Width * scale));
        int h = Math.Max(1, (int)(source.Height * scale));

        Bitmap result = new Bitmap(w, h);
        using Graphics g = Graphics.FromImage(result);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.DrawImage(source, 0, 0, w, h);
        return result;
    }

    private void BtnSave_Click(object? sender, EventArgs e) => SavePng();

    private void SavePng()
    {
        if (canvas.ClientSize.Width <= 0 || canvas.ClientSize.Height <= 0)
        {
            MessageBox.Show("Vùng vẽ quá nhỏ để lưu.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using SaveFileDialog dialog = new SaveFileDialog();
        dialog.Title = "Lưu hình vẽ";
        dialog.Filter = "PNG image (*.png)|*.png";
        dialog.DefaultExt = "png";
        dialog.FileName = "Drawing.png";

        if (dialog.ShowDialog() != DialogResult.OK) return;

        try
        {
            using Bitmap bitmap = new Bitmap(canvas.ClientSize.Width, canvas.ClientSize.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(canvas.BackColor);
                RenderScene(g, includeCurrentItem: false);   // cùng hàm vẽ với canvas
            }
            bitmap.Save(dialog.FileName, ImageFormat.Png);
            UpdateStatus(null, $"Đã lưu {Path.GetFileName(dialog.FileName)}");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Không lưu được file: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>Phím tắt: Ctrl+Z = Undo, Ctrl+S = Lưu PNG, Ctrl+O = Mở ảnh nền.</summary>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        switch (keyData)
        {
            case Keys.Control | Keys.Z:
                Undo();
                return true;
            case Keys.Control | Keys.S:
                SavePng();
                return true;
            case Keys.Control | Keys.O:
                OpenBackgroundImage();
                return true;
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        backgroundImage?.Dispose();
        backgroundImage = null;
        base.OnFormClosed(e);
    }

    // =====================================================================
    // THANH TRẠNG THÁI (lblStatus)
    // =====================================================================
    /// <summary>
    /// Hiển thị tọa độ chuột, số nét vẽ và thông báo. Truyền location = null khi chuột ra ngoài canvas.
    /// message = null thì giữ nguyên thông báo trước đó.
    /// </summary>
    private void UpdateStatus(Point? location, string? message)
    {
        if (message != null)
            lastMessage = message;

        string coords = location.HasValue ? $"X = {location.Value.X}, Y = {location.Value.Y}" : "X = -, Y = -";
        lblStatus.Text = $"{coords}  |  Items = {items.Count}  |  {lastMessage}";
    }
}
