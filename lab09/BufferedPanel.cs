namespace StudentCardPrinter;

/// <summary>Panel bật DoubleBuffered và tự vẽ lại khi đổi kích thước, giúp preview không nhấp nháy.</summary>
public class BufferedPanel : Panel
{
    public BufferedPanel()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
    }
}
