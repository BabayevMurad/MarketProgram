namespace MarketProgram.Library.Models
{
    public class Category
    {
        public int Id { get; set; }
        public List<Product>? Products { get; set; }
        public string? Name { get; set; }

        Category() { }

        public Category(string name, List<Product> products) { Products = products; Name = name; }

        public override string ToString()
        {
            return $"Name: {Name}";
        }
    }
}
