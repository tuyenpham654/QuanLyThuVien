using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien
{
    struct BanDoc
    {
        private int idBanDoc;
        private string name;
        private DateTime ngayDK;

        public int IdBanDoc { get => idBanDoc; set => idBanDoc = value; }
        public string Name { get => name; set => name = value; }
        public DateTime NgayDK { get => ngayDK; set => ngayDK = value; }

        public void DisplayBDInformation()
        {
            Console.WriteLine("              Thông tin bạn đọc thư viện:");

            // Đọc thông tin sách từ file BanDoc.txt
            List<string> BD = File.ReadAllLines("BanDoc.txt").ToList();
            Console.WriteLine("              +===============================================================+");
            Console.WriteLine("              | Mã Bạn Đọc |         Họ và tên        |      Ngày đăng kí     |");

            foreach (var bd in BD)
            {
                string[] parts = bd.Split(';');
               
                Console.WriteLine("               ________________________________________________________________");
                Console.WriteLine($"              |    {parts[0],-7}| {parts[1],-26}| {parts[2],-22}|");


            }
            Console.WriteLine("              +===============================================================+\n");
            Console.ReadKey();
            Console.Clear();

        }
        public void AddBD()
        {

            int idBanDoc;
            string name;
            DateTime ngayDK;
            bool kiemtra;
            

            Console.WriteLine("              Nhập thông tin bạn đọc:");

            Console.Write("              Mã bạn đọc: ");
            kiemtra = int.TryParse(Console.ReadLine(), out idBanDoc);
            while (kiemtra == false || idBanDoc <= 0)
            {
                Console.Write("              Dữ liệu không hợp lệ, mời nhập lại: ");
                kiemtra = int.TryParse(Console.ReadLine(), out idBanDoc);
            }



            // Kiểm tra mã sách có tồn tại không
            if (IsBDExist(idBanDoc))
            {
                Console.WriteLine("              Mã Bạn Đọc đã tồn tại.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.Write("              Họ và tên bạn đọc: ");
            name = Console.ReadLine();


           

            Console.Write("              Ngày đăng kí: ");
            kiemtra = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngayDK);
            while (kiemtra == false)
            {
                Console.Write("              Định dạng không hợp lệ, mời nhập lại (dd/mm/yyyy): ");
                kiemtra = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngayDK);
            }

            


            string newBD = ($"{idBanDoc};{name};{ngayDK.ToString("dd-MM-yyyy")}");


            // Ghi thông tin sách vào file Sach.txt
            using (StreamWriter sw = File.AppendText("BanDoc.txt"))
            {
                sw.WriteLine(newBD);
                Console.WriteLine("              Đã thêm bạn đọc thành công.");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void RemoveBD()
        {
            bool kiemTra;
            Console.Write("              Nhập mã bạn đọc để xóa: ");
            string maBDToRemove = Console.ReadLine();
            do
            {
                if (IsNumeric(maBDToRemove))
                {
                    // Hop le
                    // Đọc thông tin sách từ file Sach.txt
                    List<string> books = File.ReadAllLines("BanDoc.txt").ToList();
                    int ma = int.Parse(maBDToRemove);
                    if (IsBDExist(ma))
                    {
                        for (int i = 0; i < books.Count; i++)
                        {
                            string[] parts = books[i].Split(';');

                            if (parts[0].Equals(maBDToRemove))
                            {
                                books.RemoveAt(i);
                                Console.WriteLine("              Xóa bạn đọc thành công.");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                        }
                        // Ghi đè thông tin sách vào file Sach.txt
                        File.WriteAllLines("BanDoc.txt", books);
                        return;
                    }
                    else
                    {

                        Console.WriteLine("              Mã bạn đọc không tồn tại.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }


                }
                else
                {
                    Console.Write("              Nhập sai định dạng, vui  lòng nhập lại: ");
                    maBDToRemove = Console.ReadLine();
                    kiemTra = false;
                }
            } while (kiemTra == false);

        }
        public static bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }
        static bool IsBDExist(int idBanDoc)
        {
            // Đọc thông tin sách từ file Sach.txt
            List<string> books = File.ReadAllLines("BanDoc.txt").ToList();
            bool check = false;
            int ma;
            foreach (var book in books)
            {
                string[] parts = book.Split(';');
                ma = int.Parse(parts[0].Trim());
                if (ma == idBanDoc)
                {
                    check = true;
                    break;
                }
            }

            return check;
        }
    }
    
}
