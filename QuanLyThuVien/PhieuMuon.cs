using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien
{
    struct PhieuMuon
    {
        private int soPhieuMuon;
        private int maBanDoc;
        private int maSach;
        private DateTime ngayMuon;
        private DateTime ngayTra;
        private int tinhTrang;

        public int SoPhieuMuon { get => soPhieuMuon; set => soPhieuMuon = value; }
        public int MaBanDoc { get => maBanDoc; set => maBanDoc = value; }
        public int MaSach { get => maSach; set => maSach = value; }
        public DateTime NgayMuon { get => ngayMuon; set => ngayMuon = value; }
        public DateTime NgayTra { get => ngayTra; set => ngayTra = value; }
        public int TinhTrang { get => tinhTrang; set => tinhTrang = value; }


        public void DisplayBorrowingInformation()
        {
            Console.WriteLine("Thông tin phiếu mượn:");

            List<string> borrowings = File.ReadAllLines("PhieuMuon.txt").ToList();
            Console.WriteLine("              +==========================================================================================================+");
            Console.WriteLine("              | Số phiếu mượn | Mã bạn đọc  |   Mã sách  |    Ngày mượn    |    Ngày phải trả     |Tình trạng phiếu mượn |");

            foreach (var borrowing in borrowings)
            {
                string[] parts = borrowing.Split(';');
                Console.WriteLine("               ---------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"              | {parts[0],-14}| {parts[1],-11} | {parts[2],-11}| {parts[3],-16}| {parts[4],-20} | {parts[5],-21}|");
            }
            Console.WriteLine("              +==========================================================================================================+");
        }

        public void BorrowBook()
        {
            Console.WriteLine("Nhập mã sách để mượn:");

            string maSach = Console.ReadLine();

            if (IsBookAvailable(maSach))
            {
                Console.WriteLine("Nhập mã bạn đọc:");

                string maBanDoc = Console.ReadLine();

                if (IsReaderExist(maBanDoc))
                {
                    List<string> borrowings = File.ReadAllLines("PhieuMuon.txt").ToList();

                    int soPhieuMuon = borrowings.Count + 1;

                    DateTime ngayMuon = DateTime.Now;
                    DateTime ngayTra = ngayMuon.AddDays(7);

                    string newBorrowing = $"{soPhieuMuon};{maBanDoc};{maSach};{ngayMuon.ToString("dd/MM/yyyy")};{ngayTra.ToString("dd/MM/yyyy")}";
                    borrowings.Add(newBorrowing);

                    UpdateBookStatus(maSach, soPhieuMuon);

                    File.WriteAllLines("PhieuMuon.txt", borrowings);

                    Console.WriteLine("Đã tạo phiếu mượn thành công.");
                }
                else
                {
                    Console.WriteLine("Mã bạn đọc không tồn tại.");
                }
            }
            else
            {
                Console.WriteLine("Sách không có sẵn để mượn.");
            }
        }

        public void ReturnBook()
        {
            Console.WriteLine("Nhập số phiếu mượn để trả sách:");

            int soPhieuMuonToReturn = int.Parse(Console.ReadLine());

            List<string> borrowings = File.ReadAllLines("PhieuMuon.txt").ToList();

            foreach (var borrowing in borrowings)
            {
                string[] parts = borrowing.Split(';');

                if (int.Parse(parts[0]) == soPhieuMuonToReturn && parts[5] == "1")
                {
                    UpdateBookAndBorrowingStatus(parts[2], soPhieuMuonToReturn);

                    File.WriteAllLines("Sach.txt", GetUpdatedBooksList(parts[2]));
                    File.WriteAllLines("PhieuMuon.txt", GetUpdatedBorrowingsList(borrowings, soPhieuMuonToReturn));

                    Console.WriteLine("Đã trả sách thành công.");
                    return;
                }
                else if (int.Parse(parts[0]) == soPhieuMuonToReturn && parts[5] == "0")
                {
                    Console.WriteLine("Phiếu mượn đã được trả trước đó.");
                    return;
                }
            }

            Console.WriteLine("Không tìm thấy số phiếu mượn cần trả.");
        }

        public bool IsBookAvailable(string maSach)
        {
            List<string> books = File.ReadAllLines("Sach.txt").ToList();

            foreach (var book in books)
            {
                string[] parts = book.Split(';');

                if (parts[0].Equals(maSach) && parts[8] == "0")
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsReaderExist(string maBanDoc)
        {
            List<string> readers = File.ReadAllLines("BanDoc.txt").ToList();

            foreach (var reader in readers)
            {
                string[] parts = reader.Split(';');

                if (parts[0].Equals(maBanDoc))
                {
                    return true;
                }
            }

            return false;
        }

        public void UpdateBookStatus(string maSach, int soPhieuMuon)
        {
            List<string> books = File.ReadAllLines("Sach.txt").ToList();

            for (int i = 0; i < books.Count; i++)
            {
                string[] parts = books[i].Split(';');

                if (parts[0].Equals(maSach))
                {
                    parts[8] = soPhieuMuon.ToString();
                    books[i] = string.Join(";", parts);
                    break;
                }
            }

            File.WriteAllLines("Sach.txt", books);
        }

        public void UpdateBookAndBorrowingStatus(string maSach, int soPhieuMuon)
        {
            List<string> books = File.ReadAllLines("Sach.txt").ToList();

            for (int i = 0; i < books.Count; i++)
            {
                string[] parts = books[i].Split(';');

                if (parts[0].Equals(maSach))
                {
                    parts[8] = "0";
                    books[i] = string.Join(";", parts);
                    break;
                }
            }

            List<string> borrowings = File.ReadAllLines("PhieuMuon.txt").ToList();

            for (int i = 0; i < borrowings.Count; i++)
            {
                string[] parts = borrowings[i].Split(';');

                if (int.Parse(parts[0]) == soPhieuMuon)
                {
                    parts[5] = "0";
                    borrowings[i] = string.Join(";", parts);
                    break;
                }
            }

            File.WriteAllLines("Sach.txt", books);
            File.WriteAllLines("PhieuMuon.txt", borrowings);
        }

        public List<string> GetUpdatedBooksList(string maSach)
        {
            List<string> books = File.ReadAllLines("Sach.txt").ToList();

            for (int i = 0; i < books.Count; i++)
            {
                string[] parts = books[i].Split(';');

                if (parts[0].Equals(maSach))
                {
                    parts[8] = "0";
                    books[i] = string.Join(";", parts);
                    break;
                }
            }

            return books;
        }

        public List<string> GetUpdatedBorrowingsList(List<string> borrowings, int soPhieuMuon)
        {
            for (int i = 0; i < borrowings.Count; i++)
            {
                string[] parts = borrowings[i].Split(';');

                if (int.Parse(parts[0]) == soPhieuMuon)
                {
                    parts[5] = "0";
                    borrowings[i] = string.Join(";", parts);
                    break;
                }
            }

            return borrowings;
        }


    }
}
