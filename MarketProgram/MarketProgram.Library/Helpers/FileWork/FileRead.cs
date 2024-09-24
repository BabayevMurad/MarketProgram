using MarketProgram.Library.Models;
using MarketProgram.Library.Data;
using Microsoft.EntityFrameworkCore;


namespace MarketProgram.Library.Helpers.FileWork
{
    public static class FileReadClass
    {
        public static List<User> FileReadUser(MarketAppContext marketAppContext)
        {
            return marketAppContext.User.Include(u => u.Basket).ToList();
        }

        public static List<Category> FileReadProduct(MarketAppContext marketAppContext)
        {
            return marketAppContext.Category.Include(c => c.Products).ToList();
        }

        public static List<BuyHistory> FileReadBuyHistory(MarketAppContext marketAppContext)
        {
            return marketAppContext.BuyHistory.Include(b => b.Products).ToList();
        }

        public static List<Admin> FileReadAdmin(MarketAppContext marketAppContext)
        {
            return marketAppContext.Admin.ToList();
        }
    }
}
