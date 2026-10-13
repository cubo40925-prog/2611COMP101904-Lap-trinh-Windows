namespace ShopManagerMini.Models;

/// <summary>Tài khoản đăng nhập.</summary>
public class User
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;   // "Admin" hoặc "User"
}
