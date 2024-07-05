namespace MarketProgram.Library.Models
{
    public class BuyHistory
    {
        public string? UserName { get; set; }
        public DateTime? BuyTime { get; set; }
        public List<Product>? Products { get; set; }
        public double? Price { get; set; }

        public BuyHistory() { }

        public BuyHistory(string userName, DateTime buyTime, List<Product> products, double price)
        {
            UserName = userName;
            BuyTime = buyTime;
            Products = products;
            Price = price;
        }
    }
}
