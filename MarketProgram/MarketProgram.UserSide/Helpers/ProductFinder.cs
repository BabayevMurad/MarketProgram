using MarketProgram.Library.Models;

namespace MarketProgram.UserSide.Helpers
{
    internal class ProductFinderClass
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
    }
}
