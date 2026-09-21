using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyNhanVien
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            List<NhanVien> danhSach = new List<NhanVien>();

            int soLuong = NhapSoNguyenTrongKhoang("Nhập số lượng nhân viên cần nhập (tối thiểu 5): ", 5, 100);

            for (int i = 1; i <= soLuong; i++)
            {
                NhanVien nv = NhapMotNhanVien(i);
                danhSach.Add(nv);
                Console.WriteLine();
            }

            int luaChon;
            do
            {
                HienThiMenu();
                luaChon = NhapSoNguyenTrongKhoang("Chọn chức năng: ", 0, 4);

                switch (luaChon)
                {
                    case 1:
                        XuatDanhSach(danhSach);
                        break;
                    case 2:
                        TimTheoMa(danhSach);
                        break;
                    case 3:
                        TimLuongCaoNhat(danhSach);
                        break;
                    case 4:
                        TinhTongLuong(danhSach);
                        break;
                    case 0:
                        Console.WriteLine("Tạm biệt!");
                        break;
                }

                if (luaChon != 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Nhấn Enter để tiếp tục...");
                    Console.ReadLine();
                }

            } while (luaChon != 0);
        }

        static void HienThiMenu()
        {
            Console.WriteLine();
            Console.WriteLine("========== MENU ==========");
            Console.WriteLine("1. Xuất danh sách nhân viên");
            Console.WriteLine("2. Tìm nhân viên theo mã");
            Console.WriteLine("3. Tìm nhân viên có lương cao nhất");
            Console.WriteLine("4. Tính tổng lương công ty phải trả");
            Console.WriteLine("0. Thoát");
        }

        
        static NhanVien NhapMotNhanVien(int thuTu)
        {
            Console.WriteLine($"--- Nhập nhân viên thứ {thuTu} ---");
            Console.WriteLine("Chọn loại nhân viên: 1. Văn phòng   2. Kinh doanh   3. Thời vụ");
            int loai = NhapSoNguyenTrongKhoang("Nhập lựa chọn (1-3): ", 1, 3);

            string maNV = NhapChuoiKhongRong("Nhập mã nhân viên: ");
            string hoTen = NhapChuoiKhongRong("Nhập họ tên: ");
            double luongCoBan = NhapSoThucLonHon0("Nhập lương cơ bản: ");

            if (loai == 1)
            {
                int soNgay = NhapSoNguyenTrongKhoang("Nhập số ngày làm việc (0-31): ", 0, 31);
                return new NVVP(maNV, hoTen, luongCoBan, soNgay);
            }
            else if (loai == 2)
            {
                double doanhSo = NhapSoThucKhongAm("Nhap doanh so: ");
                return new NVKD(maNV, hoTen, luongCoBan, doanhSo);
            }
            else
            {
                int soGio = NhapSoNguyenKhongAm("Nhập số giờ làm: ");
                double luongTheoGio = NhapSoThucKhongAm("Nhập lương theo giờ: ");
                return new NVTV(maNV, hoTen, luongCoBan, soGio, luongTheoGio);
            }
        }

        
        static void XuatDanhSach(List<NhanVien> danhSach)
        {
            Console.WriteLine("===== DANH SÁCH NHÂN VIÊN =====");
            foreach (NhanVien nv in danhSach)
            {
                nv.HienThiThongTin();
            }
        }

        static void TimTheoMa(List<NhanVien> danhSach)
        {
            string ma = NhapChuoiKhongRong("Nhập mã nhân viên cần tìm: ");
            bool timThay = false;

            foreach (NhanVien nv in danhSach)
            {
                if (nv.MaNV.Equals(ma, StringComparison.OrdinalIgnoreCase))
                {
                    nv.HienThiThongTin();
                    timThay = true;
                    break;
                }
            }

            if (!timThay)
                Console.WriteLine("Không tìm thấy nhân viên có mã " + ma);
        }

        
        static void TimLuongCaoNhat(List<NhanVien> danhSach)
        {
            NhanVien nvCaoNhat = danhSach[0];

            foreach (NhanVien nv in danhSach)
            {
                if (nv.TinhLuong() > nvCaoNhat.TinhLuong())
                    nvCaoNhat = nv;
            }

            Console.WriteLine("Nhân viên có lương cao nhất:");
            nvCaoNhat.HienThiThongTin();
        }

        
        static void TinhTongLuong(List<NhanVien> danhSach)
        {
            double tongLuong = 0;

            foreach (NhanVien nv in danhSach)
            {
                tongLuong += nv.TinhLuong();
            }

            Console.WriteLine($"Tổng lương công ty phải trả: {tongLuong:N0}");
        }

        static string NhapChuoiKhongRong(string thongBao)
        {
            string ketQua;
            do
            {
                Console.Write(thongBao);
                ketQua = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ketQua))
                    Console.WriteLine("Không được để trống, vui lòng nhập lại!");
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
                Console.WriteLine($"Vui lòng nhập số nguyên từ {min} đến {max}!");
            }
        }

        static int NhapSoNguyenKhongAm(string thongBao)
        {
            int so;
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine();
                if (int.TryParse(input, out so) && so >= 0)
                    return so;
                Console.WriteLine("Vui lòng nhập số nguyên không âm!");
            }
        }

        static double NhapSoThucLonHon0(string thongBao)
        {
            double so;
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine();
                if (double.TryParse(input, out so) && so > 0)
                    return so;
                Console.WriteLine("Vui lòng nhập số lớn hơn 0!");
            }
        }

        static double NhapSoThucKhongAm(string thongBao)
        {
            double so;
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine();
                if (double.TryParse(input, out so) && so >= 0)
                    return so;
                Console.WriteLine("Vui lòng nhập số không âm!");
            }
        }
    }
}