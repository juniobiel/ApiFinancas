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

        /* EF Relations */
        public IEnumerable<Transaction> Transactions { get; set; }
        public Transaction TransferTransaction { get; set; }
    }
}
