using System.Text.Json.Serialization;
using MarketProgram.Library.Models;

namespace MarketProgram.UserSide.Models
{
    internal class User
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Login { get; set; }
        public string? Pasword { get; set; }

        [JsonIgnore]
        public List<Product>? Basket { get; set; }

        public User() { }
        public User(string name, string surname, string login, string pasword, List<Product> basket) { Name = name; Login = login; Pasword = pasword; Basket = basket; Surname = surname; }

        public override string ToString()
        {
            return $"Name: {Name}\nSurname: {Surname}\nLogin: {Login}";
        }

        public bool Equal(ref User user)
        {
            if (Login == user.Login && Pasword == user.Pasword) { return true; }

            return false;
        }
    }
}
