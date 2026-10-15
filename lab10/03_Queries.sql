/* ============================================================================
   COMP1019 - Lập trình trên Windows | Lab 10
   FILE 03: TRUY VẤN VÀ KIỂM THỬ RÀNG BUỘC TRÊN EduManagerDB

   Phải chạy 01 và 02 trước. Có thể chạy cả file (F5) hoặc bôi đen từng câu rồi chạy (F5).
   Cấu trúc:
     PHẦN A  Câu 1-4    SELECT, JOIN, LIKE, WHERE
     PHẦN B  Câu 5-6    UPDATE, DELETE (chạy trong transaction + ROLLBACK nên dữ liệu không bị đổi)
     PHẦN C  Câu 7-10   JOIN, GROUP BY, HAVING
     PHẦN D  Câu 11-12  Kiểm thử lỗi ràng buộc (mỗi câu một batch, SQL Server PHẢI báo lỗi màu đỏ)
     PHẦN E  Truy vấn bổ sung (xếp loại, xếp hạng, thống kê theo môn/lớp/khoa, ...)
     PHẦN F  Kiểm thử ràng buộc tự động: in bảng PASS/FAIL cho 14 trường hợp
   ============================================================================ */

USE EduManagerDB;
GO


/* ############################################################################
   PHẦN A. TRUY VẤN CƠ BẢN
   ############################################################################ */

-- Câu 1. Liệt kê toàn bộ sinh viên ---------------------------------------------
SELECT MaSV, HoTen, NgaySinh, GioiTinh, MaLop
FROM SINHVIEN
ORDER BY MaSV;
GO

-- Câu 2. Liệt kê sinh viên kèm tên lớp và tên khoa (JOIN 3 bảng) ---------------
-- INNER JOIN: chỉ lấy dòng có dữ liệu khớp ở cả hai bảng.
SELECT sv.MaSV, sv.HoTen, l.TenLop, k.TenKhoa
FROM SINHVIEN sv
INNER JOIN LOP  l ON sv.MaLop = l.MaLop
INNER JOIN KHOA k ON l.MaKhoa = k.MaKhoa
ORDER BY k.TenKhoa, l.TenLop, sv.MaSV;
GO

-- Câu 3. Tìm sinh viên theo một phần họ tên bằng LIKE -------------------------
-- Dấu % đại diện cho chuỗi ký tự bất kỳ. Database dùng collation CI nên không phân biệt hoa/thường.
-- Chữ N trước chuỗi: hằng chuỗi Unicode (bắt buộc khi có tiếng Việt có dấu).
SELECT MaSV, HoTen, MaLop
FROM SINHVIEN
WHERE HoTen LIKE N'%An%'
ORDER BY HoTen ASC;

-- Biến thể: tìm theo họ (bắt đầu bằng "Nguyễn") và theo tên (kết thúc bằng "Lan")
SELECT MaSV, HoTen FROM SINHVIEN WHERE HoTen LIKE N'Nguyễn%';
SELECT MaSV, HoTen FROM SINHVIEN WHERE HoTen LIKE N'%Lan';
GO

-- Câu 4. Liệt kê sinh viên theo mã lớp -----------------------------------------
SELECT MaSV, HoTen, GioiTinh
FROM SINHVIEN
WHERE MaLop = 'CNTT01'
ORDER BY MaSV;
GO


/* ############################################################################
   PHẦN B. UPDATE VÀ DELETE
   Mỗi khối chạy trong BEGIN TRANSACTION ... ROLLBACK để vừa xem được kết quả
   thay đổi, vừa giữ nguyên dữ liệu gốc (chạy lại file nhiều lần vẫn đúng).
   Muốn thay đổi thật: đổi ROLLBACK TRANSACTION thành COMMIT TRANSACTION.
   QUY TẮC: luôn kiểm tra điều kiện WHERE trước khi UPDATE/DELETE. Thiếu WHERE là đổi/xóa TOÀN BẢNG.
   ############################################################################ */

-- Câu 5. Cập nhật họ tên hoặc lớp của một sinh viên ----------------------------
DECLARE @rows INT;

