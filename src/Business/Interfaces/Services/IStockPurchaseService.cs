using Business.Models;

namespace Business.Interfaces.Services
{
    public interface IStockPurchaseService : IDisposable
    {
        Task Add( StockPurchase stockPurchase );
    }
}
