using System;

namespace QuanLySinhVienOOP
{
    public class SinhVien : Nguoi
    {
        private double diemTrungBinh;

        public string MaSinhVien { get; set; }
        public string MaLop { get; set; }

        // Diem trung binh chi nhan gia tri tu 0 den 10
        public double DiemTrungBinh
        {
            get { return diemTrungBinh; }
            set { diemTrungBinh = (value >= 0 && value <= 10) ? value : 0; }
        }

        public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh, string maLop, double diemTrungBinh)
            : base(hoTen, ngaySinh)
        {
            MaSinhVien = maSinhVien;
            MaLop = maLop;
            DiemTrungBinh = diemTrungBinh;
        }

        // Xep loai dua tren diem trung binh
        public string XepLoai()
        {
            if (DiemTrungBinh >= 8)
                return "Gioi";
            else if (DiemTrungBinh >= 6.5)
                return "Kha";
            else if (DiemTrungBinh >= 5)
                return "Trung binh";
            else
                return "Yeu";
        }

        // Override lai de them thong tin rieng cua sinh vien (da hinh)
        public override string LayThongTin()
        {
            return $"Ma SV: {MaSinhVien} - Ho ten: {HoTen} - Lop: {MaLop} - Diem: {DiemTrungBinh:0.0} - Xep loai: {XepLoai()}";
        }
    }
}
