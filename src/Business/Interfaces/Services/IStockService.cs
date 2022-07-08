using Business.Models;

namespace Business.Interfaces.Services
{
    public interface IStockService : IDisposable
    {
        Task<int> Add( Stock stock );

        Task Update( Stock stock );

        Task<Stock> GetStockByTicker( string ticker );
    }
}