BEGIN TRANSACTION;

    -- 5a. Đổi họ tên SV001
    UPDATE SINHVIEN
    SET HoTen = N'Nguyễn Văn An (đã sửa)'
    WHERE MaSV = 'SV001';
    SET @rows = @@ROWCOUNT;
    PRINT CONCAT(N'5a. Số dòng bị ảnh hưởng khi đổi họ tên: ', @rows);

    -- 5b. Chuyển SV002 sang lớp CNTT02 (lớp phải tồn tại, nếu không sẽ vi phạm khóa ngoại)
    UPDATE SINHVIEN
    SET MaLop = 'CNTT02'
    WHERE MaSV = 'SV002';
    SET @rows = @@ROWCOUNT;
    PRINT CONCAT(N'5b. Số dòng bị ảnh hưởng khi đổi lớp: ', @rows);

    -- Xem kết quả SAU khi sửa (còn trong transaction)
    SELECT MaSV, HoTen, MaLop FROM SINHVIEN WHERE MaSV IN ('SV001', 'SV002');

ROLLBACK TRANSACTION;   -- hủy thay đổi; đổi thành COMMIT TRANSACTION nếu muốn giữ

-- Xem lại SAU rollback: dữ liệu đã trở về như cũ
SELECT MaSV, HoTen, MaLop FROM SINHVIEN WHERE MaSV IN ('SV001', 'SV002');
GO

-- Câu 6. Xóa một sinh viên CHƯA có dữ liệu điểm ---------------------------------
-- Điều kiện NOT EXISTS bảo đảm chỉ xóa khi sinh viên không có dòng nào trong KETQUA.
DECLARE @rows INT;

BEGIN TRANSACTION;

    -- 6a. SV007 chưa có điểm -> xóa được (1 dòng)
    DELETE sv
    FROM SINHVIEN sv
    WHERE sv.MaSV = 'SV007'
      AND NOT EXISTS (SELECT 1 FROM KETQUA kq WHERE kq.MaSV = sv.MaSV);
    SET @rows = @@ROWCOUNT;
    PRINT CONCAT(N'6a. SV007 (chưa có điểm): số dòng đã xóa = ', @rows);

    -- 6b. SV003 đã có điểm -> điều kiện NOT EXISTS sai nên KHÔNG xóa gì (0 dòng)
    DELETE sv
    FROM SINHVIEN sv
    WHERE sv.MaSV = 'SV003'
      AND NOT EXISTS (SELECT 1 FROM KETQUA kq WHERE kq.MaSV = sv.MaSV);
    SET @rows = @@ROWCOUNT;
    PRINT CONCAT(N'6b. SV003 (đã có điểm): số dòng đã xóa = ', @rows);

    -- Danh sách còn lại trong transaction: không còn SV007, vẫn còn SV003
    SELECT MaSV, HoTen, MaLop FROM SINHVIEN ORDER BY MaSV;

ROLLBACK TRANSACTION;   -- khôi phục lại SV007

-- Kiểm tra: SV007 đã trở lại
SELECT MaSV, HoTen FROM SINHVIEN WHERE MaSV = 'SV007';
GO


/* ############################################################################
   PHẦN C. JOIN, GROUP BY, HAVING
   ############################################################################ */

-- Câu 7. Liệt kê điểm của từng sinh viên theo từng môn --------------------------
SELECT sv.MaSV, sv.HoTen, mh.TenMH, kq.Diem
FROM KETQUA kq
INNER JOIN SINHVIEN sv ON kq.MaSV = sv.MaSV
INNER JOIN MONHOC   mh ON kq.MaMH = mh.MaMH
ORDER BY sv.MaSV, mh.TenMH;
GO

-- Câu 8. Tính điểm trung bình của từng sinh viên -------------------------------
-- GROUP BY gom các dòng điểm theo sinh viên rồi AVG tính trung bình mỗi nhóm.
-- Nhân 1.0 để AVG tính bằng số thập phân chính xác, CAST làm tròn còn 2 chữ số.
SELECT sv.MaSV, sv.HoTen,
       COUNT(kq.MaMH)                           AS SoMon,
       CAST(AVG(kq.Diem * 1.0) AS DECIMAL(4,2)) AS DiemTrungBinh
