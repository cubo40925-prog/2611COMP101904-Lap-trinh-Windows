### Lab02 - C# cơ bản: Quản lý mảng số nguyên bằng Console

## Thông tin sinh viên
* **Họ tên:** Lê Quốc Tuấn
* **MSSV:** 51.01.104.115
* **Lớp:** 51.01.CNTT.A

## Mô tả
Ứng dụng C# Console App cho phép quản lý một mảng số nguyên thông qua menu điều khiển. Chương trình được tổ chức thành các phương thức nhỏ, hỗ trợ nhập/xuất, tính toán, tìm kiếm, sắp xếp và kiểm tra dữ liệu đầu vào.

## Chức năng
* **1. Nhập mảng:** Nhập số lượng phần tử $n$ ($n > 0$) và các giá trị trong mảng.
* **2. Xuất mảng:** In toàn bộ các phần tử hiện có trong mảng ra màn hình.
* **3. Tính tổng:** Tính và xuất tổng tất cả các phần tử trong mảng.
* **4. Tìm lớn nhất và nhỏ nhất:** Tìm giá trị $Max$ và $Min$ của mảng.
* **5. Đếm chẵn/lẻ:** Thống kê số lượng phần tử chẵn và số lượng phần tử lẻ.
* **6. Sắp xếp tăng dần:** Sắp xếp mảng theo thứ tự tăng dần và in kết quả.
* **7. Tìm kiếm:** Nhập giá trị $x$ và xuất vị trí xuất hiện đầu tiên (nếu có).
* **0. Thoát:** Kết thúc chương trình.

## Cách chạy
1. Mở file `lab2.slnx` (hoặc `lab2.csproj`) bằng Visual Studio.
2. Nhấn `F5` (hoặc nút Start) để biên dịch và chạy chương trình.

## Hình ảnh minh họa chương trình

### 1) Mẫu Menu chính

![MENU](./images/MENU.png)

### 2) Nhập và xuất mảng
- Testcase 1: n = 4, 7 4 8 2

![NHAP_XUAT_TC1](./images/IN_OUT_TC1.png)

- Testcase 2: n = 6, -9 -11 -5 10 19 1
  
![NHAP_XUAT_TC2](./images/IN_OUT_TC2.png)

### 3) Tính tổng, tìm Max/Min, đếm chẵn/lẻ
- Testcase 1: n = 4, 7 4 8 2

![SUM_TC1](./images/SUM_TC1.png)

![MAX_MIN_TC1](./images/MAX_MIN_TC1.png)

![EVEN_ODD_TC1](./images/EVEN_ODD_TC1.png)

- Testcase 2: n = 6, -9 -11 -5 10 19 1

![SUM_TC2](./images/SUM_TC2.png)

![MAX_MIN_TC2](./images/MAX_MIN_TC2.png)

![EVEN_ODD_TC2](./images/EVEN_ODD_TC2.png)

### 4) Sắp xếp tăng dần
- Testcase 1: n = 4, 7 4 8 2

![SORT_TC1](./images/SORT_TC1.png)

- Testcase 2: n = 6, -9 -11 -5 10 19 1

![SORT_TC2](./images/SORT_TC2.png)

### 5) Tìm kiếm phần tử
- Testcase 1: n = 4, 7 4 8 2, 2

![FIND_TC1](./images/SORT_TC1.png)

- Testcase 2: n = 6, -9 -11 -5 10 19 1, 11

![FIND_TC2](./images/SORT_TC2.png)

### 6) Kiểm tra lỗi nhập dữ liệu (Validation)
- **Lỗi chọn chức năng xuất trước khi nhập mảng khi lần đầu chạy chương trình**

![ERROR1](./images/ERROR1.png)

- **Lỗi nhập mảng âm**

![ERROR2](./images/ERROR2.png)

- **Lỗi nhập ký tự hay vì số để chọn chức năng**

![ERROR3](./images/ERROR3.png)


