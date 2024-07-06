using MarketProgram.Library.Models;

namespace MarketProgram.Library.Helpers
{
    public static class ProductFinderClass
    {
        public static Product? ProductFinder(List<Category> categories, Product product_intput)
        {
            foreach (var category in categories)
            {
                foreach (var product in category.Products!)
                {
                    if (product.Equal(ref product_intput))
                        return product;
                }
            }
            return null;
        }

        public static Product? ProductFinder(List<Product> products, Product product_intput)
        {
            foreach (var product in products)
            {
                if (product.Equal(ref product_intput))
                    return product;
            }
            return null;
        }
    }
}
