using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface IStockTransactionRepository : IRepository<StockTransaction>
    {
        Task<IEnumerable<StockTransaction>> GetTransactionsByTicker( Guid userId, string ticker );
    }
}
