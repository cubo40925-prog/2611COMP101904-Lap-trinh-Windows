using System.ComponentModel;
using ShopManagerMini.Models;

namespace ShopManagerMini.Data;

/// <summary>
/// Kho dữ liệu dùng chung trong bộ nhớ. Đặt static để khi đóng rồi mở lại
/// FrmProduct / FrmCustomer thì dữ liệu vẫn còn.
/// </summary>
public static class DataStore
{
    public static List<User> Users { get; } = new()
    {
        new User { Username = "admin", Password = "123", FullName = "Quản trị viên", Role = "Admin" },
        new User { Username = "user",  Password = "123", FullName = "Nhân viên bán hàng", Role = "User" }
    };

    public static BindingList<Product> Products { get; } = new()
    {
        new Product { MaSP = "SP01", TenSP = "Mì gói Hảo Hảo", LoaiSP = "Thực phẩm", DonGia = 4500, SoLuong = 200 },
        new Product { MaSP = "SP02", TenSP = "Nước suối 500ml", LoaiSP = "Đồ uống", DonGia = 6000, SoLuong = 150 },
        new Product { MaSP = "SP03", TenSP = "Bột giặt 3kg", LoaiSP = "Gia dụng", DonGia = 135000, SoLuong = 25 },
        new Product { MaSP = "SP04", TenSP = "Bánh quy bơ", LoaiSP = "Bánh kẹo", DonGia = 32000, SoLuong = 60 }
    };

    public static BindingList<Customer> Customers { get; } = new()
    {
        new Customer { MaKH = "KH01", HoTen = "Nguyễn Văn An", SoDienThoai = "0901234567", LoaiKH = "Thường" },
        new Customer { MaKH = "KH02", HoTen = "Trần Thị Bình", SoDienThoai = "0912345678", LoaiKH = "Thân thiết" },
        new Customer { MaKH = "KH03", HoTen = "Lê Hoàng Cường", SoDienThoai = "0987654321", LoaiKH = "VIP" }
    };

    /// <summary>Trả về User nếu đúng tài khoản/mật khẩu, ngược lại trả về null.</summary>
    public static User? Authenticate(string username, string password) =>
        Users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password == password);
}
