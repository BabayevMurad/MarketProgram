using MarketProgram.Library.Helpers.FileWork;
using MarketProgram.Library.Helpers;
using MarketProgram.Library.Helpers.UserSide;
using MarketProgram.Library.Models;
using MarketProgram.Library.Data;

namespace MarketProgram.Library.Services
{
    public class ControlPanelUser
    {
        public int Id { get; set; }
        List<User>? Users { get; set; }

        User? User { get; set; }

        List<Category> Mehsul { get; set; }

        List<BuyHistory>? BuyHistories { get; set; }

        MarketAppContext? MarketAppContext { get; set; } = new MarketAppContext();

        public ControlPanelUser()
        {
            var UsersTest = FileReadClass.FileReadUser(MarketAppContext!);

            if (UsersTest is null)
                Users = new List<User>();
            else
                Users = UsersTest;

            UsersTest = null;

            var ProductsTest = FileReadClass.FileReadProduct(MarketAppContext!);

            if (ProductsTest is null)
            {
                Mehsul = new List<Category> {
                    new Category("Un", new List<Product> {
                        new Product("Çörək", 0.55, "Ağ Çörək.",10,"Un"),
                        new Product("Bulka", 0.6, "Kişmişli.",5,"Un"),
                        new Product("Çörək", 1, "Qara Çörək.",3,"Un"),
                    }),
                    new Category("Süd", new List<Product> {
                        new Product("Süd", 1.2, "15% 1L.",6,"Süd"),
                        new Product("Pendir", 0.9, "İvanovka.",2,"Süd"),
                        new Product("Yağ", 16, "Kərə.",9,"Süd"),
                    }),
                };
            }
            else
                Mehsul = ProductsTest;

            var BuyHistoryTest = FileReadClass.FileReadBuyHistory(MarketAppContext!);

            if (BuyHistoryTest is null)
                BuyHistories = new List<BuyHistory>();
            else
                BuyHistories = BuyHistoryTest;
        }

        public void Menyu1()
        {
            int colorChoice = 0;
            ConsoleKeyInfo key;
            string[] menyu = { "Exit.", "Login.", "Register." };

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
                                FileSaveClass.FileSaveUser(MarketAppContext!, Users!);
                                FileSaveClass.FileSaveProduct(MarketAppContext! , Mehsul);
                                FileSaveClass.FileSaveBuyHistory(MarketAppContext!, BuyHistories!);
                                System.Environment.Exit(0);
                                break;
                            case 1:
                                if (Login()) { return; }
                                break;
                            case 2:
                                Register();
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
                User = null;
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

            var user1 = new User() { Login = login.ToLower(), Pasword = password };

            User? user2 = null;

            foreach (var item in Users!)
            {
                if (item.Equal(ref user1))
                {
                    user2 = item;
                }
            }

            if (user2 is null)
            {
                Console.WriteLine("Belə İstifadəçi Yoxdu.");
                Thread.Sleep(1100);
            }
            else
            {
                Console.WriteLine("Uğurla geydiyatdan keçdiniz.");
                Thread.Sleep(1100);
                User = user2;
                User.Basket = new();
                return true;
            }

            return false;
        }

        void Register()
        {
        Start:

            Console.Clear();
            Console.ResetColor();


            Console.WriteLine("\t\t\tRegister");

            Console.WriteLine("Əvvələ Qayıtmaq Üçün ESC basın.");

            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }

            Console.Write("Name: ");
            var name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            Console.Write("Surname: ");
            var surname = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(surname))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
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

            var useradd = new User(name, surname, login.ToLower(), password, new List<Product> { });

            Users!.Add(useradd);

            Console.WriteLine("Uğurla qeydiyatdan keçdiniz.");

