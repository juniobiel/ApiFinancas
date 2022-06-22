namespace Business.Models
{
    public class Account : Entity
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal AccountBalance { get; set; }
        public Guid AccountCreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? AccountUpdatedByUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /* EF Relations */
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<Transaction> ReceivedTransactions { get; set; }
    }
}
