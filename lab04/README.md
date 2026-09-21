# BÁO CÁO THỰC HÀNH LAB 04

## CHỦ ĐỀ: XỬ LÝ NGOẠI LỆ, DELEGATE/EVENT, FUNC VÀ GENERIC TỔNG QUÁT TRONG C#

* **Học phần:** 2611COMP101904 - Lập trình trên Windows
* **Sinh viên thực hiện:** Lê Quốc Tuấn
* **MSSV:** 51.01.104.115
* **Lớp:** 51.01.CNTT.A
* **Môi trường phát triển:** Microsoft Visual Studio - C# Console App
* **Mô hình kiến trúc:** Generic Repository & Layered Architecture (OOP)

---

## 1. Mục tiêu và Kiến trúc hệ thống

Dự án Xây dựng Hệ thống Quản lý Sản phẩm (Product Manager) nhằm hiện thực hóa các kỹ thuật C# nâng cao theo yêu cầu đặc tả:

1. **Kho lưu trữ tổng quát (Generic Repository Pattern):** Thiết lập `Repository<T>` áp dụng điều kiện ràng buộc `where T : IEntity`. Lớp này đảm nhận toàn bộ thao tác thêm, xóa, tra cứu thực thể trong bộ nhớ mà không phụ thuộc vào đối tượng cụ thể.
2. **Cơ chế Hướng sự kiện (Event / Action Delegate):** Khai báo các sự kiện `ProductAdded` và `ProductRemoved` tại `ProductService`. Khi có thao tác thêm hoặc xóa thành công, hệ thống tự động phát tín hiệu (trigger event) để tầng hiển thị (`Program.cs`) phản hồi thông báo màu sắc ra Console.
3. **Biểu thức Lambda & Func Delegate:** Sử dụng `Func<Product, bool>` làm tham số nhận điều kiện lọc (Predicate) cho các hàm tìm kiếm theo tên và lọc theo dải giá `Filter(double giaMin, double giaMax)`.
4. **Xử lý ngoại lệ tùy chỉnh (Custom Exceptions):** Định nghĩa hai lớp ngoại lệ mở rộng `DuplicateProductException` (khi trùng mã) và `ProductNotFoundException` (khi không tìm thấy mã). Toàn bộ thao tác nhập xuất được bao bọc bởi `try-catch` đảm bảo chương trình không bị crash bất thường.
5. **Ràng buộc đóng gói dữ liệu (Encapsulation):** Lớp `Product` kiểm soát nghiêm ngặt hai thuộc tính `Price` và `Quantity`, tự động hiệu chỉnh giá trị không âm.

---

## 2. Minh chứng kết quả kiểm thử (Test Cases)

### 2.1. Thêm mới sản phẩm và Kích hoạt Event
Thêm sản phẩm thành công, hệ thống tự động phát Event báo tín hiệu chữ màu xanh trên màn hình Console:
![Thêm sản phẩm](./images/01_them_san_pham.png)

### 2.2. Kiểm soát trùng lặp khóa chính (Primary Key Validation)
Bắt ngoại lệ `DuplicateProductException` và đưa ra thông báo lỗi khi cố tình nhập trùng `MaSP` đã có trong hệ thống:
![Lỗi trùng mã sản phẩm](./images/02_trung_ma_sp.png)

### 2.3. Ràng buộc toàn vẹn dữ liệu nhập (Validation & Exception)
Tự động bắt lỗi nhập sai định dạng số (`FormatException`) hoặc dữ liệu đơn giá/số lượng không hợp lệ mà không làm dừng chương trình:
![Kiểm tra dữ liệu nhập](./images/03_validate_nhap_lieu.png)

### 2.4. Xuất danh sách & Tìm kiếm theo mã/tên
Tra cứu thực thể trong Generic Repository theo mã hoặc sử dụng biểu thức Lambda tìm kiếm từ khóa họ tên:
![Xuất danh sách và Tìm kiếm](./images/04_xuat_tim_kiem.png)

### 2.5. Xử lý ngoại lệ và hiển thị thông báo lỗi (Error Handling)
Hệ thống bắt các ngoại lệ dị thường (`ProductNotFoundException`, `ArgumentException`, `FormatException`), hiển thị thông báo lỗi chi tiết trên Console và giữ cho chương trình hoạt động ổn định:
![Xử lý lỗi](./images/09_xu_ly_loi.png)

### 2.6. Lọc dữ liệu linh hoạt qua Func<Product, bool>
Trích xuất danh sách các sản phẩm thỏa mãn khoảng giá `[Min - Max]` bằng việc truyền `Func` vào Repository:
![Lọc theo khoảng giá](./images/05_loc_khoang_gia.png)

### 2.7. Xóa sản phẩm thành công khi tìm thấy mã (Chức năng 6)
Nhập mã sản phẩm cần xóa, hệ thống xác thực sự tồn tại của thực thể trong Generic Repository, thực hiện xóa khỏi bộ nhớ và kích hoạt `ProductRemoved` Event phát thông báo xác nhận:
![Xóa sản phẩm thành công](./images/11_xoa_san_pham_thanh_cong.png)

### 2.8. Thống kê tổng giá trị kho & Xóa sản phẩm
Tính tổng giá trị kho (`Price * Quantity`) và bẫy lỗi `ProductNotFoundException` khi thực hiện xóa một mã không tồn tại:
![Tính tổng kho và Xóa sản phẩm](./images/06_tong_kho_xoa_sp.png)

### 2.9. Bẫy ngoại lệ và xử lý lỗi hệ thống (Exception Handling)
Chương trình bao bọc toàn bộ luồng I/O bằng cấu trúc `try-catch`, bắt chính xác các lớp ngoại lệ (`ProductNotFoundException`, `ArgumentException`, `FormatException`), hiển thị thông báo trực quan và giữ chương trình luôn hoạt động ổn định:
![Xử lý ngoại lệ](./images/10_xu_ly_loi_he_thong.png)
