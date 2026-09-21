using System;

namespace QuanLyNhanVien
{
    public class NhanVien
    {
        private string maNV;
        private string hoTen;
        private double luongCoBan;

        public string MaNV { get; set; }
        public string HoTen { get; set; }

        public double LuongCoBan    
        {
            get { return luongCoBan; }
            set { luongCoBan = value > 0 ? value : 0; }
        }
        public NhanVien(string maNV, string hoTen, double luongCoBan)
        {
            this.maNV = maNV;
            this.hoTen = hoTen;
            this.luongCoBan = luongCoBan;
        }
        public virtual double TinhLuong()
        {
            return luongCoBan;
        }
        public virtual void HienThiThongTin()
        {
            Console.WriteLine($"Mã NV: {MaNV} - Họ tên: {HoTen} - Lương: {TinhLuong():N0}");
        }
    }
}
