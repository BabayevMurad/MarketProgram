namespace MarketProgram.AdminSide.Models
{
    internal class Prices
    {
        public DateTime DateTime { get; set; }

        public double Price { get; set; }

        public Prices(DateTime dateTime, double price)
        {
            DateTime = dateTime;
            Price = price;
        }

        public override string ToString()
        {
            return $"Date: {DateTime}; Price: {Price}";
        }
    }
}
