using System;

namespace QuanLyNhanVien
{
    public class NVKD : NhanVien
    {
	    private double doanhSo;

	    public double DoanhSo
        {   
            get { return doanhSo; }
            set { doanhSo = value >= 0 ? value : 0; }
        }
        public NVKD(string maNV, string hoTen, double luongCoBan, double doanhSo) : base(maNV, hoTen, luongCoBan)
	    {
		    this.doanhSo = doanhSo;
	    }
        public override double TinhLuong()
        {
            return LuongCoBan + 0.05 * DoanhSo;
        }

        public override void HienThiThongTin()
        {
            Console.WriteLine($"[Nhân viên kinh doanh] Mã NV: {MaNV} - Họ tên: {HoTen} - Doanh số: {DoanhSo:N0} - Lương: {TinhLuong():N0}");
        }
    }
}
