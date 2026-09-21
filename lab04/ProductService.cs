using System;
using System.Collections.Generic;

namespace QuanLySanPham
{
    public class ProductService
    {
        private Repository<Product> repository = new Repository<Product>();

        public event Action<Product> ProductAdded;
        public event Action<Product> ProductRemoved;

        public void AddProduct(Product sp)
        {
            if (string.IsNullOrWhiteSpace(sp.MaSP))
                throw new ArgumentException("Mã sản phẩm không được rỗng!");

            if (repository.FindById(sp.MaSP) != null)
                throw new DuplicateProductException("Mã sản phẩm " + sp.MaSP + " đã tồn tại!");

            repository.Add(sp);
            ProductAdded?.Invoke(sp);
        }

        public void RemoveProduct(string ma)
        {
            Product sp = repository.FindById(ma);
            if (sp == null)
                throw new ProductNotFoundException("Không tìm thấy sản phẩm có mã " + ma);

            repository.Remove(ma);
            ProductRemoved?.Invoke(sp);
        }

        public Product FindByMa(string ma)
        {
            return repository.FindById(ma);
        }

        // Alias để tương thích với Program.cs
        public Product FindByMaSP(string ma)
        {
            return FindByMa(ma);
        }

        public List<Product> Search(string tuKhoa)
        {
            Func<Product, bool> dieuKien = p => p.TenSP.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0;
            return repository.Find(dieuKien);
        }

        public List<Product> Filter(double giaMin, double giaMax)
        {
            Func<Product, bool> dieuKien = p => p.Price >= giaMin && p.Price <= giaMax;
            return repository.Find(dieuKien);
        }

        public List<Product> GetAll()
        {
            return repository.GetAll();
        }

        public double TinhTongGiaTriKho()
        {
            double tong = 0;
            foreach (Product sp in repository.GetAll())
            {
                tong += sp.Price * sp.Quantity;
            }
            return tong;
        }

        // Alias để tương thích với Program.cs
        public double GetTotalInventoryValue()
        {
            return TinhTongGiaTriKho();
        }
    }
}