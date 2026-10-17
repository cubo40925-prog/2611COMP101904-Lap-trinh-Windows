/* ============================================================================
   COMP1019 - Lập trình trên Windows | Lab 10
   FILE 01: TẠO DATABASE EduManagerDB, BẢNG, KHÓA, RÀNG BUỘC, CHỈ MỤC, VIEW

   Cách chạy: mở file trong SSMS (hoặc Azure Data Studio), bấm Execute (F5).
   Script có thể chạy lại nhiều lần: mỗi lần chạy sẽ xóa database cũ và tạo lại.

   Thứ tự chạy cả bài:  01_CreateDatabase.sql  ->  02_InsertSampleData.sql  ->  03_Queries.sql
   ============================================================================ */


/* ----------------------------------------------------------------------------
   PHẦN 1. TẠO DATABASE
   ---------------------------------------------------------------------------- */
USE master;
GO

-- Nếu database cũ đang tồn tại thì đóng hết kết nối rồi xóa (tránh lỗi "database is in use").
IF DB_ID(N'EduManagerDB') IS NOT NULL
BEGIN
    ALTER DATABASE EduManagerDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE EduManagerDB;
END
GO

-- Collation Vietnamese_CI_AS: sắp xếp đúng thứ tự tiếng Việt, không phân biệt hoa/thường
-- (tìm 'an' vẫn ra 'An'), nhưng vẫn phân biệt dấu.
CREATE DATABASE EduManagerDB COLLATE Vietnamese_CI_AS;
GO

USE EduManagerDB;
GO


/* ----------------------------------------------------------------------------
   PHẦN 2. TẠO 6 BẢNG
   Thứ tự tạo: bảng cha trước, bảng con sau
   KHOA -> LOP -> SINHVIEN ; MONHOC ; KETQUA (con của SINHVIEN và MONHOC) ; TAIKHOAN
   Quy ước tên: PK_<Bảng>, FK_<BảngCon>_<BảngCha>, CK_<Bảng>_<Cột>, UQ_<Bảng>_<Cột>, DF_<Bảng>_<Cột>
   Khóa ngoại dùng ON UPDATE CASCADE (đổi mã ở bảng cha thì bảng con tự đổi theo)
   và ON DELETE NO ACTION (mặc định: không cho xóa bản ghi cha đang được tham chiếu).
   ---------------------------------------------------------------------------- */

-- 2.1 KHOA: thông tin khoa
CREATE TABLE KHOA (
    MaKhoa      VARCHAR(20)   NOT NULL,
    TenKhoa     NVARCHAR(100) NOT NULL,
    DienThoai   VARCHAR(15)   NULL,
    CONSTRAINT PK_KHOA PRIMARY KEY (MaKhoa),
    CONSTRAINT UQ_KHOA_TenKhoa UNIQUE (TenKhoa),                       -- không có 2 khoa trùng tên
    CONSTRAINT CK_KHOA_DienThoai                                       -- chỉ gồm chữ số, dài 9-11
        CHECK (DienThoai IS NULL
               OR (DienThoai NOT LIKE '%[^0-9]%' AND LEN(DienThoai) BETWEEN 9 AND 11))
);
GO

-- 2.2 LOP: mỗi lớp thuộc đúng một khoa
CREATE TABLE LOP (
    MaLop       VARCHAR(20)   NOT NULL,
    TenLop      NVARCHAR(100) NOT NULL,
    MaKhoa      VARCHAR(20)   NOT NULL,
    CONSTRAINT PK_LOP PRIMARY KEY (MaLop),
    CONSTRAINT UQ_LOP_TenLop UNIQUE (TenLop),
    CONSTRAINT FK_LOP_KHOA
        FOREIGN KEY (MaKhoa) REFERENCES KHOA (MaKhoa)
        ON UPDATE CASCADE
);
GO

