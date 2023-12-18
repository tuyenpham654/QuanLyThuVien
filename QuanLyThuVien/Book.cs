using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien
{
    struct Book
    {
        //static List<Book> books = new List<Book>();

        private int maSach;
        private string tenSach;
        private string tacgia;
        private string nhaXuatBan;
        private long giaBan;
        private DateTime namPhatHanh;
        private int soTrang;
        private DateTime ngayNhapKho;
        private int tinhTrangSach;

        public int MaSach { get => maSach; set => maSach = value; }
        public string TenSach { get => tenSach; set => tenSach = value; }
        public string Tacgia { get => tacgia; set => tacgia = value; }
        public long GiaBan { get => giaBan; set => giaBan = value; }
        public DateTime NamPhatHanh { get => namPhatHanh; set => namPhatHanh = value; }
        public int SoTrang { get => soTrang; set => soTrang = value; }
        public DateTime NgayNhapKho { get => ngayNhapKho; set => ngayNhapKho = value; }
        public int TinhTrangSach { get => tinhTrangSach; set => tinhTrangSach = value; }
        public string NhaXuatBan { get => nhaXuatBan; set => nhaXuatBan = value; }


        public void DisplayBookInformation()
        {
            Console.WriteLine("              Thông tin sách trong thư viện:");

            // Đọc thông tin sách từ file Sach.txt
            List<string> books = File.ReadAllLines("Sach.txt").ToList();
            Console.WriteLine("              +===================================================================================================================================================================+");
            Console.WriteLine("              | Mã Sách |          Tên Sách        |          Tác giả         |     Nhà xuất bản    |    Giá Bán     | Năm phát hành | Số trang | Ngày Nhập kho | Tình trạng sách |");
        
            foreach (var book in books)
            {
                string[] parts = book.Split(';');
                if( parts[8] == "0"){
                    parts[8]= "chưa mượn";
                    }
                else
                {
                    parts[8] = "đang mượn";
                }
                Console.WriteLine("               ___________________________________________________________________________________________________________________________________________________________________");
                Console.WriteLine($"              |    {parts[0],-5}| {parts[1],-25}| {parts[2],-25}| {parts[3],-20}| {parts[4],-15}| {parts[5],-14}|   {parts[6],-7}|  {parts[7],-13}|   {parts[8],-14}|");



                //  Console.WriteLine($"Mã sách: {parts[0]}, Tên sách: {parts[1]}, Tác giả: {parts[2]}, Nhà xuất bản: {parts[3]}, Giá bán: {parts[4]}, Năm phát hành: {parts[5]}, Số trang: {parts[6]}, Ngày nhập kho: {parts[7]}, Tình trạng sách: {parts[8]}");
            }
            Console.WriteLine("              +===================================================================================================================================================================+");

            Console.Write("              Nhập mã để xem chi tiết: ");
            int id=int.Parse(Console.ReadLine()); 
            FindID(id);
        }

        public void FindID(int ID)
        {
            List<string> books = File.ReadAllLines("Sach.txt").ToList();
            foreach (var book in books)
            {
                string[] parts = book.Split(';');
                int ma = int.Parse(parts[0].Trim());
                if (ma == ID)
                {
                    if (parts[8] == "0")
                    {
                        parts[8] = "chưa mượn";
                    }
                    else
                    {
                        parts[8] = "đang mượn";
                    }
                    Console.WriteLine($"Mã sách: {parts[0]}\n Tên sách: {parts[1]}\n Tác giả: {parts[2]}\n Nhà xuất bản: {parts[3]}\n Giá bán: {parts[4]}\n Năm phát hành: {parts[5]}\n Số trang: {parts[6]}\n Ngày nhập kho: {parts[7]}\n Tình trạng sách: {parts[8]}");
                    Console.ReadKey();
                    Console.Clear();
                }
             
                    

            }
 
        }
        public void AddBook()
        {

            int maSach;
            string tenSach;
            string tacgia;
            string nhaXuatBan;
            long giaBan;
            DateTime namPhatHanh;
            int soTrang;
            DateTime ngayNhapKho;
            int tinhTrangSach;
            bool kiemtra;

            Console.WriteLine("Nhập thông tin sách mới:");

            Console.Write("Mã sách: ");
            maSach = int.Parse(Console.ReadLine());



            // Kiểm tra mã sách có tồn tại không
            if (IsBookExist(maSach))
            {
                Console.WriteLine("Sách đã tồn tại trong thư viện.");
                return;
            }

            Console.Write("Tên sách: ");
            tenSach = Console.ReadLine();

            Console.Write("Tác giả: ");
            tacgia = Console.ReadLine();

            Console.Write("Nhà xuất bản: ");
            nhaXuatBan = Console.ReadLine();

            Console.Write("giá bán: ");
            giaBan = int.Parse(Console.ReadLine());


            Console.Write("Năm Phát hành: ");
            kiemtra = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out namPhatHanh);
            //   kiemtra = uint.TryParse(Console.ReadLine(), out uint maPhieu);
            while (kiemtra == false)
            {
                Console.Write("      Định dạng không hợp lệ, mời nhập lại (dd/mm/yyyy): ");
                kiemtra = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out namPhatHanh);
            }

            //Console.Write("Năm Phát hành: ");
            //namPhatHanh = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null).Date;

            Console.Write("Số trang: ");
            soTrang = int.Parse(Console.ReadLine());

            Console.Write("Ngày nhập kho: ");
            ngayNhapKho = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);


            tinhTrangSach = 0;




            // Ghi thông tin sách vào file Sach.txt
            using (StreamWriter sw = File.AppendText("Sach.txt"))
            {
                sw.WriteLine($"{maSach};{tenSach};{tacgia};{nhaXuatBan};{giaBan};{namPhatHanh.ToString("dd-MM-yyyy")};{soTrang};{ngayNhapKho.ToString("dd-MM-yyyy")};{tinhTrangSach}");
                Console.WriteLine("Sách đã được thêm vào thư viện.");
            }
        }

        public void RemoveBook()
        {
            Console.WriteLine("Nhập mã sách để xóa:");

            string maSachToRemove = Console.ReadLine();

            // Đọc thông tin sách từ file Sach.txt
            List<string> books = File.ReadAllLines("Sach.txt").ToList();

            for (int i = 0; i < books.Count; i++)
            {
                string[] parts = books[i].Split(';');

                if (parts[0].Equals(maSachToRemove) && parts[8].Equals("0"))
                {
                    books.RemoveAt(i);
                    Console.WriteLine("Sách đã được xóa khỏi thư viện.");
                    break;
                }
                else if (parts[0].Equals(maSachToRemove) && !parts[8].Equals("0"))
                {
                    Console.WriteLine("Không thể xóa sách đang được mượn.");
                    break;
                }
            }

            // Ghi đè thông tin sách vào file Sach.txt
            File.WriteAllLines("Sach.txt", books);
        }

        static bool IsBookExist(int maSach)
        {
            // Đọc thông tin sách từ file Sach.txt
            List<string> books = File.ReadAllLines("Sach.txt").ToList();
            bool check = false;
            int ma;
            foreach (var book in books)
            {
                string[] parts = book.Split(';');
                ma = int.Parse(parts[0].Trim());
                if (ma == maSach)
                {
                    check = true;
                    break;
                }

                /*
                                if (parts[0].Equals(maSach))
                                {
                                    return true;
                                }*/
            }

            return check;
        }
    }
}