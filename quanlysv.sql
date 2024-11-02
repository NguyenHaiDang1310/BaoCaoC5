CREATE DATABASE quanlysv;
GO
USE quanlysv;
GO
CREATE TABLE SinhVien (
    MaSinhVien NVARCHAR(50) PRIMARY KEY,  -- Mã sinh viên là khóa chính
    HoTen NVARCHAR(100),                  -- Họ tên
    NgaySinh DATE,                        -- Ngày sinh
    GioiTinh NVARCHAR(10),                -- Giới tính
    DiemTB FLOAT,                         -- Điểm trung bình
    Khoa NVARCHAR(100)                    -- Khoa của sinh viên
);
GO
CREATE TABLE TaiKhoan (
    TenDangNhap NVARCHAR(50) PRIMARY KEY, -- Tên đăng nhập là khóa chính
    MatKhau NVARCHAR(100),                -- Mật khẩu
    Keypass NVARCHAR(100)                 -- Khóa bảo mật
);
GO
CREATE TABLE Khoa (
    MaKhoa NVARCHAR(50) PRIMARY KEY,  -- Mã khoa là khóa chính
    TenKhoa NVARCHAR(100)             -- Tên khoa
);
GO

INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES ('CNTT', 'Công nghệ thông tin');
INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES ('KT', 'Kế Toán');
INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES ('NN', 'Ngoại Ngữ');
INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES ('DT', 'Điện tử');
GO

GO
SELECT * FROM SinhVien;
SELECT * FROM Khoa;
SELECT * FROM TaiKhoan;
SELECT TenKhoa FROM Khoa;

ALTER TABLE TaiKhoan
ADD HoTen NVARCHAR(100),
    Email NVARCHAR(100),
    SoDienThoai NVARCHAR(15)


INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES ('CNTT', N'Công nghệ thông tin');
INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES ('KT', N'Kế Toán');
INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES ('NN', N'Ngoại Ngữ');
INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES ('DT', N'Điện tử');



EXEC sp_help 'SinhVien';
EXEC sp_help 'Khoa';
ALTER TABLE SinhVien ALTER COLUMN Khoa NVARCHAR(100);
ALTER TABLE Khoa ALTER COLUMN TenKhoa NVARCHAR(100);
ALTER TABLE SinhVien ADD TinhTrangTotNghiep NVARCHAR(50);

INSERT INTO SinhVien (MaSinhVien, HoTen, NgaySinh, GioiTinh, DiemTB, Khoa, HinhAnh, TinhTrangTotNghiep)
VALUES 
    ('2122110026', N'Tran Van Quang', '2003-04-22', N'Nam', 8.5, N'Công nghệ thông tin', 0x89504E470D0A1A0A0000000044948445200000040, NULL),
    ('2122110027', N'Le Thi Hoa', '2002-08-15', N'Nữ', 8.2, N'Kinh tế', 0xFFD8FFE000104464494600101010100000000000FFE2022, NULL),
    ('2122110028', N'Pham Minh Tri', '2001-09-13', N'Nam', 7.8, N'Cơ khí', 0x89504E470D0A1A0A0000000044948445200000040, NULL),
    ('2122110029', N'Bui Ngoc Lan', '2003-05-17', N'Nữ', 9.0, N'Kế toán', 0xFFD8FFE000104464494600101010100000000000FFE2022, NULL),
    ('2122110030', N'Nguyen Van Bao', '2004-11-23', N'Nam', 7.6, N'Công nghệ thông tin', 0x89504E470D0A1A0A0000000044948445200000040, NULL),
    ('2122110031', N'Vu Thi Thanh', '2002-12-08', N'Nữ', 8.9, N'Kế toán', 0xFFD8FFE000104464494600101010100000000000FFE2022, NULL),
    ('2122110032', N'Hoang Minh Tu', '2003-03-19', N'Nam', 8.3, N'Kinh tế', 0x89504E470D0A1A0A0000000044948445200000040, NULL);


DELETE FROM SinhVien
WHERE MaSinhVien IN ('2122110026', '2122110027', '2122110028', '2122110029', '2122110030', '2122110031', '2122110032');
ALTER TABLE SinhVien
ADD TinhTrangTotNghiep NVARCHAR(50);