namespace ShopManagerMini.Models;

/// <summary>Sản phẩm của cửa hàng.</summary>
public class Product
{
    public string MaSP { get; set; } = string.Empty;
    public string TenSP { get; set; } = string.Empty;
    public string LoaiSP { get; set; } = string.Empty;
    public decimal DonGia { get; set; }
    public int SoLuong { get; set; }
}
