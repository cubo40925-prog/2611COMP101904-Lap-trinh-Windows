using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLySinhVienOOP
{
    // Lop nay quan ly List<SinhVien>, Program.cs khong duoc dung truc tiep vao danh sach
    public class QuanLySinhVien
    {
        private List<SinhVien> danhSach = new List<SinhVien>();

        // Them sinh vien moi, tra ve false neu ma da ton tai
        public bool Them(SinhVien sv)
        {
            if (TimTheoMa(sv.MaSinhVien) != null)
                return false;

            danhSach.Add(sv);
            return true;
        }

        // Sua diem trung binh theo ma, tra ve false neu khong tim thay
        public bool Sua(string ma, double diemMoi)
        {
            SinhVien sv = TimTheoMa(ma);
            if (sv == null)
                return false;

            sv.DiemTrungBinh = diemMoi;
            return true;
        }

        // Xoa sinh vien theo ma, tra ve false neu khong tim thay
        public bool Xoa(string ma)
        {
            SinhVien sv = TimTheoMa(ma);
            if (sv == null)
                return false;

            danhSach.Remove(sv);
            return true;
        }

        // Tim theo ma - dung LINQ FirstOrDefault
        public SinhVien TimTheoMa(string ma)
        {
            return danhSach.FirstOrDefault(sv => sv.MaSinhVien.Equals(ma, StringComparison.OrdinalIgnoreCase));
        }

        // Tim theo ten - dung LINQ Where + Contains (khong phan biet hoa thuong)
        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            return danhSach.Where(sv => sv.HoTen.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        // Sap xep theo diem giam dan - dung LINQ OrderByDescending
        public List<SinhVien> SapXepTheoDiem()
        {
            return danhSach.OrderByDescending(sv => sv.DiemTrungBinh).ToList();
        }

        // Loc sinh vien dat (diem >= 5) - dung LINQ Where
        public List<SinhVien> LocSinhVienDat()
        {
            return danhSach.Where(sv => sv.DiemTrungBinh >= 5).ToList();
        }

        // Lay ban sao danh sach de hien thi, khong cho sua truc tiep tu ben ngoai
        public List<SinhVien> LayDanhSach()
        {
            return danhSach.ToList();
        }
    }
}
