using MarketProgram.Library.Data;
using MarketProgram.Library.Models.Dto_s;
using Microsoft.EntityFrameworkCore;

namespace MarketProgram.Library.Models
{
    public class BuyHistory
    {
        public MarketAppContext MarketAppContext { get; set; } = new MarketAppContext();

        public int Id { get; set; }
        public string? UserLogin { get; set; }
        public DateTime BuyTime { get; set; }
        public double Price { get; set; }

        public List<BuyHistoryProduct> ByProducts { get; set; }

        public List<Product> Products { get; set; }

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        
        public BuyHistory() { }
        
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        
        public BuyHistory(string userName, DateTime buyTime, List<Product> products, double price)
        
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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
