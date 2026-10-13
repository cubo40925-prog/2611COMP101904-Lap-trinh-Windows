#  HỆ THỐNG QUẢN LÝ CỬA HÀNG MINI (SHOP MANAGER MINI)

* **Học phần:** 2611COMP101904 - Lập trình trên Windows
* **Giảng viên hướng dẫn:** ThS. Lê Thanh Thoại
* **Sinh viên thực hiện:** Lê Quốc Tuấn - 51.01.104.115
* **Môi trường phát triển:** Microsoft Visual Studio - C# Windows Forms App
* **Tên ứng dụng:** `ShopManagerMini`

---

## 1. Giới thiệu và Kiến trúc Hệ thống

Dự án `ShopManagerMini` được xây dựng nhằm hiện thực hóa giải pháp quản lý cửa hàng mini nhiều Form, áp dụng mô hình kiến trúc đa tài liệu MDI (Multiple Document Interface) và luồng điều khiển hướng sự kiện:

- **Mô hình kiến trúc MDI Parent - Child:** `FrmMain` đóng vai trò là Form cha chứa (`IsMdiContainer = true`), quản lý vòng đời và không gian hiển thị của các Form con bên trong vùng làm việc, ngăn chặn việc phân mảnh cửa sổ độc lập ngoài màn hình desktop.
- **Cơ chế xác thực và Điều phối luồng khởi động (Modal Authentication Flow):** Sử dụng `FrmLogin` chạy dưới dạng hộp thoại độc quyền (`ShowDialog()`). Dữ liệu người dùng được đóng gói trong đối tượng `User` và truyền sang Form chính `FrmMain` thông qua constructor.
- **Quản lý Form con đơn lẻ (Singleton Child Navigation):** Kiểm tra tập hợp `this.MdiChildren` thông qua hàm `DaMoForm(Type)` nhằm kích hoạt (`Activate()`) lại thể hiện sẵn có thay vì khởi tạo trùng lặp khi người dùng chọn menu nhiều lần.
- **Lưu trữ dữ liệu trong bộ nhớ (In-Memory Data Binding):** Toàn bộ thực thể `User`, `Product`, `Customer` được quản lý bằng cấu trúc danh sách liên kết trong bộ nhớ và đồng bộ hiển thị lên `DataGridView`.

---

## 2. Đặc tả Danh mục Form và Chức năng

| Tên Form | Vai trò kiến trúc | Trách nhiệm chức năng chính |
| :--- | :--- | :--- |
| `FrmLogin` | Modal Form | Tiếp nhận xác thực tài khoản mẫu, bẫy lỗi nhập liệu, trả `DialogResult.OK` khi hợp lệ. |
| `FrmMain` | MDI Container | Form trung tâm chứa `MenuStrip`, `StatusStrip` hiển thị định danh/vai trò người dùng và quản lý Form con. |
| `FrmProduct` | MDI Child | Quản lý danh mục sản phẩm (Thêm, Sửa, Xóa, Tìm kiếm), hiển thị `DataGridView` và bẫy lỗi ràng buộc. |
| `FrmCustomer` | MDI Child | Quản lý danh sách khách hàng, hiển thị bảng thông tin đối tác và thêm mới dữ liệu. |
| `FrmAbout` | Child Form | Hiển thị thông tin bản quyền ứng dụng, thông tin sinh viên thực hiện và lớp học phần. |

---

## 3. Minh chứng Thực nghiệm và Kiểm thử Hệ thống (Test Cases)

### 3.1. Giao diện Đăng nhập hệ thống (`FrmLogin`)
- Giao diện khởi tạo màn hình đăng nhập:
  ![Giao diện đăng nhập](Images/login.png)
- Đăng nhập thành công và điều phối dữ liệu người dùng lên thanh `StatusStrip`:
  ![Đăng nhập thành công](Images/02_DangNhap_ThanhCong.png)

### 3.2. Nhóm Minh chứng Lỗi Không Nhập Thông tin (Validation & Error Handling)
Hệ thống kiểm soát chặt chẽ tính hợp lệ của dữ liệu đầu vào, kích hoạt hộp thoại cảnh báo khi người dùng bỏ trống hoặc vi phạm quy tắc nghiệp vụ:
- Cảnh báo khi bỏ trống thông tin đăng nhập:
  ![Lỗi trống thông tin đăng nhập](Images/01_DangNhap.png)
- Cảnh báo yêu cầu nhập đầy đủ tài khoản và mật khẩu:
  ![Cảnh báo lỗi nhập liệu đăng nhập](Images/Screenshot_2026-10-13_161709.png)
- Kiểm soát lỗi ràng buộc dữ liệu đầu vào tại Form Quản lý Sản phẩm (`FrmProduct`):
  ![Bắt lỗi sản phẩm](Images/Screenshot_2026-10-13_161732.png)

### 3.3. Quản lý Không gian MDI Container (`FrmMain`)
Khởi tạo và quản lý đồng thời các Form nghiệp vụ con bên trong vùng làm việc của `FrmMain`, không mở thành các cửa sổ rời rạc:
![Kiến trúc MDI](Images/03_MDI_ChuaNhieuFormCon.png)

### 3.4. Quản lý Danh mục Sản phẩm và Khách hàng
- Giao diện quản lý danh mục sản phẩm:
  ![Quản lý sản phẩm](Images/04_Product.png)
- Giao diện quản lý danh sách khách hàng:
  ![Quản lý khách hàng](Images/05_Custome.png)

### 3.5. Thông tin Bản quyền và Xác nhận Thao tác An toàn
- Màn hình `FrmAbout` hiển thị thông tin hệ thống và định danh sinh viên:
  ![Thông tin giới thiệu](Images/06_FrmAbout_ThongTin.png)
- Hộp thoại xác nhận an toàn khi thực thi thao tác xóa hoặc thoát chương trình:
  ![Xác nhận an toàn](Images/07_XacNhan_Xoa_Thoat.png)