-- 2.3 SINHVIEN: mỗi sinh viên thuộc đúng một lớp
--     Email và SoDienThoai là cột bổ sung (cho phép NULL), có thể bỏ nếu không cần.
CREATE TABLE SINHVIEN (
    MaSV        VARCHAR(20)   NOT NULL,
    HoTen       NVARCHAR(100) NOT NULL,
    NgaySinh    DATE          NULL,
    GioiTinh    NVARCHAR(10)  NOT NULL,
    MaLop       VARCHAR(20)   NOT NULL,
    Email       VARCHAR(100)  NULL,
    SoDienThoai VARCHAR(15)   NULL,
    CONSTRAINT PK_SINHVIEN PRIMARY KEY (MaSV),
    CONSTRAINT CK_SINHVIEN_HoTen                                       -- họ tên không được chỉ là khoảng trắng
        CHECK (LEN(LTRIM(RTRIM(HoTen))) > 0),
    CONSTRAINT CK_SINHVIEN_GioiTinh                                    -- yêu cầu đề: chỉ Nam hoặc Nữ
        CHECK (GioiTinh IN (N'Nam', N'Nữ')),
    CONSTRAINT CK_SINHVIEN_NgaySinh                                    -- ngày sinh không ở tương lai
        CHECK (NgaySinh IS NULL OR NgaySinh <= CAST(GETDATE() AS DATE)),
    CONSTRAINT CK_SINHVIEN_Email                                       -- dạng a@b.c
        CHECK (Email IS NULL OR Email LIKE '_%@_%._%'),
    CONSTRAINT CK_SINHVIEN_SoDienThoai
        CHECK (SoDienThoai IS NULL
               OR (SoDienThoai NOT LIKE '%[^0-9]%' AND LEN(SoDienThoai) BETWEEN 9 AND 11)),
    CONSTRAINT FK_SINHVIEN_LOP
        FOREIGN KEY (MaLop) REFERENCES LOP (MaLop)
        ON UPDATE CASCADE
);
GO

-- 2.4 MONHOC: thông tin môn học
CREATE TABLE MONHOC (
    MaMH        VARCHAR(20)   NOT NULL,
    TenMH       NVARCHAR(100) NOT NULL,
    SoTinChi    INT           NOT NULL,
    CONSTRAINT PK_MONHOC PRIMARY KEY (MaMH),
    CONSTRAINT UQ_MONHOC_TenMH UNIQUE (TenMH),
    CONSTRAINT CK_MONHOC_SoTinChi CHECK (SoTinChi > 0)                 -- yêu cầu đề: số tín chỉ > 0
);
GO

-- 2.5 KETQUA: điểm của sinh viên theo môn.
--     Khóa chính ghép (MaSV, MaMH): mỗi sinh viên chỉ có một điểm cho mỗi môn.
CREATE TABLE KETQUA (
    MaSV        VARCHAR(20)   NOT NULL,
    MaMH        VARCHAR(20)   NOT NULL,
    Diem        DECIMAL(4,2)  NOT NULL,
    CONSTRAINT PK_KETQUA PRIMARY KEY (MaSV, MaMH),
    CONSTRAINT CK_KETQUA_Diem CHECK (Diem BETWEEN 0 AND 10),           -- yêu cầu đề: điểm từ 0 đến 10
    CONSTRAINT FK_KETQUA_SINHVIEN
        FOREIGN KEY (MaSV) REFERENCES SINHVIEN (MaSV)
        ON UPDATE CASCADE,
    CONSTRAINT FK_KETQUA_MONHOC
        FOREIGN KEY (MaMH) REFERENCES MONHOC (MaMH)
        ON UPDATE CASCADE
);
GO

-- 2.6 TAIKHOAN: tài khoản đăng nhập demo cho ứng dụng WinForms ở các buổi sau.
--     LƯU Ý: mật khẩu lưu dạng chữ thường CHỈ ĐỂ DEMO. Ứng dụng thật phải lưu mã băm (hash + salt).
CREATE TABLE TAIKHOAN (
    TenDangNhap VARCHAR(50)   NOT NULL,
    MatKhau     VARCHAR(100)  NOT NULL,
    HoTen       NVARCHAR(100) NOT NULL,
    VaiTro      VARCHAR(20)   NOT NULL CONSTRAINT DF_TAIKHOAN_VaiTro DEFAULT 'User',
    TrangThai   BIT           NOT NULL CONSTRAINT DF_TAIKHOAN_TrangThai DEFAULT 1,   -- 1 = hoạt động, 0 = khóa
    NgayTao     DATETIME2(0)  NOT NULL CONSTRAINT DF_TAIKHOAN_NgayTao DEFAULT SYSDATETIME(),
    CONSTRAINT PK_TAIKHOAN PRIMARY KEY (TenDangNhap),
    CONSTRAINT CK_TAIKHOAN_VaiTro CHECK (VaiTro IN ('Admin', 'User'))
);
GO


