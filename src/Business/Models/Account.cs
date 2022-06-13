namespace Business.Models
{
    public class Account : Entity
    {
        public Account()
        {
            AccountId = Guid.NewGuid();
        }

        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal AccountBalance { get; set; }
        public Guid AccountCreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? AccountUpdatedByUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /* EF Relations */
        public IEnumerable<Transaction> Transactions { get; set; }
        public Transaction TransferTransaction { get; set; }
    }
}
