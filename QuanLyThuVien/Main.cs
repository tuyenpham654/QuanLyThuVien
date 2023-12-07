using System;
using System.Collections.Generic;
using System.Linq;
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
    internal class Main
    {
        
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
        static void Main(string[] args)
        {
            
                
        }
    }
}