/* ----------------------------------------------------------------------------
   PHẦN 3. CHỈ MỤC (INDEX) giúp JOIN và tìm kiếm nhanh hơn
   Khóa chính và UNIQUE đã tự có chỉ mục. Khóa ngoại thì SQL Server KHÔNG tự tạo.
   ---------------------------------------------------------------------------- */
CREATE INDEX IX_LOP_MaKhoa       ON LOP (MaKhoa);
CREATE INDEX IX_SINHVIEN_MaLop   ON SINHVIEN (MaLop);
CREATE INDEX IX_SINHVIEN_HoTen   ON SINHVIEN (HoTen);       -- phục vụ tìm theo họ tên
CREATE INDEX IX_KETQUA_MaMH      ON KETQUA (MaMH);          -- (MaSV đã nằm trong khóa chính)

-- Email không được trùng. Dùng filtered index vì UNIQUE thường chỉ cho phép đúng 1 giá trị NULL.
CREATE UNIQUE INDEX UX_SINHVIEN_Email ON SINHVIEN (Email) WHERE Email IS NOT NULL;
GO


/* ----------------------------------------------------------------------------
   PHẦN 4. VIEW (truy vấn lưu sẵn) dùng cho các báo cáo ở file 03
   CREATE VIEW phải là câu lệnh đầu tiên của batch nên cần GO trước và sau.
   ---------------------------------------------------------------------------- */

-- Sinh viên kèm tên lớp, mã khoa và tên khoa
CREATE VIEW vw_SinhVienChiTiet AS
SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh,
       sv.MaLop, l.TenLop, k.MaKhoa, k.TenKhoa
FROM SINHVIEN sv
INNER JOIN LOP  l ON sv.MaLop = l.MaLop
INNER JOIN KHOA k ON l.MaKhoa = k.MaKhoa;
GO

-- Điểm trung bình từng sinh viên: DiemTB (trung bình cộng) và DiemTBTinChi (có trọng số tín chỉ).
-- Dùng LEFT JOIN nên sinh viên chưa có điểm vẫn xuất hiện (SoMonDaHoc = 0, điểm = NULL).
CREATE VIEW vw_DiemTrungBinh AS
SELECT sv.MaSV, sv.HoTen, sv.MaLop,
       COUNT(kq.MaMH) AS SoMonDaHoc,
       CAST(AVG(kq.Diem * 1.0) AS DECIMAL(4,2)) AS DiemTB,
       CAST(SUM(kq.Diem * mh.SoTinChi) * 1.0 / NULLIF(SUM(mh.SoTinChi), 0) AS DECIMAL(4,2)) AS DiemTBTinChi
FROM SINHVIEN sv
LEFT JOIN KETQUA kq ON sv.MaSV = kq.MaSV
LEFT JOIN MONHOC mh ON kq.MaMH = mh.MaMH
GROUP BY sv.MaSV, sv.HoTen, sv.MaLop;
GO


/* ----------------------------------------------------------------------------
   PHẦN 5. KIỂM TRA NHANH (chụp ảnh kết quả phần này cho báo cáo)
   ---------------------------------------------------------------------------- */

-- 5.1 Danh sách bảng đã tạo (phải đủ 6 bảng)
SELECT name AS TenBang, create_date AS NgayTao
FROM sys.tables
ORDER BY name;

-- 5.2 Danh sách khóa ngoại
SELECT fk.name                            AS TenKhoaNgoai,
       OBJECT_NAME(fk.parent_object_id)   AS BangCon,
       OBJECT_NAME(fk.referenced_object_id) AS BangCha,
       fk.update_referential_action_desc  AS KhiCapNhat,
       fk.delete_referential_action_desc  AS KhiXoa
FROM sys.foreign_keys fk
ORDER BY BangCon, TenKhoaNgoai;

-- 5.3 Danh sách ràng buộc CHECK
SELECT OBJECT_NAME(parent_object_id) AS TenBang, name AS TenRangBuoc, definition AS DieuKien
FROM sys.check_constraints
ORDER BY TenBang, TenRangBuoc;
GO
