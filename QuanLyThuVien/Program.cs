using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien
{
    struct Book
    {
        public string BookCode;
        public string Title;
        public int PublicationYear;
        public int Price;
    }

    class Program
    {

        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;
            // Register();

            if (Login() == true)
            {
                FormMenu();
            }
            else
            {
                Console.Write("Nhập sai 3 lần chương trình kết thúc");
                Console.ReadKey();
            }

            /* while (attempts < maxAttempts)
             {
                 Console.Write("                             +------------------------------------------------------+ \n");
                 Console.Write("                             |                   ĐĂNG NHẬP HỆ THỐNG                 | \n");
                 Console.Write("                             +------------------------------------------------------+ \n");
                 Console.Write("                             Tài khoản: ");
                 string username = Console.ReadLine();

                 Console.Write("                             Mật khẩu: ");
                 string password = ReadPassword();
                 string hashedPassword = HashPassword(password);

                 if (CheckCredentials(username, hashedPassword))
                 {
                     int maxBooks = 100; // Số lượng tối đa cuốn sách trong thư viện
                     Book[] books = new Book[maxBooks];
                     int bookCount = 0;
                     while (true)
                     {
                         Console.OutputEncoding = Encoding.UTF8;
                         Console.WriteLine("\n---- Quan ly cuon sach trong thu vien ----");
                         Console.WriteLine("1. Nhap thong tin cuon sach");
                         Console.WriteLine("2. Xuat danh sach cac cuon sach");
                         Console.WriteLine("3. Tim kiem va sua gia cuon sach theo tua");
                         Console.WriteLine("4. Tim kiem va xoa cuon sach theo ma");
                         Console.WriteLine("5. Sap xep danh sach cuon sach tang dan theo ma");
                         Console.WriteLine("6. Sap xep danh sach cuon sach giam dan theo nam xuat ban");
                         Console.WriteLine("0. Thoat chuong trinh");
                         Console.Write("Nhap lua chon: ");
                         int choice;
                         int.TryParse(Console.ReadLine(), out choice);
                         switch (choice)
                         {
                             case 1:
                                 if (bookCount < maxBooks)
                                 {
                                     books[bookCount] = InputBook();
                                     bookCount++;
                                 }
                                 else
                                 {
                                     Console.WriteLine("Danh sach cuon sach da day. Khong the them  cuon sach moi.");
                                 }
                                 break;
                             case 2:
                                 if (bookCount > 0)
                                 {
                                     DisplayBooks(books);
                                 }
                                 else
                                 {
                                     Console.WriteLine("Danh sach cuon sach rong.");
                                 }
                                 break;
                             case 3:
                                 if (bookCount > 0)
                                 {
                                     Console.Write("Nhap tua sach can tim: ");
                                     string titleToFind = Console.ReadLine();
                                     Console.Write("Nhap gia moi cua cuon sach: ");
                                     int newPrice;
                                     int.TryParse(Console.ReadLine(), out newPrice);
                                     SearchAndUpdatePrice(books, titleToFind, newPrice);
                                 }
                                 else
                                 {
                                     Console.WriteLine("Danh sach cuon sach rong.");
                                 }
                                 break;
                             case 4:
                                 if (bookCount > 0)
                                 {
                                     Console.Write("Nhap ma sach can tim: ");
                                     string bookCodeToFind = Console.ReadLine();
                                     SearchAndDeleteByCode(ref books, bookCodeToFind);
                                 }
                                 else
                                 {
                                     Console.WriteLine("Danh sach cuon sach rong.");
                                 }
                                 break;
                             case 5:
                                 if (bookCount > 0)
                                 {
                                     SelectionSortByBookCode(books);
                                     Console.WriteLine("Da sap xep danh sach cac cuon sach tang dan theo ma sach.");

                                     DisplayBooks(books);

                                 }
                                 else
                                 {
                                     Console.WriteLine("Danh sach cuon sach rong.");
                                 }
                                 break;
                             case 6:
                                 if (bookCount > 0)
                                 {
                                     InsertionSortByPublicationYearDescending(books);
                                     Console.WriteLine("Da sap xep danh sach cac cuon sach giam dan theo nam xuat ban.");

                                     DisplayBooks(books);

                                 }
                                 else
                                 {
                                     Console.WriteLine("Danh sach cuon sach rong.");
                                 }
                                 break;
                             case 0:
                                 Console.WriteLine("Thoat chuong trinh.");
                                 return;
                             default:
                                 Console.WriteLine("Lua chon khong hop le.");
                                 break;
                         }
                     }
                 }
                 else
                 {
                     Console.WriteLine("Tài khoản hoặc Mật khẩu không đúng. Vui lòng nhập lại.");
                     attempts++;
                 }
             }
             Environment.Exit(0);*/
        }
        static void FormMenu()
        {
            int maxBooks = 100; // Số lượng tối đa cuốn sách trong thư viện
            Book[] books = new Book[maxBooks];
            int bookCount = 0;
            while (true)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("\n---- Quan ly cuon sach trong thu vien ----");
                Console.WriteLine("1. Nhap thong tin cuon sach");
                Console.WriteLine("2. Xuat danh sach cac cuon sach");
                Console.WriteLine("3. Tim kiem va sua gia cuon sach theo tua");
                Console.WriteLine("4. Tim kiem va xoa cuon sach theo ma");
                Console.WriteLine("5. Sap xep danh sach cuon sach tang dan theo ma");
                Console.WriteLine("6. Sap xep danh sach cuon sach giam dan theo nam xuat ban");
                Console.WriteLine("0. Thoat chuong trinh");
                Console.Write("Nhap lua chon: ");
                int choice;
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        if (bookCount < maxBooks)
                        {
                            books[bookCount] = InputBook();
                            bookCount++;
                        }
                        else
                        {
                            Console.WriteLine("Danh sach cuon sach da day. Khong the them  cuon sach moi.");
                        }
                        break;
                    case 2:
                        if (bookCount > 0)
                        {
                            DisplayBooks(books);
                        }
                        else
                        {
                            Console.WriteLine("Danh sach cuon sach rong.");
                        }
                        break;
                    case 3:
                        if (bookCount > 0)
                        {
                            Console.Write("Nhap tua sach can tim: ");
                            string titleToFind = Console.ReadLine();
                            Console.Write("Nhap gia moi cua cuon sach: ");
                            int newPrice;
                            int.TryParse(Console.ReadLine(), out newPrice);
                            SearchAndUpdatePrice(books, titleToFind, newPrice);
                        }
                        else
                        {
                            Console.WriteLine("Danh sach cuon sach rong.");
                        }
                        break;
                    case 4:
                        if (bookCount > 0)
                        {
                            Console.Write("Nhap ma sach can tim: ");
                            string bookCodeToFind = Console.ReadLine();
                            SearchAndDeleteByCode(ref books, bookCodeToFind);
                        }
                        else
                        {
                            Console.WriteLine("Danh sach cuon sach rong.");
                        }
                        break;
                    case 5:
                        if (bookCount > 0)
                        {
                            SelectionSortByBookCode(books);
                            Console.WriteLine("Da sap xep danh sach cac cuon sach tang dan theo ma sach.");

                            DisplayBooks(books);

                        }
                        else
                        {
                            Console.WriteLine("Danh sach cuon sach rong.");
                        }
                        break;
                    case 6:
                        if (bookCount > 0)
                        {
                            InsertionSortByPublicationYearDescending(books);
                            Console.WriteLine("Da sap xep danh sach cac cuon sach giam dan theo nam xuat ban.");

                            DisplayBooks(books);

                        }
                        else
                        {
                            Console.WriteLine("Danh sach cuon sach rong.");
                        }
                        break;
                    case 0:
                        Console.WriteLine("Thoat chuong trinh.");
                        return;
                    default:
                        Console.WriteLine("Lua chon khong hop le.");
                        break;
                }
            }
        }
        static bool Login()
        {
            int attempts = 0;
            int maxAttempts = 3;
            while (attempts < maxAttempts)
            {
                string username = "";
                string password = "";
                Console.Write("                             +------------------------------------------------------+ \n");
                Console.Write("                             |                   ĐĂNG NHẬP HỆ THỐNG                 | \n");
                Console.Write("                             +------------------------------------------------------+ \n");
                Console.Write("                             Tài khoản: ");
         
                while (string.IsNullOrWhiteSpace(username))
                {        
                    username = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(username))
                    {
                        Console.Write("                             Tên tài khoản không được để trống.\n" +
                            "                             Mời nhập lại: ");
                       
                    }
                }
                Console.Write("                             Mật khẩu: ");
                
                while (string.IsNullOrWhiteSpace(password))
                {
                //    Console.Write("Nhập mật khẩu: ");
                    password = ReadPassword();


                    if (string.IsNullOrWhiteSpace(password))
                    {
                        Console.Write("                             Mật khẩu không được để trống.\n " +
                            "                            Mời nhập lại: ");
                    }
                }
             

                string hashedPassword = HashPassword(password);
                if (CheckCredentials(username, hashedPassword))
                {
                    Console.WriteLine("                             Đăng Nhập Thành Công!");
                    return true;
                }
                else
                {
                    Console.WriteLine("                             Tài khoản hoặc Mật khẩu không đúng. Vui lòng nhập lại.");
                    Console.ReadKey();
                    Console.Clear();
                    attempts++;
                  
                }
            }

            return false;
        }

        static bool CheckCredentials(string username, string password)
        {
            string filePath = "Admin.txt";

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length == 2 && parts[0] == username && parts[1] == password)
                    {
                        return true;
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading credentials file: {ex.Message}");
            }

            return false;
        }
        static void CreateUser()
        {
            string newUsername = "";
            string newPassword = "";

            while (string.IsNullOrWhiteSpace(newUsername))
            {
                Console.Write("Nhập tên tài khoản: ");
                newUsername = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newUsername))
                {
                    Console.WriteLine("Tên tài khoản không được để trống. Vui lòng thử lại.");
                }
            }

            while (string.IsNullOrWhiteSpace(newPassword))
            {
                Console.Write("Nhập mật khẩu: ");
                newPassword = ReadPassword();

                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    Console.WriteLine("Mật khẩu không được để trống. Vui lòng thử lại.");
                }
            }
            string newUserLine = $"{newUsername},{newPassword}\n";

            string filePath = "Admin.txt";

            try
            {
                File.AppendAllText(filePath, newUserLine);
                Console.WriteLine("Tài khoản được tạo thành công!");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error writing to credentials file: {ex.Message}");
            }
        }
        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
        // Nhập thông tin cuốn sách
        public static Book InputBook()
        {
            Book book = new Book();
            Console.Write("Nhap ma sach (6 ky tu): ");
            book.BookCode = Console.ReadLine();

            Console.Write("Nhap tua sach (toi da 30 ky tu): ");
            book.Title = Console.ReadLine();

            Console.Write("Nhap nam xuat ban (lon hon 1900): ");
            int.TryParse(Console.ReadLine(), out book.PublicationYear);

            Console.Write("Nhap gia sach (toi da 6 chu so): ");
            int.TryParse(Console.ReadLine(), out book.Price);

            return book;
        }
        // Xuất thông tin cuốn sách
        public static void DisplayBook(Book book)
        {
            Console.WriteLine($"Ma sach: {book.BookCode}, Tua sach: {book.Title},   Nam xuat ban: {book.PublicationYear}, Gia: {book.Price}   ");
        }
        // Xuất danh sách các cuốn sách
        public static void DisplayBooks(Book[] books)
        {
            Console.WriteLine("\nDanh sach cac cuon sach trong thu vien:");
            foreach (var book in books)
            {
                DisplayBook(book);
            }
        }
        // Tìm kiếm một cuốn sách theo tựa sách và sửa giá của cuốn sách đó
        public static void SearchAndUpdatePrice(Book[] books, string titleToFind, int
       newPrice)
        {
            bool found = false;
            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].Title == titleToFind)
                {
                    books[i].Price = newPrice;
                    found = true;
                    Console.WriteLine($"Tim thay tua sach '{titleToFind}' va da cap nhat gia     moi: {newPrice}  ");
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine($"Khong tim thay tua sach '{titleToFind}' trong danh  sach.");
            }
        }
        // Tìm kiếm một cuốn sách theo mã sách và xóa cuốn sách đó ra khỏi danh sách
        public static void SearchAndDeleteByCode(ref Book[] books, string bookCodeToFind)
        {
            int foundIndex = -1;
            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].BookCode == bookCodeToFind)
                {
                    foundIndex = i;
                    break;
                }
            }
            if (foundIndex != -1)
            {
                Array.Copy(books, foundIndex + 1, books, foundIndex, books.Length -
               foundIndex - 1);
                Array.Resize(ref books, books.Length - 1);
                Console.WriteLine($"Da xoa cuon sach co ma sach '{bookCodeToFind}' ra    khoi danh sach.");
            }
            else
            {
                Console.WriteLine($"Khong tim thay cuoi sach co ma sach     '{bookCodeToFind}' trong danh sach.");
            }
        }
        // Sắp xếp danh sách các cuốn sách tăng dần theo mã sách
        public static void SelectionSortByBookCode(Book[] books)
        {
            for (int i = 0; i < books.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < books.Length; j++)
                {
                    if (String.Compare(books[j].BookCode, books[minIndex].BookCode) <
                   0)
                    {
                        minIndex = j;
                    }
                }
                if (minIndex != i)
                {
                    Book temp = books[i];
                    books[i] = books[minIndex];
                    books[minIndex] = temp;
                }
            }
        }
        // Sắp xếp danh sách các cuốn sách giảm dần theo năm xuất bản
        public static void InsertionSortByPublicationYearDescending(Book[] books)
        {
            for (int i = 1; i < books.Length; i++)
            {
                Book key = books[i];
                int j = i - 1;
                while (j >= 0 && books[j].PublicationYear < key.PublicationYear)
                {
                    books[j + 1] = books[j];
                    j = j - 1;
                }
                books[j + 1] = key;
            }
        }
        private static int Partition(Book[] books, int low, int high)
        {
            int pivot = books[high].Price;
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (books[j].Price >= pivot)
                {
                    i++;
                    Book temp = books[i];
                    books[i] = books[j];
                    books[j] = temp;
                }
            }
            Book temp2 = books[i + 1];
            books[i + 1] = books[high];
            books[high] = temp2;
            return i + 1;
        }

        static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
     

     
        static void Register()
        {

            string newUsername = "";
            string newPassword = "";
            Console.WriteLine("Đăng ký tài khoản:");
            while (string.IsNullOrWhiteSpace(newUsername))
            {
                Console.Write("Nhập tên tài khoản: ");
                newUsername = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newUsername))
                {
                    Console.WriteLine("Tên tài khoản không được để trống. Vui lòng thử lại.");
                }
            }

            while (string.IsNullOrWhiteSpace(newPassword))
            {
                Console.Write("Nhập mật khẩu: ");
                newPassword = ReadPassword();

                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    Console.WriteLine("Mật khẩu không được để trống. Vui lòng thử lại.");
                }
            }
            string hashedPassword = HashPassword(newPassword);
            string newUserLine = $"{newUsername},{hashedPassword}\n";
            string filePath = "Admin.txt";

            try
            {
                File.AppendAllText(filePath, newUserLine);
                Console.WriteLine("Tài khoản được tạo thành công!");
                Console.ReadKey();  
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error writing to credentials file: {ex.Message}");
            }

        }
        static void LibraryMenu()
        {

            while (true)
            {
                int maxBooks = 100; // Số lượng tối đa cuốn sách trong thư viện
                Book[] books = new Book[maxBooks];

                Console.WriteLine("1. Hiển thị thông tin sách");
                Console.WriteLine("2. Thêm sách");
                Console.WriteLine("3. Xóa sách");
                Console.WriteLine("Nhấn ESC để thoát");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Exiting...");
                    break;
                }

                switch (key.KeyChar)
                {
                    case '1':
                        DisplayBooks(books);
                        break;
                    case '2':
                        InputBook();
                        break;
                    case '3':

                        break;
                    default:
                        Console.WriteLine("Lựa chọn không đúng. Vui lòng chọn lại.");
                        break;
                }
            }
        }
    }
}

    

