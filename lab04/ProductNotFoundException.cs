using System;

namespace QuanLySanPham
{
    // Exception tự tạo: phát sinh khi không tìm thấy sản phẩm
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message) : base(message)
        {
        }
    }
}