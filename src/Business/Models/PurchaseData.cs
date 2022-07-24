namespace Business.Models
{
    public class PurchaseData
    {
        public Guid TransactionId { get; set; }
        public string Ticker { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal PurchaseTaxes { get; set; }
        public StockType StockType { get; set; }
        public Guid UserCreated { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
