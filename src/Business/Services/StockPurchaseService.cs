using Business.Interfaces;
using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;
using Business.Services.Validations;

namespace Business.Services
{
    public class StockPurchaseService : BaseService, IStockPurchaseService
    {
        private readonly IStockPurchaseRepository _stockPurchaseRepository;
        private readonly IStockService _stockService;
        private readonly IUser _appUser;
        public StockPurchaseService( INotificator notificator,
            IStockPurchaseRepository stockPurchaseRepository,
            IStockService stockService,
            IUser appUser ) : base(notificator)
        {
            _stockPurchaseRepository = stockPurchaseRepository;
            _stockService = stockService;
            _appUser = appUser;
        }

        public async Task Add( StockPurchase stockPurchase )
        {
            if (!ExecuteValidation(new StockPurchaseValidation(), stockPurchase)) return;

            var stock = await _stockService.GetStockByTicker(stockPurchase.StockTicker);

            if (stock == null)
            {
                stock = new Stock()
                {
                    StockTicker = stockPurchase.StockTicker,
                    InitialPrice = stockPurchase.StockPrice,
                    InitialDate = DateTime.Now,
                };

                stockPurchase.StockId = await _stockService.Add(stock);
            }

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