FROM SINHVIEN sv
INNER JOIN KETQUA kq ON sv.MaSV = kq.MaSV
GROUP BY sv.MaSV, sv.HoTen
ORDER BY DiemTrungBinh DESC;
GO

-- Câu 9. Thống kê số sinh viên theo từng lớp -----------------------------------
-- LEFT JOIN để lớp chưa có sinh viên (CNTT03) vẫn hiện với số lượng 0.
-- Phải đếm COUNT(sv.MaSV) chứ không phải COUNT(*) (COUNT(*) sẽ đếm cả dòng NULL thành 1).
SELECT l.MaLop, l.TenLop, COUNT(sv.MaSV) AS SoSinhVien
FROM LOP l
LEFT JOIN SINHVIEN sv ON l.MaLop = sv.MaLop
GROUP BY l.MaLop, l.TenLop
ORDER BY SoSinhVien DESC, l.MaLop;
GO

-- Câu 10. Liệt kê các lớp có từ 2 sinh viên trở lên -----------------------------
-- WHERE lọc dòng TRƯỚC khi gom nhóm; HAVING lọc nhóm SAU khi gom nhóm.
SELECT l.MaLop, l.TenLop, COUNT(sv.MaSV) AS SoSinhVien
FROM LOP l
LEFT JOIN SINHVIEN sv ON l.MaLop = sv.MaLop
GROUP BY l.MaLop, l.TenLop
HAVING COUNT(sv.MaSV) >= 2
ORDER BY SoSinhVien DESC, l.MaLop;
GO


/* ############################################################################
   PHẦN D. KIỂM THỬ LỖI RÀNG BUỘC
   Mỗi câu nằm trong một batch riêng (cách nhau bởi GO) nên khi gặp lỗi, các câu sau vẫn chạy.
   KẾT QUẢ ĐÚNG là SQL Server BÁO LỖI (Messages có chữ đỏ). Chụp ảnh các thông báo lỗi này.
   ############################################################################ */

-- Câu 11. Thêm sinh viên vào lớp không tồn tại
-- Lỗi mong đợi: Msg 547 - The INSERT statement conflicted with the FOREIGN KEY constraint "FK_SINHVIEN_LOP"
INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop)
VALUES ('SV999', N'Sinh viên lỗi', '2005-01-01', N'Nam', 'CNTT99');
GO

-- Câu 12. Nhập điểm ngoài khoảng 0 đến 10
-- Lỗi mong đợi: Msg 547 - The INSERT statement conflicted with the CHECK constraint "CK_KETQUA_Diem"
-- (SV007 và COMP1019 đều có thật, chưa có điểm với nhau nên chỉ riêng điểm 12 là sai)
INSERT INTO KETQUA (MaSV, MaMH, Diem)
VALUES ('SV007', 'COMP1019', 12);
GO

-- Câu 12b. Điểm âm
-- Lỗi mong đợi: Msg 547 - CHECK constraint "CK_KETQUA_Diem"
INSERT INTO KETQUA (MaSV, MaMH, Diem)
VALUES ('SV007', 'COMP1019', -1);
GO

-- Câu 13 (thêm). Giới tính không thuộc Nam/Nữ
-- Lỗi mong đợi: Msg 547 - CHECK constraint "CK_SINHVIEN_GioiTinh"
INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop)
VALUES ('SV998', N'Sinh viên giới tính lỗi', '2005-01-01', N'Khác', 'CNTT01');
GO

-- Câu 14 (thêm). Trùng khóa chính
-- Lỗi mong đợi: Msg 2627 - Violation of PRIMARY KEY constraint "PK_SINHVIEN"
INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop)
VALUES ('SV001', N'Trùng mã', '2005-01-01', N'Nam', 'CNTT01');
GO

