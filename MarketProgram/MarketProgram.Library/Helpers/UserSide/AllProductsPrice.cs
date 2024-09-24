using MarketProgram.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProgram.Library.Helpers.UserSide
{
    static public class AllProductsPrice
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
