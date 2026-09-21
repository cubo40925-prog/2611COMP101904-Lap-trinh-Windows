using System;

namespace QuanLySinhVienOOP
{
    public class Nguoi
    {
        private string hoTen;
        private DateTime ngaySinh;

        public string HoTen
        {
            get { return hoTen; }
            set { hoTen = value; }
        }

        public DateTime NgaySinh
        {
            get { return ngaySinh; }
            set { ngaySinh = value; }
        }

        public Nguoi(string hoTen, DateTime ngaySinh)
        {
            HoTen = hoTen;
            NgaySinh = ngaySinh;
        }

        // Co the override lai o lop con
        public virtual string LayThongTin()
        {
            return $"Ho ten: {HoTen} - Ngay sinh: {NgaySinh:dd/MM/yyyy}";
        }
    }
}
