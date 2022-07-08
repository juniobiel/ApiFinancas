namespace Business.Models
{
    public class StockPurchase : Entity
    {
        public Guid StockPurchaseId { get; set; }
        public int StockId { get; set; }
        public string StockTicker { get; set; }
        public decimal StockPrice { get; set; }
        public int StockQt { get; set; }
        public decimal PurchaseTaxes { get; set; }
        public DateTime PurchaseDate { get; set; }

        public Guid UserId_Created { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UserId_Updated { get; set; }
        public DateTime? UpdatedAt { get; set; }


        //EF relation
        public Stock Stock { get; set; }

    }
}
