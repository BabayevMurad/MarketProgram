namespace MarketProgram.Library.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Login { get; set; }
        public string? Pasword { get; set; }

        //I dont add relationship witdh products because this basket don't drafted.
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
