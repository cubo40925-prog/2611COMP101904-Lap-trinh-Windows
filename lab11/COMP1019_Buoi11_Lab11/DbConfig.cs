namespace COMP1019_Buoi11_Lab11;

/// <summary>
/// Nơi DUY NHẤT chứa chuỗi kết nối. Cần đổi Data Source cho đúng máy của bạn:
///   .                       : SQL Server mặc định trên máy này
///   .\SQLEXPRESS            : SQL Server Express
///   (localdb)\MSSQLLocalDB  : LocalDB đi kèm Visual Studio
/// Không muốn sửa code: đặt biến môi trường EDUMANAGER_CONNSTR (xem README).
/// </summary>
internal static class DbConfig
{
    private const string DefaultConnectionString =
        @"Data Source=.;Initial Catalog=EduManagerDB;Integrated Security=True;TrustServerCertificate=True";

    public static string ConnectionString
    {
        get
        {
            string? fromEnvironment = Environment.GetEnvironmentVariable("EDUMANAGER_CONNSTR");
            return string.IsNullOrWhiteSpace(fromEnvironment) ? DefaultConnectionString : fromEnvironment;
        }
    }
}
