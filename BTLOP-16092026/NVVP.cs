using System;

namespace QuanLyNhanVien
{
    public class NVVP : NhanVien
    {
        private int soNgayLamViec;

        public int SoNgayLamViec
        {
            get { return soNgayLamViec; }
            set { soNgayLamViec = (value >= 0 && value <= 31) ? value : 0; }
        }
        public NVVP(string maNV, string hoTen, double luongCoBan, int soNgayLamViec) : base(maNV, hoTen, luongCoBan)
        {
            this.SoNgayLamViec = soNgayLamViec;
        }
        public virtual double TinhLuong()
        {
            return LuongCoBan + SoNgayLamViec * 200000;
        }

        public override void HienThiThongTin()
        {
            Console.WriteLine($"[Nhân viên văn phòng] Mã NV: {MaNV} - Họ tên: {HoTen} - Số ngày làm: {SoNgayLamViec} - Lương: {TinhLuong():N0}");
        }
    }
}
