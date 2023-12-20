using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien
{
    struct Book
    {
        static List<Book> books = new List<Book>();
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
            Console.WriteLine("              +===================================================================================================================================================================+\n");
            Console.Write("              Nhấn T để xem chi tiết. nhấn phím bất kỳ để quay lại\n");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.T)
            {
                SearchBookByCode();
            }
            Console.Clear();
            return;
            
        }
        static void SearchBookByCode()
        {
            while (true)
            {
                Console.Write("\b \b");
                Console.Write("\n              Nhập mã sách cần tìm: ");
                string maSachToSearch = Console.ReadLine();
                bool find = false;

                List<string> books = File.ReadAllLines("Sach.txt").ToList();

                foreach (var book in books)
                {
                    string[] parts = book.Split(';');

                    if (parts[0].Equals(maSachToSearch))
                    {
                        if (parts[8] == "0")
                        {
                            parts[8] = "chưa mượn";
                        }
                        else
                        {
                            parts[8] = "đang mượn";
                        }
                        Console.WriteLine($"              Thông tin sách có mã {maSachToSearch}:");
                        Console.WriteLine("              +========================================+");
                        Console.WriteLine($"              | Mã sách         | {parts[0],-20} |\n" +
                            "               ________________________________________\n" +
                            $"              | Tên sách        | {parts[1],-20} |\n" +
                            "               ________________________________________\n" +
                            $"              | Tác giả         | {parts[2],-20} |\n" +
                            "               ________________________________________\n" +
                            $"              | Nhà xuất bản    | {parts[3],-20} |\n" +
                            "               ________________________________________\n" +
                            $"              | Giá bán         | {parts[4],-20} |\n" +
                            "               ________________________________________\n" +
                            $"              | Năm phát hành   | {parts[5],-20} |\n" +
                            "               ________________________________________\n" +
                            $"              | Số trang        | {parts[6],-20} |\n" +
                            "               ________________________________________\n" +
                            $"              | Ngày nhập kho   | {parts[7],-20} |\n" +
                            "               ________________________________________\n" +
                            $"              | Tình trạng sách | {parts[8],-20} |\n"+
                            "              +========================================+\n");

                        //Console.ReadKey();
                        find = true;
                        break;
                    }
                }

                if (!find)
                {
                    Console.WriteLine($"              Không tìm thấy sách có mã {maSachToSearch}.");
                }

                Console.Write("              Tiếp tục tìm kiếm? (Y): ");
                char choice = Console.ReadKey().KeyChar;

                if (char.ToUpper(choice) != 'Y')
                {
                    Console.Clear();
                    break;
                    
                }
            }
            
        }
       
        public void AddBook()
        {

            int maSach;
            string tenSach;
            string tacgia;
            string nhaXuatBan;
            double giaBan;
            DateTime namPhatHanh;
            int soTrang;
            DateTime ngayNhapKho;
            int tinhTrangSach;
            bool kiemtra;

            Console.WriteLine("              Nhập thông tin sách mới:");

            Console.Write("              Mã sách: ");
            kiemtra = int.TryParse(Console.ReadLine(), out maSach);
            while (kiemtra == false||maSach<0)
            {
                Console.Write("              Dữ liệu không hợp lệ, mời nhập lại: ");
                kiemtra = int.TryParse(Console.ReadLine(), out maSach);
            }



            // Kiểm tra mã sách có tồn tại không
            if (IsBookExist(maSach))
            {
                Console.WriteLine("              Sách đã tồn tại trong thư viện.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.Write("              Tên sách: ");
            tenSach = Console.ReadLine();

            Console.Write("              Tác giả: ");
            tacgia = Console.ReadLine();

            Console.Write("              Nhà xuất bản: ");
            nhaXuatBan = Console.ReadLine();

            Console.Write("              giá bán: ");
           // giaBan = double.Parse(Console.ReadLine());
            kiemtra = double.TryParse(Console.ReadLine(), out giaBan);
            while (kiemtra == false||giaBan<0)
            {
                Console.Write("              Dữ liệu không hợp lệ, mời nhập lại: ");
                kiemtra = double.TryParse(Console.ReadLine(), out giaBan);
            }

            Console.Write("              Năm Phát hành: ");
            kiemtra = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out namPhatHanh);
            while (kiemtra == false)
            {
                Console.Write("              Định dạng không hợp lệ, mời nhập lại (dd/mm/yyyy): ");
                kiemtra = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out namPhatHanh);
            }

            Console.Write("              Số trang: ");
            kiemtra = int.TryParse(Console.ReadLine(), out soTrang);
            while (kiemtra == false || soTrang < 0)
            {
                Console.Write("              Dữ liệu không hợp lệ, mời nhập lại: ");
                kiemtra = int.TryParse(Console.ReadLine(), out soTrang);
            }

            Console.Write("              Ngày nhập kho: ");
            kiemtra = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngayNhapKho);
            while (kiemtra == false)
            {
                Console.Write("              Định dạng không hợp lệ, mời nhập lại (dd/mm/yyyy): ");
                kiemtra = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngayNhapKho);
            }


            tinhTrangSach = 0;

            string newBook= ($"{maSach};{tenSach};{tacgia};{nhaXuatBan};{giaBan};{namPhatHanh.ToString("dd-MM-yyyy")};{soTrang};{ngayNhapKho.ToString("dd-MM-yyyy")};{tinhTrangSach}");


            // Ghi thông tin sách vào file Sach.txt
            using (StreamWriter sw = File.AppendText("Sach.txt"))
            {
                sw.WriteLine(newBook);
                Console.WriteLine("              Sách đã được thêm vào thư viện.");
                Console.ReadKey();
                Console.Clear();
            }
        }


        public static bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }

        // xóa
        public void RemoveBook()
        {
            bool kiemTra;
            Console.Write("              Nhập mã sách để xóa: ");
            string maSachToRemove = Console.ReadLine();
            do
            {
                if (IsNumeric(maSachToRemove))
                {
                    // Hop le
                    // Đọc thông tin sách từ file Sach.txt
                    List<string> books = File.ReadAllLines("Sach.txt").ToList();
                    int ma = int.Parse(maSachToRemove);
                    if (IsBookExist(ma))
                    { 
                        for (int i = 0; i < books.Count; i++)
                        {
                            string[] parts = books[i].Split(';');

                            if (parts[0].Equals(maSachToRemove) && parts[8].Equals("0"))
                            {
                                books.RemoveAt(i);
                                Console.WriteLine("              Sách đã được xóa khỏi thư viện.");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            else if (parts[0].Equals(maSachToRemove) && !parts[8].Equals("0"))
                            {
                                Console.WriteLine("              Không thể xóa sách đang được mượn.");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                        }
                        // Ghi đè thông tin sách vào file Sach.txt
                        File.WriteAllLines("Sach.txt", books);
                        return;
                    }
                    else
                    {

                        Console.WriteLine("              Mã sách không tồn tại.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }

                   
                }
                else
                {
                    Console.Write("              Nhập sai định dạng, vui  lòng nhập lại: ");
                    maSachToRemove = Console.ReadLine();
                    kiemTra = false;
                }
            }while (kiemTra==false);
          
        }

      

        // cập nhật
        public void UpdateBook()
        {
            Console.Write("Nhập mã sách cần cập nhật: ");
            string maSachToUpdate = Console.ReadLine();

            List<string> books = File.ReadAllLines("Sach.txt").ToList();
            bool found = false;

            for (int i = 0; i < books.Count; i++)
            {
                string[] parts = books[i].Split(';');
                string trangThai = "";
                bool kiemTra;
                if (parts[0].Equals(maSachToUpdate))
                {
                    Console.WriteLine($"              Thông tin sách có mã {maSachToUpdate}:");
                    if (parts[8] == "0")
                    {
                        trangThai = "chưa mượn";
                    }
                    else
                    {
                        trangThai = "đang mượn";
                    }
                    Console.WriteLine("              +========================================+");
                    Console.WriteLine($"              | Mã sách         | {parts[0],-20} |\n" +
                        "               ________________________________________\n" +
                        $"              | Tên sách        | {parts[1],-20} |\n" +
                        "               ________________________________________\n" +
                        $"              | Tác giả         | {parts[2],-20} |\n" +
                        "               ________________________________________\n" +
                        $"              | Nhà xuất bản    | {parts[3],-20} |\n" +
                        "               ________________________________________\n" +
                        $"              | Giá bán         | {parts[4],-20} |\n" +
                        "               ________________________________________\n" +
                        $"              | Năm phát hành   | {parts[5],-20} |\n" +
                        "               ________________________________________\n" +
                        $"              | Số trang        | {parts[6],-20} |\n" +
                        "               ________________________________________\n" +
                        $"              | Ngày nhập kho   | {parts[7],-20} |\n" +
                        "               ________________________________________\n" +
                        $"              | Tình trạng sách | {trangThai,-20} |\n" +
                        "              +========================================+\n");
                    // Nhập thông tin cập nhật
                    Console.WriteLine("              Nhập thông tin cập nhật (Nhấn Enter để giữ nguyên)");
                    Console.Write("              Tên sách: ");
                    string tenSach = Console.ReadLine();
                    if (tenSach == "") tenSach = parts[1];

                    Console.Write("              Tác giả: ");
                    string tacGia = Console.ReadLine();
                    if (tacGia == "") tacGia = parts[2];

                    Console.Write("              Nhà xuất bản: ");
                    string nhaXuatBan= Console.ReadLine();
                    if (nhaXuatBan == "") tacGia = parts[3];

                    Console.Write("              Giá bán: ");
                    double giaBan = 0;
                    string giaBanInPut= Console.ReadLine();
                 //    giaBan=double.Parse(Console.ReadLine());
                    if (giaBanInPut == "") giaBan = double.Parse(parts[4]);


                    Console.Write("              Năm Phát hành: ");

                    DateTime namPhatHanh;
                    kiemTra = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out namPhatHanh);
                 /*   if (namPhatHanhInPut == "" || DateTime.TryParse(namPhatHanhInput, out namPhatHanh))
                    { namPhatHanh = DateTime.Parse(parts[5]); }
                    else
                    {
                        //   kiemTra = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out namPhatHanh);
                        while (kiemTra == false)
                        {
                            Console.Write("              Định dạng không hợp lệ, mời nhập lại (dd/mm/yyyy): ");
                            kiemTra = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out namPhatHanh);
                        }
                    }*/
                    if (namPhatHanh == null)
                    {
                        namPhatHanh = DateTime.Parse(parts[5]);
                    }
                    kiemTra = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out namPhatHanh);
                    //   kiemtra = uint.TryParse(Console.ReadLine(), out uint maPhieu);
                    while (kiemTra == false)
                    {
                        Console.Write("              Định dạng không hợp lệ, mời nhập lại (dd/mm/yyyy): ");
                        kiemTra = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out namPhatHanh);
                    }


                    // Cập nhật các thông tin khác của sách...

                    // Tạo thông tin sách mới
                    string updatedBook = $"{maSachToUpdate},{tenSach},{tacGia},..."; // Cập nhật các thông tin khác của sách

                    // Cập nhật thông tin sách trong danh sách
                    books[i] = updatedBook;

                    Console.WriteLine($"Thông tin sách đã được cập nhật.");
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Không tìm thấy sách có mã {maSachToUpdate}.");
            }

            // Ghi danh sách sách đã được cập nhật vào file
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