-- Câu 15 (thêm). Số tín chỉ không hợp lệ
-- Lỗi mong đợi: Msg 547 - CHECK constraint "CK_MONHOC_SoTinChi"
INSERT INTO MONHOC (MaMH, TenMH, SoTinChi)
VALUES ('TEST0001', N'Môn học lỗi', 0);
GO

-- Câu 16 (thêm). Xóa sinh viên đang có điểm: khóa ngoại KETQUA -> SINHVIEN chặn lại
-- Lỗi mong đợi: Msg 547 - The DELETE statement conflicted with the REFERENCE constraint "FK_KETQUA_SINHVIEN"
DELETE FROM SINHVIEN WHERE MaSV = 'SV001';
GO


/* ############################################################################
   PHẦN E. TRUY VẤN BỔ SUNG
   ############################################################################ */

-- E1. Xếp loại và xếp hạng sinh viên theo điểm trung bình
-- CASE: rẽ nhánh theo điều kiện. RANK() OVER: xếp hạng, điểm bằng nhau thì cùng hạng.
SELECT sv.MaSV, sv.HoTen, l.TenLop,
       CAST(AVG(kq.Diem * 1.0) AS DECIMAL(4,2)) AS DiemTB,
       CASE WHEN AVG(kq.Diem * 1.0) >= 8   THEN N'Giỏi'
            WHEN AVG(kq.Diem * 1.0) >= 6.5 THEN N'Khá'
            WHEN AVG(kq.Diem * 1.0) >= 5   THEN N'Trung bình'
            ELSE N'Yếu' END                         AS XepLoai,
       RANK() OVER (ORDER BY AVG(kq.Diem * 1.0) DESC) AS Hang
FROM SINHVIEN sv
INNER JOIN LOP    l  ON sv.MaLop = l.MaLop
INNER JOIN KETQUA kq ON sv.MaSV = kq.MaSV
GROUP BY sv.MaSV, sv.HoTen, l.TenLop
ORDER BY Hang, sv.MaSV;
GO

-- E2. Top 3 sinh viên có điểm trung bình cao nhất
SELECT TOP (3) sv.MaSV, sv.HoTen, CAST(AVG(kq.Diem * 1.0) AS DECIMAL(4,2)) AS DiemTB
FROM SINHVIEN sv
INNER JOIN KETQUA kq ON sv.MaSV = kq.MaSV
GROUP BY sv.MaSV, sv.HoTen
ORDER BY AVG(kq.Diem * 1.0) DESC;
GO

-- E3. Sinh viên CHƯA có điểm môn nào (LEFT JOIN rồi lọc IS NULL)
SELECT sv.MaSV, sv.HoTen, sv.MaLop
FROM SINHVIEN sv
LEFT JOIN KETQUA kq ON sv.MaSV = kq.MaSV
WHERE kq.MaSV IS NULL;
GO

-- E4. Thống kê theo môn học: số sinh viên đã có điểm, điểm cao nhất, thấp nhất, trung bình
SELECT mh.MaMH, mh.TenMH, mh.SoTinChi,
       COUNT(kq.MaSV)                            AS SoSinhVien,
       MAX(kq.Diem)                              AS DiemCaoNhat,
       MIN(kq.Diem)                              AS DiemThapNhat,
       CAST(AVG(kq.Diem * 1.0) AS DECIMAL(4,2))  AS DiemTrungBinh
FROM MONHOC mh
LEFT JOIN KETQUA kq ON mh.MaMH = kq.MaMH
GROUP BY mh.MaMH, mh.TenMH, mh.SoTinChi
ORDER BY SoSinhVien DESC, mh.MaMH;
GO

-- E5. Điểm trung bình theo lớp
SELECT l.MaLop, l.TenLop,
       COUNT(DISTINCT sv.MaSV)                   AS SoSinhVien,
       CAST(AVG(kq.Diem * 1.0) AS DECIMAL(4,2))  AS DiemTrungBinh
FROM LOP l
LEFT JOIN SINHVIEN sv ON l.MaLop = sv.MaLop
LEFT JOIN KETQUA   kq ON sv.MaSV = kq.MaSV
GROUP BY l.MaLop, l.TenLop
ORDER BY l.MaLop;
GO

