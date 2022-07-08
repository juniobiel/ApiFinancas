using Business.Interfaces.Repositories;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        public StockRepository( FinanceDbContext context ) : base(context)
        {
        }

        public async Task<Stock> GetStock( Guid userId, string ticker )
        {
            return await Db.Stocks.AsNoTracking()
                .Where(s => s.UserId_Created == userId)
                .FirstOrDefaultAsync(s => s.StockTicker == ticker);
        }
    }
}
