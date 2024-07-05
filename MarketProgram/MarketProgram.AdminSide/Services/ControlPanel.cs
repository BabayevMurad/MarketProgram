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

        List<BuyHistory> BuyHistories { get; set; }

        public ControlPanel()
        {
            var AdminsTest = FileReadClass.FileRead<List<Admin>>("AdminFilePath");

            if (AdminsTest is null)
                Admins = new List<Admin> { new Admin("Murad", "Babayev", "babay_aq38", "0503167673") };
            else
                Admins = AdminsTest;

            AdminsTest = null;

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

            var BuyHistoryTest = FileReadClass.FileRead<List<BuyHistory>>("BuyHistoryFilePath");

            if (BuyHistoryTest is null)
                BuyHistories = new List<BuyHistory>();
            else
                BuyHistories = BuyHistoryTest;
        }

        public void Menyu1()
        {
            int colorChoice = 0;
            ConsoleKeyInfo key;
            string[] menyu = { "Exit.", "Login." };

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
                                FileSaveClass.FileSave(Admins, "AdminFilePath");
                                FileSaveClass.FileSave(Mehsul, "ProductsFilePath");
                                FileSaveClass.FileSave(BuyHistories, "BuyHistoryFilePath");
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

        void Menyu4()
        {
            int colorChoice = 0;

            ConsoleKeyInfo key;

            string[] menyu = ["1.Category.", "2.Add Category.", "3.Delete Category.", "4.Category Name Change.", "5.Statistika."]; 

            while (true)
            {
                Console.Clear();
                Console.ResetColor();

                Console.WriteLine("\t\t\tMenyu");

                Console.WriteLine("Çıxmaq Üçün Esc Basın.");

                for (int i = 0; i < menyu.Length; i++)
                {
                    if (i == colorChoice)
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
                        Menyu4Checker(colorChoice);
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
            }
        }

        void Menyu4Checker(int colorChoice)
        {
            switch (colorChoice)
            {
                case 0:
                    Menyu2();
                    break;
                case 1:
                    CategoryAdd();
                    break;
                case 2:
                    CategoryDelete();
                    break;
                case 3:
                    CategoryEdit();
                    break;
                default:
                    break;
            }
        }

        #region Category

        void CategoryAdd()
        {
        Start:

            ConsoleKeyInfo key;

            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("Çıxmaq Üçün Esc Basın.");

            key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
                return;

            Console.WriteLine("\t\t\tCategory Add.");

            Console.Write("Kategoriya Adını Daxil Edin :");

            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            Mehsul.Add(new Category(name, new List<Product>()));

            Console.WriteLine("Kateqoriya əlavə olunu.");

            Thread.Sleep(1000);

            return;
        }

        void CategoryDelete()
        {
            int colorChoice = 0;

            ConsoleKeyInfo key;

            while (true)
            {
                Console.Clear();
                Console.ResetColor();

                Console.WriteLine("\t\t\tCategory Delete");

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
                        Mehsul.Remove(Mehsul[colorChoice]);
                        Console.WriteLine("Kateqoriya Silindi.");
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
            }
        } 

        void CategoryEdit()
        {
            int colorChoice = 0;

            ConsoleKeyInfo key;

            while (true)
            {
                Console.Clear();
                Console.ResetColor();

                Console.WriteLine("\t\t\tCategory Edit");

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
                        CategoryNameChange(Mehsul[colorChoice]);
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
            }
        }

        void CategoryNameChange(Category category)
        {
        Start:

            ConsoleKeyInfo key;

            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("Çıxmaq Üçün Esc Basın.");

            key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
                return;

            Console.WriteLine("\t\t\tCategory Name Edit.");

            Console.Write("Kategoriya Adını Daxil Edin :");

            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            category.Name = name;

            Console.WriteLine("Kateqoriya adı dəyişildi.");

            Thread.Sleep(1000);

            return;
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
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
            }
        }

        void Menyu3(List<Product> products)
        {
            int colorChoice = 0;

            ConsoleKeyInfo key;

            while (true)
            {
                Console.Clear();
                Console.ResetColor();

                Console.WriteLine("\t\t\tProducts");

                Console.WriteLine("Çıxmaq Üçün Esc Basın.");
                Console.WriteLine("Produkt Əlavə Etmək Üçün A Basın.");

                for (int i = 0; i < products.Count; i++)
                {
                    if (i == colorChoice)
                        Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(products[i]);

                    Console.ResetColor();
                }

                key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.W:
                        if (colorChoice > 0)
                            colorChoice--;
                        else colorChoice = products.Count - 1;
                        break;
                    case ConsoleKey.S:
                        if (colorChoice < products.Count - 1)
                            colorChoice++;
                        else colorChoice = 0;
                        break;
                    case ConsoleKey.Enter:
                        Menyu5(products[colorChoice]);
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
            }
        }

        void Menyu5(Product product)
        {
            string[] menyu = ["0.Exit", "1.Delete.", "2.Edit"];

            int colorChoice = 0; 
            
            ConsoleKeyInfo key;

            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("\t\t\tProductsMenyu");

            Console.WriteLine($"{product}");

            Console.WriteLine("Çıxmaq Üçün Esc Basın.");

            for (int i = 0; i < menyu.Length; i++)
            {
                if (i == colorChoice)
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
                    else colorChoice = product.Count - 1;
                    break;
                case ConsoleKey.S:
                    if (colorChoice < product.Count - 1)
                        colorChoice++;
                    else colorChoice = 0;
                    break;
                case ConsoleKey.Enter:
                    if (colorChoice == 0)
                        return;
                    Menyu5Checker(colorChoice, ref product);
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    break;
            }
        }

        void Menyu5Checker(int colorChoice, ref Product product)
        {
            switch (colorChoice)
            {
                case 1:
                    ProductDelete(ref product);
                    break;
                case 2:
                    ProductEdit(ref product);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Products

        void ProductDelete(ref Product product)
        {
            foreach (Category category in Mehsul)
            {
                if (category.Products!.Contains(product))
                {
                    category.Products!.Remove(product);
                }
            }
        }

        void ProductEdit(ref Product product)
        {
            string[] menyu = ["0.Exit", "1.Name.", "2.Description", "3.Price", "4.Count"];

            int colorChoice = 0;

            ConsoleKeyInfo key;

            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("\t\t\tProductsEdit");

            Console.WriteLine("Çıxmaq Üçün Esc Basın.");

            for (int i = 0; i < menyu.Length; i++)
            {
                if (i == colorChoice)
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
                    if (colorChoice == 0)
                        return;
                    ProductEditChecker(colorChoice, ref product);
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    break;
            }
        }

        void ProductEditChecker(int colorChoice, ref Product product) 
        {
            switch (colorChoice)
            {
                case 1:
                    ProductNameChange(ref product);
                    break;
                case 2:
                    ProductDescriptionChange(ref product);
                    break;
                case 3:
                    ProductPriceChange(ref product);
                    break;
                case 4:
                    ProductCountChange(ref product);
                    break;
                default:
                    break;
            }
        }

        void ProductNameChange(ref Product product)
        {
        Start:

            ConsoleKeyInfo key;

            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("Çıxmaq Üçün Esc Basın.");

            key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
                return;

            Console.WriteLine("\t\t\tProduct Name Edit.");

            Console.Write("Productun Adını Daxil Edin :");

            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            product.Name = name;

            Console.WriteLine("Produkt adı dəyişildi.");

            Thread.Sleep(1000);

            return;
        }

        void ProductDescriptionChange(ref Product product)
        {
        Start:

            ConsoleKeyInfo key;

            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("Çıxmaq Üçün Esc Basın.");

            key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
                return;

            Console.WriteLine("\t\t\tProduct Description Edit.");

            Console.Write("Produktun Descriptionu Daxil Edin :");

            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

           product.Description = name;

            Console.WriteLine("Produkt Descriptionnu dəyişildi.");

            Thread.Sleep(1000);

            return;
        }

        void ProductPriceChange(ref Product product)
        {
        Start:

            double price;

            ConsoleKeyInfo key;

            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("Çıxmaq Üçün Esc Basın.");

            key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
                return;

            Console.WriteLine("\t\t\tProduct price Edit.");

            Console.Write("Produkt Qiymetini Daxil Edin :");

            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            if (!double.TryParse(name, out price))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            product.Price = price;

            Console.WriteLine("Produkt Qiymeti dəyişildi.");

            Thread.Sleep(1000);

            return;
        }

        void ProductCountChange(ref Product product)
        {
        Start:

            int price;

            ConsoleKeyInfo key;

            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("Çıxmaq Üçün Esc Basın.");

            key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
                return;

            Console.WriteLine("\t\t\tProduct price Edit.");

            Console.Write("Produkt Qiymetini Daxil Edin :");

            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            if (!int.TryParse(name, out price))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            product.Count += price;

            Console.WriteLine("Produkt Qiymeti dəyişildi.");

            Thread.Sleep(1000);

            return;
        }
        #endregion

        void Statistika(ref List<BuyHistory> buyHistories)
        {
            int colorChoice = 0;

            ConsoleKeyInfo key;

            string[] menyu = ["1.Statistika.", "2.Çeklər."];

            while (true)
            {
                Console.Clear();
                Console.ResetColor();

                Console.WriteLine("\t\t\tMenyu");

                Console.WriteLine("Çıxmaq Üçün Esc Basın.");

                for (int i = 0; i < menyu.Length; i++)
                {
                    if (i == colorChoice)
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
                        
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
            }
        }

        void StatistikaChecker(int colorChoice)
        {
            switch (colorChoice)
            {
                case 0:
                    
                    break;
                case 1:
                    Bills();
                    break;
                default:
                    break;
            }
        }

        void Bills()
        {
            foreach (var buyHistory in BuyHistories)
            {
                buyHistory.ConsoleWrite();
            }
        }

        public void Start()
        {
            while (true)
            {
                Menyu1();
                Menyu4();
            }
        }
    }
}
