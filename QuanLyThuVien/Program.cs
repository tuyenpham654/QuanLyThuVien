
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static QuanLyThuVien.Book;
using static QuanLyThuVien.PhieuMuon;
namespace QuanLyThuVien
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;
            if (Login(0))
            {
                Console.Clear();
                Console.WriteLine("Đăng nhập thành công!");
                // Hiển thị menu
                while (true)
                {
                    ShowMainMenu();
                    int choice = GetChoice(1, 3);

                    switch (choice)
                    {
                        case 1:
                            ManageBooks();
                            break;
                        case 2:
                            ManageBorrowing();
                            break;
                        case 3:
                            Console.WriteLine("Thoát khỏi hệ thống.");
                            return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Đăng nhập thất bại. Thoát khỏi hệ thống.");
            }
        }
      
        static bool Login(int atm)
        {
            Console.WriteLine("Nhấn Esc để thoát chương trình");
            int attempt = 0;
            attempt = atm;
            while (attempt < 3)
            {
                string userName = "";
                string pass = "";
                Console.Write("                             +------------------------------------------------------+ \n");
                Console.Write("                             |                   ĐĂNG NHẬP HỆ THỐNG                 | \n");
                Console.Write("                             +------------------------------------------------------+ \n");
                Console.Write("                             Tài khoản: ");


                while (string.IsNullOrWhiteSpace(userName))
                {
                    userName = ReadUser(attempt);

                    if (string.IsNullOrWhiteSpace(userName))
                    {
                        Console.Write("                             Tên tài khoản không được để trống.\n" +
                            "                             Mời nhập lại: ");

                    }
                }
                Console.Write("                             Mật khẩu: ");

                while (string.IsNullOrWhiteSpace(pass))
                {
                    //    Console.Write("Nhập mật khẩu: ");
                    pass = ReadPassword(attempt);


                    if (string.IsNullOrWhiteSpace(pass))
                    {
                        Console.Write("                             Mật khẩu không được để trống.\n " +
                            "                            Mời nhập lại: ");
                    }
                }
                string hashedPassword = HashPassword(pass);
                if (CheckCredentials(userName, hashedPassword))
                {
                    return true;
                }
                else
                {
                    attempt++;
                    Console.WriteLine($"                             Đăng nhập thất bại. Bạn còn {3 - attempt} lần thử. \nNhấn Esc để kết thúc chương trình");
                    ConsoleKeyInfo keyInfor = Console.ReadKey();
                    if (keyInfor.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                        //Console.ReadKey();
                    Console.Clear();
                }



                
            }

            return false;
        }

        

        // định dạng tài khoản: xóa dấu cách

        static string ReadUser(int attempt)
        {
            string userName = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    Console.Write($"\n                             Bạn còn {3 - attempt} lần thử. \nNhấn Esc để kết thúc chương trình. Nhấn phím bất kỳ để hủy");
                    key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.Clear();
                        Login(attempt);
                    }
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    userName += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && userName.Length > 0)
                {
                    userName = userName.Substring(0, userName.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            
            return userName;
        }

        // định dạng mật khẩu khi nhập thành ****
        static string ReadPassword(int attempt)
        {
            string pass = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

               // ConsoleKeyInfo keyInfor = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    // Console.Clear();
                    Console.Write($"\n                             Bạn còn {3-attempt} lần thử. \nNhấn Esc để kết thúc chương trình. Nhấn phím bất kỳ để hủy");
                    key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.Clear();
                        Login(attempt);
                    }
                }
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass = pass.Substring(0, pass.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return pass;
        }






        // băm mật khẩu
        static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        static bool CheckCredentials(string username, string password)
        {
            // Đọc danh sách tài khoản từ file Admin.txt
            List<string> accounts = File.ReadAllLines("Admin.txt").ToList();

            foreach (var account in accounts)
            {
                string[] parts = account.Split(',');
                string storedUsername = parts[0].Trim();
                string storedPassword = parts[1].Trim();

                if (username == storedUsername && password == storedPassword)
                {
                    return true;
                }
            }

            return false;
        }


        static void ShowMainMenu()
        {
            Console.WriteLine("Chọn chức năng:");
            Console.WriteLine("1. Quản lý sách.");
            Console.WriteLine("2. Quản lý phiếu mượn.");
            Console.WriteLine("3. Thoát.");
        }

        static int GetChoice(int min, int max)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
            {
                Console.WriteLine("Nhập lại lựa chọn từ {0} đến {1}.", min, max);
            }
            return choice;
        }

        static void ManageBooks()
        {
            while (true)
            {
                Book books = new Book();
                Console.WriteLine("Chọn chức năng Quản lý sách:");
                Console.WriteLine("1. Hiển thị thông tin sách.");
                Console.WriteLine("2. Thêm sách.");
                Console.WriteLine("3. Xóa sách.");
                Console.WriteLine("4. Quay lại menu chính.");

                int choice = GetChoice(1, 4);

                switch (choice)
                {
                    case 1:

                        books.DisplayBookInformation();
                        break;
                    case 2:
                        books.AddBook();
                        break;
                    case 3:
                        books.RemoveBook();
                        break;
                    case 4:
                        return;
                }
            }
        }


        static void ManageBorrowing()
        {
            while (true)
            {
                PhieuMuon phieu = new PhieuMuon();
                Console.WriteLine("Chọn chức năng Quản lý phiếu mượn:");
                Console.WriteLine("1. Hiển thị thông tin phiếu mượn.");
                Console.WriteLine("2. Mượn sách.");
                Console.WriteLine("3. Trả sách.");
                Console.WriteLine("4. Quay lại menu chính.");

                int choice = GetChoice(1, 4);

                switch (choice)
                {
                    case 1:
                        phieu.DisplayBorrowingInformation();
                        break;
                    case 2:
                        phieu.BorrowBook();
                        break;
                    case 3:
                        phieu.ReturnBook();
                        break;
                    case 4:
                        return;
                }
            }
        }

    }
}
