namespace MarketProgram.Library.Models.Dto_s
{
    public class BuyHistoryProduct
    {
        public int BuyHistoryId { get; set; }
        public BuyHistory BuyHistory { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
