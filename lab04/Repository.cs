using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLySanPham
{
    // Generic class quan ly danh sach doi tuong kieu T, T phai co Id (IEntity)
    public class Repository<T> where T : IEntity
    {
        private List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
        }

        public void Remove(string id)
        {
            T item = FindById(id);
            if (item != null)
                items.Remove(item);
        }

        public T FindById(string id)
        {
            return items.FirstOrDefault(x => x.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        // Tim theo dieu kien bat ky - dung Func<T,bool>
        public List<T> Find(Func<T, bool> dieuKien)
        {
            return items.Where(dieuKien).ToList();
        }

        public List<T> GetAll()
        {
            return new List<T>(items);
        }
    }
}