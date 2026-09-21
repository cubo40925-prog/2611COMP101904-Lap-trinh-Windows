using System;

namespace QuanLyNhanVien
{
    public class NVTV : NhanVien
    {
        private int soGioLam;
        private double luongTheoGio;

        public int SoGioLam
        {
            get { return soGioLam; }
            set { soGioLam = value >= 0 ? value : 0; }
        }
        public double LuongTheoGio
        {
            get { return luongTheoGio; }
            set { luongTheoGio = value >= 0 ? value : 0; }
        }

        public NVTV(string maNV, string hoTen, double luongCoBan, int soGioLam, double luongTheoGio) : base(maNV, hoTen, luongCoBan)
        {
            this.soGioLam = soGioLam;
            this.luongTheoGio = luongTheoGio;
        }
        public override double TinhLuong()
        {
            return SoGioLam * LuongTheoGio;
        }

        public override void HienThiThongTin()
        {
            Console.WriteLine($"[Nhân viên thời vụ] Mã NV: {MaNV} - Họ tên: {HoTen} - Số giờ làm: {SoGioLam} - Lương theo giờ: {LuongTheoGio:N0} - Lương: {TinhLuong():N0}");
        }
    }   
}
