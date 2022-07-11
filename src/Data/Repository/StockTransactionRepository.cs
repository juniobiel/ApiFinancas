using Business.Interfaces.Repositories;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class StockTransactionRepository : Repository<StockTransaction>, IStockTransactionRepository
    {
        public StockTransactionRepository( FinanceDbContext context ) : base(context) { }

        public async Task<IEnumerable<StockTransaction>> GetTransactionsByTicker( Guid userId, string ticker )
        {
            return await Db.StockTransactions.AsNoTracking()
                .Where(t => t.UserId_Created == userId && t.StockTicker == ticker)
                .ToListAsync();
        }
    }
}
