using System;
using System.Collections.Generic;
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
    }
}
