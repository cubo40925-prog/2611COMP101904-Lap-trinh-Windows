using System;

namespace QuanLyMangSoNguyen
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] mangSo = null;
            int luaChon;

            do
            {
                HienThiMenu();
                luaChon = NhapSoNguyen("Chon chuc nang: ");

                switch (luaChon)
                {
                    case 1:
                        mangSo = NhapMang();
                        break;

                    case 2:
                        if (KiemTraDaNhapMang(mangSo))
                            XuatMang(mangSo);
                        break;

                    case 3:
                        if (KiemTraDaNhapMang(mangSo))
                            Console.WriteLine("Tong cac phan tu: " + TinhTong(mangSo));
                        break;

                    case 4:
                        if (KiemTraDaNhapMang(mangSo))
                        {
                            Console.WriteLine("Gia tri lon nhat: " + TimMax(mangSo));
                            Console.WriteLine("Gia tri nho nhat: " + TimMin(mangSo));
                        }
                        break;

                    case 5:
                        if (KiemTraDaNhapMang(mangSo))
                        {
                            Console.WriteLine("So phan tu chan: " + DemChan(mangSo));
                            Console.WriteLine("So phan tu le: " + DemLe(mangSo));
                        }
                        break;

                    case 6:
                        if (KiemTraDaNhapMang(mangSo))
                        {
                            SapXepTangDan(mangSo);
                            Console.WriteLine("Mang sau khi sap xep:");
                            XuatMang(mangSo);
                        }
                        break;

                    case 7:
                        if (KiemTraDaNhapMang(mangSo))
                        {
                            int x = NhapSoNguyen("Nhap gia tri can tim: ");
                            int viTri = TimKiem(mangSo, x);
                            if (viTri == -1)
                                Console.WriteLine("Khong tim thay " + x + " trong mang.");
                            else
                                Console.WriteLine("Tim thay " + x + " tai vi tri " + viTri + " (tinh tu 0).");
                        }
                        break;

                    case 0:
                        Console.WriteLine("Tam biet!");
                        break;

                    default:
                        Console.WriteLine("Lua chon khong hop le, vui long chon lai!");
                        break;
                }

                if (luaChon != 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Nhan Enter de tiep tuc...");
                    Console.ReadLine();
                }

            } while (luaChon != 0);
        }

        static void HienThiMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===== MENU =====");
            Console.WriteLine("1. Nhap mang");
            Console.WriteLine("2. Xuat mang");
            Console.WriteLine("3. Tinh tong");
            Console.WriteLine("4. Tim max/min");
            Console.WriteLine("5. Dem chan/le");
            Console.WriteLine("6. Sap xep tang dan");
            Console.WriteLine("7. Tim kiem");
            Console.WriteLine("0. Thoat");
        }

        // Kiem tra da nhap mang chua truoc khi cho xu ly cac chuc nang khac
        static bool KiemTraDaNhapMang(int[] a)
        {
            if (a == null || a.Length == 0)
            {
                Console.WriteLine("Ban chua nhap mang, vui long chon chuc nang 1 truoc!");
                return false;
            }
            return true;
        }

        // Nhap mot so nguyen bat ky, lap lai neu nhap sai dinh dang
        static int NhapSoNguyen(string message)
        {
            int so;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (int.TryParse(input, out so))
                    return so;
                Console.WriteLine("Vui long nhap dung dinh dang so nguyen!");
            }
        }

        // Nhap mot so nguyen duong, dung cho so luong phan tu cua mang
        static int NhapSoNguyenDuong(string message)
        {
            int so;
            while (true)
            {
                so = NhapSoNguyen(message);
                if (so > 0)
                    return so;
                Console.WriteLine("So luong phan tu phai la so nguyen duong, vui long nhap lai!");
            }
        }

        static int[] NhapMang()
        {
            int n = NhapSoNguyenDuong("Nhap so luong phan tu n: ");
            int[] a = new int[n];

            for (int i = 0; i < n; i++)
            {
                a[i] = NhapSoNguyen("Nhap phan tu thu " + (i + 1) + ": ");
            }

            Console.WriteLine("Nhap mang thanh cong!");
            return a;
        }

        static void XuatMang(int[] a)
        {
            Console.Write("Mang: ");
            for (int i = 0; i < a.Length; i++)
            {
                Console.Write(a[i] + " ");
            }
            Console.WriteLine();
        }

        static int TinhTong(int[] a)
        {
            int tong = 0;
            for (int i = 0; i < a.Length; i++)
            {
                tong += a[i];
            }
            return tong;
        }

        static int TimMax(int[] a)
        {
            int max = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] > max)
                    max = a[i];
            }
            return max;
        }

        static int TimMin(int[] a)
        {
            int min = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] < min)
                    min = a[i];
            }
            return min;
        }

        static int DemChan(int[] a)
        {
            int dem = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] % 2 == 0)
                    dem++;
            }
            return dem;
        }

        static int DemLe(int[] a)
        {
            int dem = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] % 2 != 0)
                    dem++;
            }
            return dem;
        }

        // Sap xep tang dan bang thuat toan bubble sort (don gian, de hieu)
        static void SapXepTangDan(int[] a)
        {
            for (int i = 0; i < a.Length - 1; i++)
            {
                for (int j = 0; j < a.Length - 1 - i; j++)
                {
                    if (a[j] > a[j + 1])
                    {
                        int tam = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = tam;
                    }
                }
            }
        }

        // Tim kiem tuyen tinh, tra ve vi tri dau tien tim thay, -1 neu khong co
        static int TimKiem(int[] a, int x)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == x)
                    return i;
            }
            return -1;
        }
    }
}