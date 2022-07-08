using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface IStockRepository : IRepository<Stock>
    {
        Task<Stock> GetStock( Guid userId, string ticker );

        Task<Stock> Add( Guid userId, Stock stock );
    }
}
