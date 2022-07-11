using Business.Models;

namespace Business.Interfaces.Services
{
    public interface IStockTransactionService : IDisposable
    {
        Task Add( StockTransaction stockPurchase );

        Task<decimal> GetMediumPrice( string ticker );
    }
}