-- E6. Số lớp, số sinh viên và điểm trung bình theo khoa
SELECT k.MaKhoa, k.TenKhoa,
       COUNT(DISTINCT l.MaLop)                   AS SoLop,
       COUNT(DISTINCT sv.MaSV)                   AS SoSinhVien,
       CAST(AVG(kq.Diem * 1.0) AS DECIMAL(4,2))  AS DiemTrungBinh
FROM KHOA k
LEFT JOIN LOP      l  ON k.MaKhoa = l.MaKhoa
LEFT JOIN SINHVIEN sv ON l.MaLop  = sv.MaLop
LEFT JOIN KETQUA   kq ON sv.MaSV  = kq.MaSV
GROUP BY k.MaKhoa, k.TenKhoa
ORDER BY k.MaKhoa;
GO

-- E7. Thống kê sinh viên theo giới tính
SELECT GioiTinh, COUNT(*) AS SoSinhVien
FROM SINHVIEN
GROUP BY GioiTinh;
GO

-- E8. Sinh viên có ít nhất một môn đạt từ 8 điểm trở lên (truy vấn con với IN)
SELECT MaSV, HoTen, MaLop
FROM SINHVIEN
WHERE MaSV IN (SELECT MaSV FROM KETQUA WHERE Diem >= 8)
ORDER BY MaSV;
GO

-- E9. Tuổi của sinh viên (trừ 1 nếu năm nay chưa tới sinh nhật)
SELECT MaSV, HoTen, NgaySinh,
       DATEDIFF(YEAR, NgaySinh, GETDATE())
         - CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, NgaySinh, GETDATE()), NgaySinh) > CAST(GETDATE() AS DATE)
                THEN 1 ELSE 0 END AS Tuoi
FROM SINHVIEN
ORDER BY NgaySinh;
GO

-- E10. Dùng VIEW đã tạo ở file 01 (viết ngắn gọn hơn nhiều)
SELECT * FROM vw_SinhVienChiTiet ORDER BY MaKhoa, MaLop, MaSV;

-- Điểm trung bình thường và điểm trung bình theo tín chỉ (môn 4 tín chỉ có trọng số lớn hơn)
SELECT * FROM vw_DiemTrungBinh ORDER BY DiemTBTinChi DESC;
GO

-- E11. Kiểm tra đăng nhập (mô phỏng ứng dụng WinForms buổi sau): chỉ tài khoản đang hoạt động
SELECT TenDangNhap, HoTen, VaiTro
FROM TAIKHOAN
WHERE TenDangNhap = 'admin' AND MatKhau = '123456' AND TrangThai = 1;
GO


/* ############################################################################
   PHẦN F. KIỂM THỬ RÀNG BUỘC TỰ ĐỘNG (PASS / FAIL)
   Mỗi trường hợp thử một thao tác SAI trong TRY...CATCH:
     - Bị SQL Server chặn (nhảy vào CATCH)  -> PASS (ràng buộc hoạt động đúng)
     - Thực hiện được                       -> FAIL (ràng buộc bị thiếu hoặc sai)
   Toàn bộ chạy trong transaction và ROLLBACK nên không để lại dữ liệu rác.
   Bảng kết quả dùng table variable nên KHÔNG bị ROLLBACK xóa mất.
   ############################################################################ */
DECLARE @Results TABLE (
    STT      INT IDENTITY(1,1),
    TestCase NVARCHAR(200),
    KetQua   NVARCHAR(10),
    ChiTiet  NVARCHAR(500)
);

BEGIN TRANSACTION;

-- T1. Khóa ngoại: sinh viên thuộc lớp không tồn tại
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop)
    VALUES ('T001', N'Test', '2005-01-01', N'Nam', 'CNTT99');
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'FK: sinh viên vào lớp không tồn tại', N'FAIL', N'Dữ liệu sai vẫn được thêm');
END TRY
BEGIN CATCH
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'FK: sinh viên vào lớp không tồn tại', N'PASS', CONCAT(N'Lỗi ', ERROR_NUMBER(), N': ', ERROR_MESSAGE()));
END CATCH;

