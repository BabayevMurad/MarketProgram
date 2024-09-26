using MarketProgram.Library.Models.Dto_s;

namespace MarketProgram.Library.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }
        public string? CategoryName { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<BuyHistoryProduct>? BuyHistories { get; set; } 

        public Product() { }

        public Product(string name, double price, string description, int count, string categoryName)
        {
            Name = name; Price = price; Description = description; Count = count; CategoryName = categoryName;
        }

        public override string ToString()
        {
            return $"Name: {Name}; Description: {Description}; Price: {Price}; Count: {Count}";
        }

        public string ToStringHistory()
        {
            return $"Name: {Name}; Count: {Count}";
        }

        public bool Equal(ref Product product)
        {
            if (Name == product.Name && Price == product.Price && Description == product.Description) { return true; }

            return false;
        }

        public Product Copy()
        {
            Product product = new Product();
            product.Name = Name;
            product.Price = Price;
            product.Description = Description;
            product.Count = Count;
            product.CategoryName = CategoryName;
            return product;
        }

    }
}
