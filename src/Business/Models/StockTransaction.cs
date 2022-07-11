namespace Business.Models
{
    public class StockTransaction : Entity
    {
        public Guid StockTransactionId { get; set; }
        public int StockId { get; set; }
        public string StockTicker { get; set; }
        public decimal StockPrice { get; set; }
        public int StockQt { get; set; }
        public decimal TransactionTaxes { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Total { get; set; }


        public Guid UserId_Created { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UserId_Updated { get; set; }
        public DateTime? UpdatedAt { get; set; }


        //EF relation
        public Stock Stock { get; set; }

    }
}
