/* ============================================================================
   COMP1019 - Lập trình trên Windows | Lab 10
   FILE 02: NHẬP DỮ LIỆU MẪU CHO EduManagerDB

   Phải chạy 01_CreateDatabase.sql trước.
   Thứ tự nhập bắt buộc theo quan hệ khóa ngoại:
   KHOA -> LOP -> SINHVIEN ; MONHOC ; KETQUA (sau SINHVIEN và MONHOC) ; TAIKHOAN

   Số lượng: 2 khoa, 5 lớp, 8 sinh viên, 5 môn học, 16 kết quả, 3 tài khoản
   (đề yêu cầu tối thiểu 2 / 3 / 6 / 4 / 8 / 2).
   Dữ liệu được thiết kế có chủ ý để các truy vấn ở file 03 có kết quả đáng xem:
     - CNTT03 chưa có sinh viên nào           -> thấy LEFT JOIN trả về 0
     - SPTIN02 chỉ có 1 sinh viên             -> bị loại khi lọc HAVING >= 2
     - SV007 chưa có điểm                     -> xóa được (câu 6) và xuất hiện ở truy vấn "chưa có điểm"
     - SV008 điểm thấp                        -> có xếp loại "Yếu"
   ============================================================================ */

USE EduManagerDB;
GO

-- 1. KHOA ---------------------------------------------------------------------
INSERT INTO KHOA (MaKhoa, TenKhoa, DienThoai) VALUES
('FIT',  N'Khoa Công nghệ Thông tin', '0281234567'),
('MATH', N'Khoa Toán - Tin học',      '0287654321');

-- 2. LOP (mỗi lớp tham chiếu một khoa đã có) -----------------------------------
INSERT INTO LOP (MaLop, TenLop, MaKhoa) VALUES
('CNTT01',  N'Công nghệ thông tin 01', 'FIT'),
('CNTT02',  N'Công nghệ thông tin 02', 'FIT'),
('CNTT03',  N'Công nghệ thông tin 03', 'FIT'),     -- lớp mới, chưa có sinh viên
('SPTIN01', N'Sư phạm Tin học 01',     'MATH'),
('SPTIN02', N'Sư phạm Tin học 02',     'MATH');

-- 3. SINHVIEN (mỗi sinh viên tham chiếu một lớp đã có) -------------------------
INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop, Email, SoDienThoai) VALUES
('SV001', N'Nguyễn Văn An',    '2005-02-10', N'Nam', 'CNTT01',  'sv001@student.edu.vn', '0901000001'),
('SV002', N'Trần Thị Bình',    '2005-08-21', N'Nữ',  'CNTT01',  'sv002@student.edu.vn', '0901000002'),
('SV003', N'Lê Minh Chi',      '2004-12-02', N'Nữ',  'CNTT02',  'sv003@student.edu.vn', '0901000003'),
('SV004', N'Phạm Quốc Dũng',   '2005-04-16', N'Nam', 'CNTT02',  'sv004@student.edu.vn', '0901000004'),
('SV005', N'Hoàng Gia Hân',    '2005-10-30', N'Nữ',  'SPTIN01', 'sv005@student.edu.vn', '0901000005'),
('SV006', N'Võ Thành Khang',   '2004-09-09', N'Nam', 'SPTIN01', 'sv006@student.edu.vn', '0901000006'),
('SV007', N'Đặng Thu Trang',   '2005-06-18', N'Nữ',  'CNTT01',  'sv007@student.edu.vn', '0901000007'),  -- chưa có điểm
('SV008', N'Bùi Ngọc Lan',     '2005-03-25', N'Nữ',  'SPTIN02', 'sv008@student.edu.vn', '0901000008');

-- 4. MONHOC ---------------------------------------------------------------------
INSERT INTO MONHOC (MaMH, TenMH, SoTinChi) VALUES
('COMP1019', N'Lập trình trên Windows',        3),
('COMP1017', N'Lập trình cơ bản',              3),
('COMP1020', N'Cơ sở dữ liệu',                 3),
('COMP1044', N'Nhập môn Công nghệ phần mềm',   3),
('MATH1001', N'Giải tích 1',                   4);

-- 5. KETQUA (tham chiếu sinh viên và môn học đã có; điểm từ 0 đến 10) ------------
INSERT INTO KETQUA (MaSV, MaMH, Diem) VALUES
('SV001', 'COMP1019', 8.5),
('SV001', 'COMP1020', 7.5),
('SV001', 'COMP1017', 9.0),
('SV002', 'COMP1019', 9.0),
('SV002', 'COMP1020', 8.0),
('SV002', 'MATH1001', 8.5),
('SV003', 'COMP1019', 6.5),
('SV003', 'COMP1017', 7.0),
('SV004', 'COMP1019', 7.0),
('SV004', 'COMP1020', 5.5),
('SV005', 'COMP1017', 8.0),
('SV005', 'MATH1001', 7.5),
('SV006', 'COMP1044', 7.8),
('SV006', 'COMP1017', 6.0),
('SV008', 'COMP1019', 4.5),
('SV008', 'MATH1001', 5.0);

-- 6. TAIKHOAN (mật khẩu demo, ứng dụng thật phải băm) ----------------------------
INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, HoTen, VaiTro, TrangThai) VALUES
('admin',   '123456', N'Quản trị viên',    'Admin', 1),
('gv01',    '123456', N'Giảng viên demo',  'User',  1),
('sv_demo', '123456', N'Sinh viên demo',   'User',  0);      -- tài khoản đang bị khóa
GO


/* ----------------------------------------------------------------------------
   KIỂM TRA SỐ DÒNG SAU KHI NHẬP (chụp ảnh kết quả này cho báo cáo)
   Kết quả đúng: KHOA 2, LOP 5, SINHVIEN 8, MONHOC 5, KETQUA 16, TAIKHOAN 3
   ---------------------------------------------------------------------------- */
SELECT N'KHOA'     AS Bang, COUNT(*) AS SoDong FROM KHOA
UNION ALL SELECT N'LOP',      COUNT(*) FROM LOP
UNION ALL SELECT N'SINHVIEN', COUNT(*) FROM SINHVIEN
UNION ALL SELECT N'MONHOC',   COUNT(*) FROM MONHOC
UNION ALL SELECT N'KETQUA',   COUNT(*) FROM KETQUA
UNION ALL SELECT N'TAIKHOAN', COUNT(*) FROM TAIKHOAN;
GO
