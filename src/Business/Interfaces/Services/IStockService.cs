using Business.Models;

namespace Business.Interfaces.Services
{
    public interface IStockService : IDisposable
    {
        Task Add( Stock stock );

        Task<Stock> GetStockByTicker( string ticker );
    }
}
