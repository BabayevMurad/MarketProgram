using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProgram.Library.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Login { get; set; }
        public string? Pasword { get; set; }

        public Admin() { }
        public Admin(string name, string surname, string login, string pasword) { Name = name; Login = login; Pasword = pasword; Surname = surname; }

        //public Admin(int ıd, string? name, string? surname, string? login, string? pasword, int buyHistoryId, BuyHistory buyHistory)
        //{
        //    Id = ıd;
        //    Name = name;
        //    Surname = surname;
        //    Login = login;
        //    Pasword = pasword;
        //    BuyHistoryId = buyHistoryId;
        //    BuyHistory = buyHistory;
        //}

        public bool Equal(ref Admin user)
        {
            if (Login == user.Login && Pasword == user.Pasword) { return true; }

            return false;
        }
    }
}
