﻿namespace Business.Models
{
    public class Category : Entity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public TransactionType TransactionType { get; set; }

        public Guid AccountCreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? AccountUpdatedByUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /* EF Relations */
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