-- T2. CHECK: điểm lớn hơn 10
BEGIN TRY
    INSERT INTO KETQUA (MaSV, MaMH, Diem) VALUES ('SV007', 'COMP1019', 10.01);
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'CHECK: điểm lớn hơn 10', N'FAIL', N'Dữ liệu sai vẫn được thêm');
END TRY
BEGIN CATCH
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'CHECK: điểm lớn hơn 10', N'PASS', CONCAT(N'Lỗi ', ERROR_NUMBER(), N': ', ERROR_MESSAGE()));
END CATCH;

-- T3. CHECK: điểm âm
BEGIN TRY
    INSERT INTO KETQUA (MaSV, MaMH, Diem) VALUES ('SV007', 'COMP1019', -0.5);
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'CHECK: điểm âm', N'FAIL', N'Dữ liệu sai vẫn được thêm');
END TRY
BEGIN CATCH
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'CHECK: điểm âm', N'PASS', CONCAT(N'Lỗi ', ERROR_NUMBER(), N': ', ERROR_MESSAGE()));
END CATCH;

-- T4. CHECK: giới tính ngoài Nam/Nữ
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop)
    VALUES ('T004', N'Test', '2005-01-01', N'Khác', 'CNTT01');
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'CHECK: giới tính ngoài Nam/Nữ', N'FAIL', N'Dữ liệu sai vẫn được thêm');
END TRY
BEGIN CATCH
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'CHECK: giới tính ngoài Nam/Nữ', N'PASS', CONCAT(N'Lỗi ', ERROR_NUMBER(), N': ', ERROR_MESSAGE()));
END CATCH;

-- T5. Khóa chính: trùng mã sinh viên
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop)
    VALUES ('SV001', N'Trùng mã', '2005-01-01', N'Nam', 'CNTT01');
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'PK: trùng mã sinh viên', N'FAIL', N'Dữ liệu sai vẫn được thêm');
END TRY
BEGIN CATCH
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'PK: trùng mã sinh viên', N'PASS', CONCAT(N'Lỗi ', ERROR_NUMBER(), N': ', ERROR_MESSAGE()));
END CATCH;

-- T6. CHECK: số tín chỉ bằng 0
BEGIN TRY
    INSERT INTO MONHOC (MaMH, TenMH, SoTinChi) VALUES ('T006', N'Môn test', 0);
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'CHECK: số tín chỉ bằng 0', N'FAIL', N'Dữ liệu sai vẫn được thêm');
END TRY
BEGIN CATCH
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'CHECK: số tín chỉ bằng 0', N'PASS', CONCAT(N'Lỗi ', ERROR_NUMBER(), N': ', ERROR_MESSAGE()));
END CATCH;

-- T7. Khóa ngoại: điểm của môn học không tồn tại
BEGIN TRY
    INSERT INTO KETQUA (MaSV, MaMH, Diem) VALUES ('SV007', 'COMP9999', 8);
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'FK: điểm của môn học không tồn tại', N'FAIL', N'Dữ liệu sai vẫn được thêm');
END TRY
BEGIN CATCH
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'FK: điểm của môn học không tồn tại', N'PASS', CONCAT(N'Lỗi ', ERROR_NUMBER(), N': ', ERROR_MESSAGE()));
END CATCH;

-- T8. Khóa ngoại: điểm của sinh viên không tồn tại
BEGIN TRY
    INSERT INTO KETQUA (MaSV, MaMH, Diem) VALUES ('SV999', 'COMP1019', 8);
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'FK: điểm của sinh viên không tồn tại', N'FAIL', N'Dữ liệu sai vẫn được thêm');
END TRY
BEGIN CATCH
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'FK: điểm của sinh viên không tồn tại', N'PASS', CONCAT(N'Lỗi ', ERROR_NUMBER(), N': ', ERROR_MESSAGE()));
END CATCH;

