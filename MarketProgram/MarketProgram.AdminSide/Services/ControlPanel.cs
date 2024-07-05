using MarketProgram.Library.Models;
using MarketProgram.Library.Helpers.FileWork;
using MarketProgram.AdminSide.Models;

namespace MarketProgram.AdminSide.Services
{
    internal class ControlPanel
    {
    List<Admin>? Admins { get; set; }

    Admin? Admin { get; set; }

    List<Category> Mehsul { get; set; }

    public ControlPanel()
    {
        var UsersTest = FileReadClass.FileRead<List<Admin>>("UserFilePath");

        if (UsersTest is null)
            Admins = new List<Admin>();
        else
            Admins = UsersTest;

        UsersTest = null;

        var ProductsTest = FileReadClass.FileRead<List<Category>>("ProductsFilePath");

        if (ProductsTest is null)
        {
            Mehsul = new List<Category> {
                    new Category("Un", new List<Product> {
                        new Product("Çörək", 0.55, "Ağ Çörək.",10),
                        new Product("Bulka", 0.6, "Kişmişli.",5),
                        new Product("Çörək", 1, "Qara Çörək.",3),
                    }),
                    new Category("Süd", new List<Product> {
                        new Product("Süd", 1.2, "15% 1L.",6),
                        new Product("Pendir", 0.9, "İvanovka.",2),
                        new Product("Yağ", 16, "Kərə.",9),
                    }),
                };
        }
        else
            Mehsul = ProductsTest;

        UsersTest = null;

        if (UsersTest is null)
            Admins = new List<Admin>();
        else
            Admins = UsersTest;

        UsersTest = null;
    }

    public void Menyu1()
    {
        int colorChoice = 0;
        ConsoleKeyInfo key;
        string[] menyu = { "Exit.", "Login."};

        while (true)
        {
            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("\t\t\tMenyu");

            for (int i = 0; i < menyu.Length; i++)
            {

                if (colorChoice == i)
                    Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine(menyu[i]);

                Console.ResetColor();
            }

            key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.W:
                    if (colorChoice > 0)
                        colorChoice--;
                    else colorChoice = menyu.Length - 1;
                    break;
                case ConsoleKey.S:
                    if (colorChoice < menyu.Length - 1)
                        colorChoice++;
                    else colorChoice = 0;
                    break;
                case ConsoleKey.Enter:
                    switch (colorChoice)
                    {
                        case 0:
                            FileSaveClass.FileSave(Admin, "AdminFilePath");
                            FileSaveClass.FileSave(Mehsul, "ProductsFilePath");
                            System.Environment.Exit(0);
                            break;
                        case 1:
                            if (Login()) { return; }
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    bool Login()
    {
    Start:

        Console.Clear();
        Console.ResetColor();

        Console.WriteLine("\t\t\tLogin");

        Console.WriteLine("Əvvələ Qayıtmaq Üçün ESC basın.");

        var key = Console.ReadKey();

        if (key.Key == ConsoleKey.Escape)
        {
            Admin = null;
            return false;
        }

        Console.Write("Login: ");
        var login = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(login))
        {
            Console.WriteLine("Xaiş olunur düzgün daxil edin.");
            Thread.Sleep(1200);
            goto Start;
        }

        Console.Write("Password: ");
        var password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Xaiş olunur düzgün daxil edin.");
            Thread.Sleep(1200);
            goto Start;
        }

        var admin1 = new Admin() { Login = login.ToLower(), Pasword = password };

        Admin? admin2 = null;

        foreach (var item in Admins!)
        {
            if (item.Equal(ref admin1))
            {
                admin2 = item;
            }
        }

        if (admin2 is null)
        {
            Console.WriteLine("Belə İstifadəçi Yoxdu.");
            Thread.Sleep(1100);
        }
        else
        {
            Console.WriteLine("Uğurla geydiyatdan keçdiniz.");
            Thread.Sleep(1100);
            Admin = admin2;
            return true;
        }

        return false;
    }

    void Menyu2()
    {
        int colorChoice = 0;

        ConsoleKeyInfo key;

        while (true)
        {
            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("\t\t\tCategories");

            Console.WriteLine("Çıxmaq Üçün Esc Basın.");

            for (int i = 0; i < Mehsul.Count; i++)
            {
                if (i == colorChoice)
                    Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Mehsul[i]);

                Console.ResetColor();
            }

            key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.W:
                    if (colorChoice > 0)
                        colorChoice--;
                    else colorChoice = Mehsul.Count - 1;
                    break;
                case ConsoleKey.S:
                    if (colorChoice < Mehsul.Count - 1)
                        colorChoice++;
                    else colorChoice = 0;
                    break;
                case ConsoleKey.Enter:
                    Menyu3(Mehsul[colorChoice].Products!);
                    break;
                default:
                    break;
            }
        }
    }

    void Menyu3(List<Product> product)
    {
        int colorChoice = 0;

        ConsoleKeyInfo key;

        while (true)
        {
            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("\t\t\tProducts");

            Console.WriteLine("Çıxmaq Üçün Esc Basın.");

            for (int i = 0; i < product.Count; i++)
            {
                if (i == colorChoice)
                    Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(product[i]);

                Console.ResetColor();
            }

            key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.W:
                    if (colorChoice > 0)
                        colorChoice--;
                    else colorChoice = product.Count - 1;
                    break;
                case ConsoleKey.S:
                    if (colorChoice < product.Count - 1)
                        colorChoice++;
                    else colorChoice = 0;
                    break;
                case ConsoleKey.Enter:
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    break;
            }
        }
    }

    public void Start()
    {
        while (true)
        {
            Menyu1();
            Menyu2();
        }
    }
    }
}
