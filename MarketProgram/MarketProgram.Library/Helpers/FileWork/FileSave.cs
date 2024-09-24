using MarketProgram.Library.Data;
using MarketProgram.Library.Models;


namespace MarketProgram.Library.Helpers.FileWork
{
    public static class FileSaveClass
    {
        public static void FileSaveUser(MarketAppContext marketAppContext, List<User> Data)
        {

            for (int i = 0; i < Data.Count; i++) 
            {
                marketAppContext.User.Update(Data[i]);
            }

            marketAppContext.SaveChanges();
        }

        public static void FileSaveProduct(MarketAppContext marketAppContext, List<Category> Data)
        {
            for (int i = 0; i < Data.Count; i++)
            {
                marketAppContext.Category.Update(Data[i]);
            }

            marketAppContext.SaveChanges();
        }

        public static void FileSaveBuyHistory(MarketAppContext marketAppContext, List<BuyHistory> Data)
        {
            for (int i = 0; i < Data.Count; i++)
            {
                marketAppContext.BuyHistory.Update(Data[i]);
            }

            marketAppContext.SaveChanges();
        }

        public static void FileSaveAdmin(MarketAppContext marketAppContext, List<Admin> Data)
        {
            for (int i = 0; i < Data.Count; i++)
            {
                marketAppContext.Admin.Update(Data[i]);
            }

            marketAppContext.SaveChanges();
        }
    }
}
