﻿namespace Business.Models
{
    public class Stock : Entity
    {
        public int StockId { get; set; }
        public string StockTicker { get; set; }
        public decimal InitialPrice { get; set; }
        public DateTime InitialDate { get; set; }
        public int StockQt { get; set; }

        public Guid UserId_Created { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UserId_Updated { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //EF Relations
        public IEnumerable<StockPurchase> StockPurchases { get; set; }
    }
}
