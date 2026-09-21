using System;
using QuanLySanPham;
using System.Globalization;

namespace ProductManager
{
    internal class Program
    {
        private static readonly ProductService _service = new ProductService();

        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Đăng ký lắng nghe event thêm/xóa sản phẩm
            _service.ProductAdded += sp => PrintEvent(sp.ToString());
            _service.ProductRemoved += sp => PrintEvent(sp.ToString());

            bool running = true;
            while (running)
            {
                PrintMenu();
                string? choice = Console.ReadLine();

                try
                {
                    switch (choice?.Trim())
                    {
                        case "1":
                            ThemSanPham();
                            break;
                        case "2":
                            XuatDanhSach();
                            break;
                        case "3":
                            TimTheoMa();
                            break;
                        case "4":
                            TimTheoTen();
                            break;
                        case "5":
                            LocTheoKhoangGia();
                            break;
                        case "6":
                            XoaSanPham();
                            break;
                        case "7":
                            TinhTongGiaTriKho();
                            break;
                        case "0":
                            running = false;
                            Console.WriteLine("Tạm biệt!");
                            break;
                        default:
                            Console.WriteLine(">> Lựa chọn không hợp lệ, vui lòng chọn lại.");
                            break;
                    }
                }
                catch (DuplicateProductException ex)
                {
                    Console.WriteLine($">> Lỗi: {ex.Message}");
                }
                catch (ProductNotFoundException ex)
                {
                    Console.WriteLine($">> Lỗi: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($">> Dữ liệu không hợp lệ: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine(">> Lỗi: Dữ liệu nhập vào không đúng định dạng số.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($">> Đã có lỗi xảy ra: {ex.Message}");
                }

                if (running)
                {
                    Console.WriteLine("\nNhấn Enter để tiếp tục...");
                    Console.ReadLine();
                }
            }
        }

        private static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("===== PRODUCT MANAGER =====");
            Console.WriteLine("1. Them san pham");
            Console.WriteLine("2. Xuat danh sach");
            Console.WriteLine("3. Tim theo ma");
            Console.WriteLine("4. Tim theo ten");
            Console.WriteLine("5. Loc theo khoang gia");
            Console.WriteLine("6. Xoa san pham");
            Console.WriteLine("7. Tinh tong gia tri kho");
            Console.WriteLine("0. Thoat");
            Console.Write("Chon: ");
        }

        private static void PrintEvent(string message)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = oldColor;
        }

        private static void ThemSanPham()
        {
            Console.Write("Nhap ma san pham: ");
            string maSP = (Console.ReadLine() ?? string.Empty).Trim();

            Console.Write("Nhap ten san pham: ");
            string tenSP = (Console.ReadLine() ?? string.Empty).Trim();

            Console.Write("Nhap don gia: ");
            double price = (double)ReadDecimal();

            Console.Write("Nhap so luong: ");
            int quantity = ReadInt();

            var product = new Product(maSP, tenSP, price, quantity);
            _service.AddProduct(product);
        }

        private static void XuatDanhSach()
        {
            var list = _service.GetAll();
            if (list.Count == 0)
            {
                Console.WriteLine(">> Danh sách sản phẩm đang trống.");
                return;
            }

            Console.WriteLine($"--- Danh sách sản phẩm ({list.Count}) ---");
            foreach (var p in list)
                Console.WriteLine(p);
        }

        private static void TimTheoMa()
        {
            Console.Write("Nhap ma san pham can tim: ");
            string maSP = (Console.ReadLine() ?? string.Empty).Trim();

            var product = _service.FindByMaSP(maSP);
            if (product == null)
                Console.WriteLine($">> Không tìm thấy sản phẩm có mã '{maSP}'.");
            else
                Console.WriteLine(product);
        }

        private static void TimTheoTen()
        {
            Console.Write("Nhap tu khoa ten san pham: ");
            string keyword = (Console.ReadLine() ?? string.Empty).Trim();

            var results = _service.Search(keyword);
            if (results.Count == 0)
            {
                Console.WriteLine(">> Không có sản phẩm nào phù hợp.");
                return;
            }

            Console.WriteLine($"--- Tìm thấy {results.Count} sản phẩm ---");
            foreach (var p in results)
                Console.WriteLine(p);
        }

        private static void LocTheoKhoangGia()
        {
            Console.Write("Nhap gia nho nhat: ");
            double minPrice = (double)ReadDecimal();

            Console.Write("Nhap gia lon nhat: ");
            double maxPrice = (double)ReadDecimal();

            if (minPrice > maxPrice)
                throw new ArgumentException("Giá nhỏ nhất không được lớn hơn giá lớn nhất.");

            var results = _service.Filter(minPrice, maxPrice);
            if (results.Count == 0)
            {
                Console.WriteLine(">> Không có sản phẩm nào trong khoảng giá này.");
                return;
            }

            Console.WriteLine($"--- Có {results.Count} sản phẩm trong khoảng giá [{minPrice:N0} - {maxPrice:N0}] ---");
            foreach (var p in results)
                Console.WriteLine(p);
        }

        private static void XoaSanPham()
        {
            Console.Write("Nhap ma san pham can xoa: ");
            string maSP = (Console.ReadLine() ?? string.Empty).Trim();

            _service.RemoveProduct(maSP);
        }

        private static void TinhTongGiaTriKho()
        {
            double total = _service.GetTotalInventoryValue();
            Console.WriteLine($">> Tổng giá trị kho hiện tại: {total:N0}");
        }

        private static decimal ReadDecimal()
        {
            string input = Console.ReadLine() ?? string.Empty;
            if (!decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal value))
                throw new FormatException("Giá trị nhập vào không phải là số hợp lệ.");
            return value;
        }

        private static int ReadInt()
        {
            string input = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(input, out int value))
                throw new FormatException("Giá trị nhập vào không phải là số nguyên hợp lệ.");
            return value;
        }
    }
}