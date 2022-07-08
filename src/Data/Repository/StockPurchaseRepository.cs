using Business.Interfaces.Repositories;
using Business.Models;
using Data.Context;

namespace Data.Repository
{
    public class StockPurchaseRepository : Repository<StockPurchase>, IStockPurchaseRepository
    {
        public StockPurchaseRepository( FinanceDbContext context ) : base(context) { }

    }
}
