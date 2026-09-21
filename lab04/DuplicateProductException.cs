using System;

namespace QuanLySanPham
{
    // Exception tu tao: phat sinh khi them san pham co ma bi trung
    public class DuplicateProductException : Exception
    {
        public DuplicateProductException(string message) : base(message)
        {
        }
    }
}