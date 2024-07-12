using MarketProgram.Library.Models;

namespace MarketProgram.UserSide.Helpers
{
    static internal class AllProductsPrice
    {
        public static double AllPrice(List<Product> products)
        {
            double price = 0;

            foreach (var product in products)
            {
                price += product.Price * product.Count;
            }

            return price;
        }
    }
}