            Thread.Sleep(1100);
        }

        void Menyu2()
        {
            int colorChoice = 0;

            List<Product> products;

            ConsoleKeyInfo key;

            while (true)
            {
                Console.Clear();
                Console.ResetColor();

                Console.WriteLine("\t\t\tCategories");

                Console.WriteLine("Çıxmaq Üçün Esc Basın.");
                Console.WriteLine("Səbətə Keçmək Üçün Space Basın.");
                Console.WriteLine("Useri Profile Girmek Etmək Üçün D Basın.");

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
                        if (Mehsul.Count == 0)
                        {
                            Console.WriteLine("Bosdur");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            products = Mehsul[colorChoice].Products!;
                            Menyu3(ref products);
                            colorChoice = 0;
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        Menyu4();
                        break;
                    case ConsoleKey.D:
                        UserRedactorMenyu();
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
            }
        }

        void Menyu3(ref List<Product> product)
        {
            int colorChoice = 0;

            ConsoleKeyInfo key;


            while (true)
            {
                Product productAdd = product[colorChoice];
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
                        if (productAdd.Count == 0)
                        {
                            Console.WriteLine("Bosdur");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            ProductAddBasket(ref productAdd);
                            colorChoice = 0;
                        }
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
            }
        }

        void ProductAddBasket(ref Product product)
        {

            Product? productlist = ProductFinderClass.ProductFinder(Mehsul, product);

            if (productlist is null)
            {
                Console.WriteLine("Yoxdu Product");
            }

            if (productlist!.Count > 1)
                productlist.Count--;
            else
            {
                foreach (var category in Mehsul)
                {
                    category.Products!.Remove(productlist);
                }
            }

            bool productexsit = false;

            foreach (var product1 in User!.Basket!)
            {
                if (product1.Equal(ref productlist))
                {
                    productexsit = true;
                }
            }

            if (productexsit)
            {
                ProductFinderClass.ProductFinder(User!.Basket, productlist)!.Count++;
            }
            else
            {
                Product product1 = new Product(productlist.Name!, productlist.Price, productlist.Description!, 1, productlist.CategoryName!);
                User!.Basket!.Add(product1);
            }

            Console.WriteLine("Produkt Əlavə Olundu.");

            Thread.Sleep(1100);
        }

        void Menyu4()
        {
            if (User!.Basket is null)
            {
                User!.Basket = new List<Product>();
            }
            List<Product> products = User!.Basket!;
            if (products.Count == 0)
            {
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("Sebet boşdur.");
                Thread.Sleep(1100);
                return;
            }

            int colorChoice = 0;
            ConsoleKeyInfo key;
            while (true)
            {
                if (products.Count == 0)
                {
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("Sebet boşdur.");
                    Thread.Sleep(1100);
                    return;
                }
                double allPrice = AllProductsPrice.AllPrice(products);
                Console.Clear();
                Console.ResetColor();

                Console.WriteLine("\t\t\tSebet");

                Console.WriteLine("Çıxmaq Üçün Esc Basın.");
                Console.WriteLine("Ödəniş Etmək Üçün Space Basın.");
                Console.WriteLine($"Qiymət: {allPrice}");

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
                        var productchoice = products[colorChoice];
                        SebetDelete(ref productchoice);
                        break;
                    case ConsoleKey.Spacebar:
                        SebetbBuy(allPrice);
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
            }
        }

        void SebetDelete(ref Product product)
        {
            Product? product1 = ProductFinderClass.ProductFinder(Mehsul, product)!;
            if (!(product.Count > 1))
            {
                if (product1 is null)
                {
                    for (int i = 0; i < Mehsul.Count; i++)
                    {
                        if (Mehsul[i].Name == product.CategoryName)
                        {
                            Product product2 = new Product(product.Name!, product.Price, product.Description!, 1, product.CategoryName!);
                            Mehsul[i].Products!.Add(product2);
                        }
                    }
                }
                else
                {
                    product1!.Count++;
                }
                User!.Basket!.Remove(product);
            }
            else
            {
                if (product1 is null)
                {
                    for (int i = 0; i < Mehsul.Count; i++)
                    {
                        if (Mehsul[i].Name == product.CategoryName)
                        {
                            Product product2 = new Product(product.Name!, product.Price, product.Description!, 1, product.CategoryName!);
                            Mehsul[i].Products!.Add(product2);
                        }
                    }
                }
                else
                {
                    product1!.Count++;
                }
                product.Count--;
            }
            Console.WriteLine("Element Silindi.");
            Thread.Sleep(1000);
        }

        void SebetbBuy(double price)
        {
        Start:

            Console.Clear();
            Console.ResetColor();

            Console.WriteLine($"Sizin {price} ödəməlisiniz.");

            Console.Write("Məbləğ: ");
            var price_str = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(price_str))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            if (!(double.TryParse(price_str, out double price1)))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            if (price1 < price)
            {
                Console.WriteLine("Daxil Etdiyiniz Məbləğ Azdır.");
                Thread.Sleep(1200);
                goto Start;
            }

            if (price1 > price)
            {
                Console.WriteLine($"Qalığ {price1 - price}");
                List<Product> productelave = new List<Product>();
                foreach (Product product in User!.Basket!)
                {
                    productelave.Add(product.Copy());
                }

                string userlogin = "";
                foreach (var item in User!.Login!)
                {
                    userlogin += item;
                }
                BuyHistory history = new BuyHistory(userlogin, DateTime.Now, productelave, price);
                BuyHistories!.Add(history);
                Thread.Sleep(1000);
                User!.Basket!.Clear();
                Menyu2();
            }
            else if (price1 == price)
            {
                Console.WriteLine("Ödənildi.");
                List<Product> productelave = new List<Product>();
                foreach (Product product in User!.Basket!)
                {
                    productelave.Add(product.Copy());
                }

                string userlogin = "";
                foreach (var item in User!.Login!)
                {
                    userlogin += item;
                }
                BuyHistory history = new BuyHistory(userlogin, DateTime.Now, productelave, price);
                BuyHistories!.Add(history);
                Thread.Sleep(1000);
                User!.Basket!.Clear();
                Menyu2();
            }
        }

        void ProductCountDecrement(List<Product> products)
        {
            foreach (Product product in products)
            {
                if (product.Count > 1)
                    product.Count--;
                else
                {
                    foreach (Category category in Mehsul)
                    {
                        if (category.Products!.Contains(product))
                        {
                            category.Products!.Remove(product);
                        }
                    }
                }

            }
        }

        void UserRedactorMenyu()
        {
            string[] menyu = ["1.Name", "2.Surname", "3.Password", "4.History"];

            int colorChoice = 0;

            ConsoleKeyInfo key;

            while (true)
            {
                Console.Clear();
                Console.ResetColor();

                Console.WriteLine("\t\tUser Profil:");


                Console.WriteLine("Çıxmaq Üçün Esc Basın.");

                Console.WriteLine(User);

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
                        UserRedactorChecker(colorChoice);
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
            }
        }

        void UserBuyHistory()
        {
            bool exsits = false;
            foreach (var history in BuyHistories!)
            {
                if (history.UserLogin == User!.Login)
                {
                    exsits = true;
                }
            }
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("User Buy History.");

            if (!exsits)
            {
                Console.WriteLine("Burda Məlumat Yoxdu.");
                Thread.Sleep(1000);
                return;
            }

            foreach (BuyHistory history in BuyHistories!)
            {
                if (history.UserLogin == User!.Login)
                {
                    history.ConsoleWrite();
                }
            }
            Console.ReadKey();
        }

        void UserRedactorChecker(int Choice)
        {
            switch (Choice)
            {
                case 0:
                    UserNameChange();
                    break;
                case 1:
                    UserSurnameChange();
                    break;
                case 2:
                    UserPasswordChange();
                    break;
                case 3:
                    UserBuyHistory();
                    break;
                default:
                    break;
            }
        }

        void UserNameChange()
        {
        Start:
            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("Əvvələ Qayıtmaq Üçün Esc Basın.");
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                return;
            }

            Console.Write("Pleace Insert Name:");
            var name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            User!.Name = name;
        }

        void UserSurnameChange()
        {
        Start:
            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("Əvvələ Qayıtmaq Üçün Esc Basın.");
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                return;
            }

            Console.Write("Pleace Insert Surname:");
            var surname = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(surname))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            User!.Surname = surname;
        }

        void UserPasswordChange()
        {
        Start:
            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("Əvvələ Qayıtmaq Üçün Esc Basın.");
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                return;
            }

            Console.Write("Pleace Insert Old Password:");
            var password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            if (User!.Pasword != password)
            {
                Console.WriteLine("Şifrə Düzgün Deyil.");
                Thread.Sleep(1200);
                goto Start;
            }

            Console.Write("Pleace Insert New Password:");
            var passwordchange = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(passwordchange))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            User!.Pasword = passwordchange;

            Console.WriteLine("Şifrə Uğurla Dəyişildi.");

            Thread.Sleep(1000);
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

