namespace MarketProgram.AdminSide.Models
{
    internal class Admin
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Login { get; set; }
        public string? Pasword { get; set; }

        public Admin() { }
        public Admin(string name, string surname, string login, string pasword) { Name = name; Login = login; Pasword = pasword; Surname = surname; }

        public bool Equal(ref Admin user)
        {
            if (Login == user.Login && Pasword == user.Pasword) { return true; }

            return false;
        }
    }
}
