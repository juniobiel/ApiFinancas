using Business.Interfaces;
using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;
using Business.Services.Validations;

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

        public async Task<int> Add( Stock stock )
        {
            if (!ExecuteValidation(new StockValidation(), stock)) return 0;

            stock.UserId_Created = _appUser.GetUserId();
            stock.CreatedAt = DateTime.Now;

            return (await _stockRepository.Add(_appUser.GetUserId(), stock)).StockId;
        }

        public async Task Update(Stock stock)
        {
            if (!ExecuteValidation(new StockValidation(), stock)) return;

            stock.UserId_Updated = _appUser.GetUserId();
            stock.UpdatedAt = DateTime.Now;

            await _stockRepository.Update(stock);
        }

        public async Task<Stock> GetStockByTicker( string ticker )
        {
            return await _stockRepository.GetStock(_appUser.GetUserId(), ticker);
        }

        public void Dispose()
        {
            _stockRepository?.Dispose();
        }
    }
}
