using Business.Interfaces.Repositories;
using Business.Models;
using Data.Context;

namespace Data.Repository
{
    public class StockTransactionRepository : Repository<StockTransaction>, IStockTransactionRepository
    {
        public StockTransactionRepository( FinanceDbContext context ) : base(context) { }

    }
}
