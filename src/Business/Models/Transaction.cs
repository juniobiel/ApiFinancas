﻿namespace Business.Models
{
    public class Transaction : Entity
    {
        public Guid TransactionId { get; set; }
        public Guid AccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int? CategoryId { get; set; }
        public decimal Value { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }

        public Guid? AccountReceiverId { get; set; }

        public Guid UserId_Created { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UserId_Updated { get; set; }
        public DateTime? UpdatedAt { get; set; }


        /* EF Relations */
        public Account Account { get; set; }
        public Account AccountReceiver { get; set; }
        public Category Category { get; set; }

    }
}
