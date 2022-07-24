using Business.Models;

namespace Business.Interfaces
{
    public interface IStockPurchaseRepository
    {
        public Task<bool> AddNewPurchase( PurchaseData purchaseData );
    }
}
