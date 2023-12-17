using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien
{
    struct Accounts
    {
        private string userName;
        private string pass;

        public string UserName { get => userName; set => userName = value; }
        public string Pass { get => pass; set => pass = value; }
    }
}
