namespace COMP1019_Lab08_SimplePaint;

/// <summary>
/// Panel tùy chỉnh làm vùng vẽ (canvas). Bật DoubleBuffered để giảm nhấp nháy
/// khi Paint được gọi liên tục lúc kéo chuột.
/// </summary>
public class DrawSurface : Panel
{
    public DrawSurface()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
        BackColor = Color.White;
        Cursor = Cursors.Cross;
        Dock = DockStyle.Fill;
    }
}
