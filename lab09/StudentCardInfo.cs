namespace StudentCardPrinter;

/// <summary>
/// Gom toàn bộ dữ liệu cần vẽ lên thẻ. Tách dữ liệu ra khỏi control nên
/// cùng một hàm vẽ có thể dùng cho cả preview và in.
/// </summary>
public class StudentCardInfo
{
    public string StudentId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public string Faculty { get; set; } = string.Empty;
    public string Course { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; } = string.Empty;
    public Image? Photo { get; set; }

    /// <summary>Màu nền thẻ (chức năng mở rộng: cho phép chọn màu nền thẻ).</summary>
    public Color CardColor { get; set; } = Color.FromArgb(238, 247, 247);
}
