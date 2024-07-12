namespace MarketProgram.Library.Models
{
    public class BuyHistory
    {
        public string? UserLogin { get; set; }
        public DateTime BuyTime { get; set; }
        public List<Product>? Products { get; set; }
        public double Price { get; set; }

        public BuyHistory() { }

        public BuyHistory(string userName, DateTime buyTime, List<Product> products, double price)
        {
            UserLogin = userName;
            BuyTime = buyTime;
            Products = products;
            Price = price;
        }

        public BuyHistory Copy()
        {
            return new BuyHistory(UserLogin!, BuyTime, Products!, Price);
        }

        public void ConsoleWrite()
        {
            Console.WriteLine("**************************************");
            Console.WriteLine($"Name: {UserLogin}");
            Console.WriteLine($"BuyTime: {BuyTime}");
            Console.WriteLine($"AllPrice: {Price}");
            Console.WriteLine("Products: ");
            foreach (Product product in Products!)
            {
                Console.WriteLine($"\t{product}");
            }
            Console.WriteLine("**************************************");
        }
    }
}
