using System;

namespace QuanLySanPham
{
    public class Product : IEntity
    {
        private double price;
        private int quantity;

        public string MaSP { get; set; }
        public string TenSP { get; set; }

        // Gia khong duoc am
        public double Price
        {
            get { return price; }
            set { price = value >= 0 ? value : 0; }
        }

        // So luong khong duoc am
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value >= 0 ? value : 0; }
        }

        // Anh xa MaSP sang Id de dung chung voi Repository<T>
        public string Id => MaSP;

        public Product(string maSP, string tenSP, double price, int quantity)
        {
            MaSP = maSP;
            TenSP = tenSP;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"Ma: {MaSP} - Ten: {TenSP} - Gia: {Price:N0} - So luong: {Quantity}";
        }
    }
}
