namespace MarketProgram.Library.Models
{
    public class Product
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }

        public Product() { }

        public Product(string name, double price, string description, int count) { Name = name; Price = price; Description = description; Count = count; }

        public override string ToString()
        {
            return $"Name: {Name}; Description: {Description}; Price: {Price}; Count: {Count}";
        }

        public bool Equal(ref Product product)
        {
            if (Name == product.Name && Price == product.Price && Description == product.Description && Count == product.Count) { return true; }

            return false;
        }
    }
}
