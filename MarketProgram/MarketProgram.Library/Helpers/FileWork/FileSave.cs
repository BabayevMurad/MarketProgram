using MarketProgram.Library.Data;
using MarketProgram.Library.Models;
using MarketProgram.Library.Models.Dto_s;


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

            for (int i = 0; i < Data.Count; i++)
            {

                for (int j = 0; j < Data[i].Products.Count; j++)
                {

                    var buyhistory = new BuyHistoryProduct()
                    {
                        BuyHistory = Data[i],

                        Product = Data[i].Products[j]
                    };

                    marketAppContext.BuyHistoryProduct.Update(buyhistory);
                };

            };

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