-- T9. Khóa chính ghép: nhập điểm trùng (cùng sinh viên, cùng môn)
BEGIN TRY
    INSERT INTO KETQUA (MaSV, MaMH, Diem) VALUES ('SV001', 'COMP1019', 5);
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'PK ghép: nhập điểm trùng sinh viên + môn', N'FAIL', N'Dữ liệu sai vẫn được thêm');
END TRY
BEGIN CATCH
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'PK ghép: nhập điểm trùng sinh viên + môn', N'PASS', CONCAT(N'Lỗi ', ERROR_NUMBER(), N': ', ERROR_MESSAGE()));
END CATCH;

-- T10. Không xóa được khoa đang có lớp
BEGIN TRY
    DELETE FROM KHOA WHERE MaKhoa = 'FIT';
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'FK: xóa khoa đang có lớp', N'FAIL', N'Khoa vẫn bị xóa');
END TRY
BEGIN CATCH
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'FK: xóa khoa đang có lớp', N'PASS', CONCAT(N'Lỗi ', ERROR_NUMBER(), N': ', ERROR_MESSAGE()));
END CATCH;

-- T11. Không xóa được sinh viên đang có điểm
BEGIN TRY
    DELETE FROM SINHVIEN WHERE MaSV = 'SV001';
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'FK: xóa sinh viên đang có điểm', N'FAIL', N'Sinh viên vẫn bị xóa');
END TRY
BEGIN CATCH
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'FK: xóa sinh viên đang có điểm', N'PASS', CONCAT(N'Lỗi ', ERROR_NUMBER(), N': ', ERROR_MESSAGE()));
END CATCH;

-- T12. CHECK: ngày sinh ở tương lai
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop)
    VALUES ('T012', N'Test', DATEADD(DAY, 30, CAST(GETDATE() AS DATE)), N'Nam', 'CNTT01');
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'CHECK: ngày sinh ở tương lai', N'FAIL', N'Dữ liệu sai vẫn được thêm');
END TRY
BEGIN CATCH
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'CHECK: ngày sinh ở tương lai', N'PASS', CONCAT(N'Lỗi ', ERROR_NUMBER(), N': ', ERROR_MESSAGE()));
END CATCH;

-- T13. UNIQUE: trùng email sinh viên
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop, Email)
    VALUES ('T013', N'Test', '2005-01-01', N'Nam', 'CNTT01', 'sv001@student.edu.vn');
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'UNIQUE: trùng email sinh viên', N'FAIL', N'Dữ liệu sai vẫn được thêm');
END TRY
BEGIN CATCH
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'UNIQUE: trùng email sinh viên', N'PASS', CONCAT(N'Lỗi ', ERROR_NUMBER(), N': ', ERROR_MESSAGE()));
END CATCH;

-- T14. CHECK: vai trò tài khoản không hợp lệ
BEGIN TRY
    INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, HoTen, VaiTro) VALUES ('t014', 'x', N'Test', 'Guest');
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'CHECK: vai trò tài khoản không hợp lệ', N'FAIL', N'Dữ liệu sai vẫn được thêm');
END TRY
BEGIN CATCH
    INSERT INTO @Results (TestCase, KetQua, ChiTiet) VALUES (N'CHECK: vai trò tài khoản không hợp lệ', N'PASS', CONCAT(N'Lỗi ', ERROR_NUMBER(), N': ', ERROR_MESSAGE()));
END CATCH;

ROLLBACK TRANSACTION;   -- hủy mọi thay đổi (nếu có test nào "lọt" vào thì cũng không để lại dữ liệu)

-- Bảng kết quả và tổng kết
SELECT STT, TestCase, KetQua, ChiTiet FROM @Results ORDER BY STT;

SELECT COUNT(*)                                          AS TongSoTest,
       SUM(CASE WHEN KetQua = N'PASS' THEN 1 ELSE 0 END) AS SoPASS,
       SUM(CASE WHEN KetQua = N'FAIL' THEN 1 ELSE 0 END) AS SoFAIL
FROM @Results;
GO
