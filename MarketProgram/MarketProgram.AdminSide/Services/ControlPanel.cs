using MarketProgram.Library.Models;
using MarketProgram.Library.Helpers.FileWork;
using MarketProgram.AdminSide.Models;
using MarketProgram.Library.Helpers;


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
                        new Product("Çörək", 0.55, "Ağ Çörək.",10,"Un"),
                        new Product("Bulka", 0.6, "Kişmişli.",5, "Un"),
                        new Product("Çörək", 1, "Qara Çörək.",3, "Un"),
                    }),
                    new Category("Süd", new List<Product> {
                        new Product("Süd", 1.2, "15% 1L.",6,"Süd"),
                        new Product("Pendir", 0.9, "İvanovka.",2,"Süd"),
                        new Product("Yağ", 16, "Kərə.",9, "Süd"),
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

            string[] menyu = ["1.Category.", "2.Statistika."];

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
                    Statistika();
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

        void CategoryDelete(int colorChoice)
        {
            Mehsul.Remove(Mehsul[colorChoice]);
            Console.WriteLine("Kateqoriya Silindi.");
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

                Console.WriteLine("Kateqoriya Əlavə etmək Üçün A Basın.");
                Console.WriteLine("Kateqoriya Silmək Üçün Kateqoriya Seçin Və D Basın.");

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
                    case ConsoleKey.A:
                        CategoryAdd();
                        break;
                    case ConsoleKey.D:
                        if (Mehsul.Count == 0)
                        {
                            Console.WriteLine("Boşdur.");
                            Thread.Sleep(1000);
                            return;
                        }
                        else
                        {
                            CategoryDelete(colorChoice);
                            colorChoice = 0;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (Mehsul.Count == 0)
                        {
                            Console.WriteLine("Boşdur.");
                            Thread.Sleep(1000);
                            return;
                        }
                        else
                        {
                            var productsref = Mehsul[colorChoice].Products!;
                            Menyu3(ref productsref, colorChoice);
                        }
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
            }
        }

        void Menyu3(ref List<Product> products, int colorchoiceforcategory)
        {
            int colorChoice = 0;

            ConsoleKeyInfo key;

            while (true)
            {
                Console.Clear();
                Console.ResetColor();

                Console.WriteLine("\t\t\tProducts");

                Console.WriteLine("Çıxmaq Üçün Esc Basın.");
                Console.WriteLine("Produkt Əlavə Üçün A Basın.");

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
                        if (products.Count == 0)
                        {
                            Console.WriteLine("Boşdur.");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            Menyu5(products[colorChoice]);
                            colorChoice = 0;
                        }

                        break;
                    case ConsoleKey.A:
                        ProductAdd(Mehsul[colorchoiceforcategory].Name!);
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

            while (true) { 

                Console.Clear();
                Console.ResetColor();

                Console.WriteLine("\t\t\tProductsMenyu");

                Console.WriteLine($"{product}");

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
                        if(Menyu5Checker(colorChoice, ref product))
                        {
                            return;
                        }
                        colorChoice = 0;
                        break;
                    default:
                        break;
                }
            }
        }

        bool Menyu5Checker(int colorChoice, ref Product product)
        {
            switch (colorChoice)
            {
                case 1:
                    ProductDelete(ref product);
                    return true;
                case 2:
                    ProductEdit(ref product);
                    break;
                default:
                    break;
            }
            return false;
        }
        #endregion

        #region Products

        void ProductAdd(string categoryname)
        {
            ConsoleKeyInfo key;
        Start:
            Console.Clear();
            Console.ResetColor();


            Console.WriteLine("\t\t\tProduct Add.");

            Console.WriteLine("Çıxmaq Üçün Esc Basın.");

            key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
                return;

            Console.Write("Productun Adını Daxil Edin :");

            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            Console.Write("Productun Descriptionu Daxil Edin :");

            string? descriptionu = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(descriptionu))
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            Console.Write("Productun Price Daxil Edin :");

            string? pricestr = Console.ReadLine();

            double price = 0;
            bool parse = double.TryParse(pricestr, out price);
            if (string.IsNullOrWhiteSpace(pricestr) && parse)
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }

            if (price <= 0)
            {
                Console.WriteLine("Produktun priceı 0-dan az ola bilməz.");
                Thread.Sleep(1200);
                goto Start;
            }

            Console.Write("Productun Countu Daxil Edin :");

            string? countstr = Console.ReadLine();
            int count = 0;
            bool parsecount = int.TryParse(countstr, out count);
            if (string.IsNullOrWhiteSpace(countstr) && parsecount)
            {
                Console.WriteLine("Xaiş olunur düzgün daxil edin.");
                Thread.Sleep(1200);
                goto Start;
            }
            if (count <= 0)
            {
                Console.WriteLine("Produktun countu 0-dan az ola bilməz.");
                Thread.Sleep(1200);
                goto Start;
            }

            foreach (var category in Mehsul)
            {
                if (category.Name == categoryname)
                {
                    category.Products!.Add(new Product(name, price, descriptionu, count, categoryname));
                }
            }

            Console.WriteLine("Produkt əlavə olundu.");

            Thread.Sleep(1000);

            return;
        }

        void ProductDelete(ref Product product)
        {
            Console.Clear();
            Console.ResetColor();
            foreach (Category category in Mehsul)
            {
                if (category.Products!.Contains(product))
                {
                    category.Products!.Remove(product);
                }
            }
            Console.WriteLine("Product Silindi.");
            Thread.Sleep(1000);
            return;
        }

        void ProductEdit(ref Product product)
        {
            string[] menyu = ["0.Exit", "1.Name.", "2.Description", "3.Price", "4.Count"];

            int colorChoice = 0;

            ConsoleKeyInfo key;

            while (true)
            {

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
                        colorChoice = 0;
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
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


            Console.WriteLine("\t\t\tProduct Name Edit.");

            Console.WriteLine("Çıxmaq Üçün E Basın.");

            key = Console.ReadKey();

            if (key.Key == ConsoleKey.E)
                return;

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

            Console.WriteLine("Çıxmaq Üçün E Basın.");

            key = Console.ReadKey();

            if (key.Key == ConsoleKey.E)
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

            Console.WriteLine("\t\t\tProduct price Edit.");

            Console.WriteLine("Çıxmaq Üçün E Basın.");

            key = Console.ReadKey();

            if (key.Key == ConsoleKey.E)
                return;

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

            if (price <= 0)
            {
                Console.WriteLine("Produktun priceı 0-dan az ola bilməz.");
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


            Console.WriteLine("\t\t\tProduct price Edit.");

            Console.WriteLine("Çıxmaq Üçün E Basın.");

            key = Console.ReadKey();

            if (key.Key == ConsoleKey.E)
                return;

            Console.Write("Produkt Count Daxil Edin :");

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

            if (price <= 0)
            {
                Console.WriteLine("Produktun countu 0-dan az ola bilməz.");
                Thread.Sleep(1200);
                goto Start;
            }

            product.Count += price;

            Console.WriteLine("Produkt Countu dəyişildi.");

            Thread.Sleep(1000);

            return;
        }
        #endregion

        #region Statistika

        void Statistika()
        {
            int colorChoice = 0;

            ConsoleKeyInfo key;

            string[] menyu = ["1.Statistika Product.", "2. Statistika Qiymət." ,"3.Çeklər."];

            while (true)
            {
                Console.Clear();
                Console.ResetColor();

                Console.WriteLine("\t\t\tStatistika");

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
                        StatistikaChecker(colorChoice);
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
                    ProductBuy();
                    break;
                case 1:
                    BuyPrice();
                    break;
                case 2:
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

            Console.ReadKey();
        }

        #endregion

        #region Statistika

        List<Product> ProductBuyAdd()
        {

            List<Product> products = new();

            Product? findProduct;

            foreach (var buyHistory in BuyHistories)
            {
                foreach (var product in buyHistory.Products!)
                {
                    if (products.Exists(x => x.Equals(product)))
                    {
                        findProduct = ProductFinderClass.ProductFinder(products, product);

                        if (findProduct == null)
                            throw new Exception("Ay Da Statistika.");

                        findProduct!.Count++;

                        findProduct = null;
                    }
                    else
                    {
                        products.Add(product);
                    }
                }
            }

            products.Sort((s1, s2) => s1.Count.CompareTo(s2.Count));

            return products;
        }

        void ProductBuy()
        {
            Console.Clear();
            Console.ResetColor();

            List<Product> products = ProductBuyAdd();

            Console.WriteLine("Products.");

            foreach (var product in products)
            {
                Console.WriteLine(product.ToStringHistory());
            }

            Console.ReadKey();
        }

        List<Prices> BuyPriceAdd()
        {
            List<BuyHistory> buyhistory = new();
            foreach (var buyhistrory in BuyHistories)
            {
                buyhistory.Add(buyhistrory.Copy());
            }

            List<Prices> prices = new List<Prices>();

            DateTime dateTime;

            while (buyhistory.Count > 0)
            {
                dateTime = buyhistory.First().BuyTime;
                Prices prices1 = new(dateTime, 0);

                for (int i = 0; i < buyhistory.Count; i++)
                {
                    if (buyhistory[i].BuyTime == dateTime)
                    {
                        prices1.Price += buyhistory[i].Price;
                        buyhistory.Remove(buyhistory[i]);
                    }
                }
                prices.Add(prices1);
            }

            return prices;
        }

        void BuyPrice()
        {
            Console.Clear();
            Console.ResetColor();

            List<Prices> prices = BuyPriceAdd();

            Console.WriteLine("\t\t\tHesabat.");

            foreach (var price in prices)
            {
                Console.WriteLine(price);
            }

            Console.WriteLine("Çıxmaq Üçün istənilən knopka basın.");

            Console.ReadKey();
        }

        #endregion

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
