namespace COMP1019_Lab08_SimplePaint;

/// <summary>
/// Dữ liệu gốc của một nét vẽ hoặc một hình.
/// Khi Form bị resize hoặc canvas bị che rồi hiện lại, chương trình dựa vào
/// danh sách DrawItem để vẽ lại đầy đủ trong sự kiện Paint.
/// </summary>
public class DrawItem
{
    public DrawMode Mode { get; set; }

    /// <summary>Điểm bắt đầu (Line, Rectangle, Ellipse).</summary>
    public Point Start { get; set; }

    /// <summary>Điểm kết thúc (Line, Rectangle, Ellipse).</summary>
    public Point End { get; set; }

    public Color Color { get; set; }
    public float Width { get; set; }

    /// <summary>true = tô màu bên trong hình (chỉ áp dụng cho Rectangle và Ellipse).</summary>
    public bool Filled { get; set; }

    /// <summary>Các điểm đi qua của nét vẽ tự do (chỉ dùng cho chế độ Pen).</summary>
    public List<Point> Points { get; set; } = new List<Point>();
}
