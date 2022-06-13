namespace Business.Models
{
    public class Category : Entity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public TransactionType TransactionType { get; set; }

        /* EF Relations */
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
