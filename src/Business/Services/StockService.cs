using Business.Interfaces;
using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;

namespace Business.Services
{
    public class StockService : BaseService, IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IUser _appUser;

        public StockService( INotificator notificator,
            IStockRepository stockRepository,
            IUser appUser ) : base(notificator)
        {
            _stockRepository = stockRepository;
            _appUser = appUser;
        }

        public Task Add( Stock stock )
        {
            if (!ExecuteValidation(new StockValidation(), stock)) return;
        }

        public async Task<Stock> GetStockByTicker( string ticker )
        {
            return await _stockRepository.GetStock(_appUser.GetUserId(), ticker);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
