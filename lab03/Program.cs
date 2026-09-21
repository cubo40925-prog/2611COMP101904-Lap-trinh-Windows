using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace QuanLySinhVienOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            // Bat UTF-8 de hien thi tieng Viet co dau dung tren Console
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            QuanLySinhVien quanLy = new QuanLySinhVien();

            int luaChon;
            do
            {
                HienThiMenu();
                luaChon = NhapSoNguyenTrongKhoang("Chon chuc nang: ", 0, 8);

                switch (luaChon)
                {
                    case 1:
                        ThemSinhVien(quanLy);
                        break;
                    case 2:
                        XuatDanhSach(quanLy.LayDanhSach());
                        break;
                    case 3:
                        TimTheoMa(quanLy);
                        break;
                    case 4:
                        TimTheoTen(quanLy);
                        break;
                    case 5:
                        SuaDiem(quanLy);
                        break;
                    case 6:
                        XoaSinhVien(quanLy);
                        break;
                    case 7:
                        XuatDanhSach(quanLy.SapXepTheoDiem());
                        break;
                    case 8:
                        XuatDanhSach(quanLy.LocSinhVienDat());
                        break;
                    case 0:
                        Console.WriteLine("Tam biet!");
                        break;
                }

                if (luaChon != 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Nhan Enter de tiep tuc...");
                    Console.ReadLine();
                }

            } while (luaChon != 0);
        }

        static void HienThiMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===== QUAN LY SINH VIEN =====");
            Console.WriteLine("1. Them sinh vien");
            Console.WriteLine("2. Xuat danh sach");
            Console.WriteLine("3. Tim sinh vien theo ma");
            Console.WriteLine("4. Tim sinh vien theo ten");
            Console.WriteLine("5. Sua diem trung binh");
            Console.WriteLine("6. Xoa sinh vien");
            Console.WriteLine("7. Sap xep theo diem giam dan");
            Console.WriteLine("8. Loc sinh vien dat");
            Console.WriteLine("0. Thoat");
        }

        static void ThemSinhVien(QuanLySinhVien quanLy)
        {
            Console.WriteLine("--- Them sinh vien moi ---");
            string ma = NhapChuoiKhongRong("Nhap ma sinh vien: ");

            if (quanLy.TimTheoMa(ma) != null)
            {
                Console.WriteLine("Ma sinh vien " + ma + " da ton tai!");
                return;
            }

            string hoTen = NhapChuoiKhongRong("Nhap ho ten: ");
            DateTime ngaySinh = NhapNgay("Nhap ngay sinh (dd/MM/yyyy): ");
            string maLop = NhapChuoiKhongRong("Nhap ma lop: ");
            double diem = NhapDiemTrungBinh("Nhap diem trung binh (0-10): ");

            SinhVien sv = new SinhVien(ma, hoTen, ngaySinh, maLop, diem);
            quanLy.Them(sv);
            Console.WriteLine("Them sinh vien thanh cong!");
        }

        static void XuatDanhSach(List<SinhVien> danhSach)
        {
            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sach rong!");
                return;
            }

            Console.WriteLine("===== DANH SACH SINH VIEN =====");
            foreach (SinhVien sv in danhSach)
            {
                Console.WriteLine(sv.LayThongTin());
            }
        }

        static void TimTheoMa(QuanLySinhVien quanLy)
        {
            string ma = NhapChuoiKhongRong("Nhap ma sinh vien can tim: ");
            SinhVien sv = quanLy.TimTheoMa(ma);

            if (sv == null)
                Console.WriteLine("Khong tim thay sinh vien co ma " + ma);
            else
                Console.WriteLine(sv.LayThongTin());
        }

        static void TimTheoTen(QuanLySinhVien quanLy)
        {
            string tuKhoa = NhapChuoiKhongRong("Nhap tu khoa ho ten can tim: ");
            List<SinhVien> ketQua = quanLy.TimTheoTen(tuKhoa);
            XuatDanhSach(ketQua);
        }

        static void SuaDiem(QuanLySinhVien quanLy)
        {
            string ma = NhapChuoiKhongRong("Nhap ma sinh vien can sua diem: ");

            if (quanLy.TimTheoMa(ma) == null)
            {
                Console.WriteLine("Khong tim thay sinh vien co ma " + ma);
                return;
            }

            double diemMoi = NhapDiemTrungBinh("Nhap diem trung binh moi (0-10): ");
            quanLy.Sua(ma, diemMoi);
            Console.WriteLine("Cap nhat diem thanh cong!");
        }

        static void XoaSinhVien(QuanLySinhVien quanLy)
        {
            string ma = NhapChuoiKhongRong("Nhap ma sinh vien can xoa: ");

            if (quanLy.Xoa(ma))
                Console.WriteLine("Xoa sinh vien thanh cong!");
            else
                Console.WriteLine("Khong tim thay sinh vien co ma " + ma);
        }

        static string NhapChuoiKhongRong(string thongBao)
        {
            string ketQua;
            do
            {
                Console.Write(thongBao);
                ketQua = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ketQua))
                    Console.WriteLine("Khong duoc de trong, vui long nhap lai!");
            } while (string.IsNullOrWhiteSpace(ketQua));
            return ketQua;
        }

        static int NhapSoNguyenTrongKhoang(string thongBao, int min, int max)
        {
            int so;
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine();
                if (int.TryParse(input, out so) && so >= min && so <= max)
                    return so;
                Console.WriteLine($"Vui long nhap so nguyen tu {min} den {max}!");
            }
        }

        static double NhapDiemTrungBinh(string thongBao)
        {
            double diem;
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine();
                if (double.TryParse(input, out diem) && diem >= 0 && diem <= 10)
                    return diem;
                Console.WriteLine("Diem khong hop le, vui long nhap lai (0-10)!");
            }
        }

        static DateTime NhapNgay(string thongBao)
        {
            DateTime ngay;
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngay))
                    return ngay;
                Console.WriteLine("Ngay khong hop le, vui long nhap dung dinh dang dd/MM/yyyy!");
            }
        }
    }
}
