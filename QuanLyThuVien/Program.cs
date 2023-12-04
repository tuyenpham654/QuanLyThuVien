using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien
{
    class Program
    {
        static void Main()
        {
            if (Login())
            {
                MainMenu();
            }
            else
            {
                Console.WriteLine("Đăng nhập sai quá số lần cho phép. Thoát chương trình...");
            }
        }

        static bool Login()
        {
            int attempts = 0;
            int maxAttempts = 3;

            while (attempts < maxAttempts)
            {
                Console.Write("                             +------------------------------------------------------+ \n");
                Console.Write("                             |                   ĐĂNG NHẬP HỆ THỐNG                 | \n");
                Console.Write("                             +------------------------------------------------------+ \n");
                Console.Write("                             Tài khoản: ");
                string username = Console.ReadLine();

                Console.Write("                             Mật khẩu: ");
                string password = ReadPassword();

                if (CheckCredentials(username, password))
                {
                    Console.WriteLine("Đăng Nhập Thành Công!");
                    return true;
                }
                else
                {
                    Console.WriteLine("Tài khoản hoặc Mật khẩu không đúng. Vui lòng nhập lại.");
                    attempts++;
                }
            }

            return false;
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Quản lý sách");
                Console.WriteLine("2. Quản lý phiếu mượn");
                Console.WriteLine("3. Tạo Tài khoản người dùng mới");
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
                        Console.WriteLine("Chức năng 1 được chọn (Quản lý sách).");
                        LibraryMenu();
                        break;
                    case '2':
                        Console.WriteLine("Chức năng 2 được chọn (Quản lý phiếu mượn).");
                        break;
                    case '3':
                        Console.WriteLine("Chức năng 3 được chọn (Tạo tài khoản người dùng mới).");
                        CreateUser();
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không đúng. Vui lòng chọn lại.");
                        break;
                }
            }
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
        static void LibraryMenu()
        {
            while (true)
            {
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
                        DisplayBookInfo();
                        break;
                    case '2':
                        AddBook();
                        break;
                    case '3':
                        RemoveBook();
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không đúng. Vui lòng chọn lại.");
                        break;
                }
            }
        }

        static void DisplayBookInfo()
        {
            string filePath = "Sach.txt";

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                Console.WriteLine("Thông tin sách:");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length == 9)
                    {
                        Console.WriteLine($"Mã sách: {parts[0]}, Tên sách: {parts[1]}, Tác giả: {parts[2]}, Nhà xuất bản: {parts[3]}, Giá bán: {parts[4]}, Năm phát hành: {parts[5]}, Số trang: {parts[6]}, Ngày nhập kho: {parts[7]}, Tình trạng sách: {parts[8]}");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading book file: {ex.Message}");
            }
        }

        static void AddBook()
        {
            Console.Write("Nhập mã sách: ");
            string bookCode = Console.ReadLine();

            if (BookExists(bookCode))
            {
                Console.WriteLine("Sách đã tồn tại. Không thêm mới.");
                return;
            }

            Console.Write("Nhập tên sách: ");
            string bookName = Console.ReadLine();

            Console.Write("Nhập tác giả: ");
            string author = Console.ReadLine();

            Console.Write("Nhập nhà xuất bản: ");
            string publisher = Console.ReadLine();

            Console.Write("Nhập giá bán: ");
            string price = Console.ReadLine();

            Console.Write("Nhập năm phát hành: ");
            string releaseYear = Console.ReadLine();

            Console.Write("Nhập số trang: ");
            string totalPages = Console.ReadLine();

            Console.Write("Nhập ngày nhập kho: ");
            string importDate = Console.ReadLine();

            // Tình trạng sách được đặt mặc định là Còn (chưa được mượn)
            string bookStatus = "Còn";

            string newBookLine = $"{bookCode},{bookName},{author},{publisher},{price},{releaseYear},{totalPages},{importDate},{bookStatus}\n";

            // Thêm sách mới vào file
            string filePath = "Sach.txt";

            try
            {
                File.AppendAllText(filePath, newBookLine);
                Console.WriteLine("Sách thêm mới thành công!");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error writing to book file: {ex.Message}");
            }
        }

        static void RemoveBook()
        {
            Console.Write("Nhập mã sách để xóa: ");
            string bookCodeToDelete = Console.ReadLine();

            if (!BookExists(bookCodeToDelete))
            {
                Console.WriteLine("Sách không tồn tại trong danh sách.");
                return;
            }

            string filePath = "Sach.txt";

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(',');

                    if (parts.Length == 9 && parts[0] == bookCodeToDelete && parts[8] == "0")
                    {
                        Console.WriteLine($"Đã xóa sách có mã {bookCodeToDelete}");
                        lines[i] = null;
                        break;
                    }
                    else if (parts.Length == 9 && parts[0] == bookCodeToDelete && parts[8] != "0")
                    {
                        Console.WriteLine("Không thể xóa sách đang được mượn.");
                        return;
                    }
                }

                File.WriteAllLines(filePath, lines.Where(line => line != null).ToArray());
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading/writing book file: {ex.Message}");
            }
        }

        static bool BookExists(string bookCode)
        {
            string filePath = "Sach.txt";

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length == 9 && parts[0] == bookCode)
                    {
                        return true;
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading book file: {ex.Message}");
            }

            return false;
        }
    }
}
