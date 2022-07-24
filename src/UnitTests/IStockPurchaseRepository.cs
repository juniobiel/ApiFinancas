namespace UnitTests
{
    public interface IStockPurchaseRepository
    {
        public Task<bool> AddNewPurchase( PurchaseData purchaseData );
    }
}
