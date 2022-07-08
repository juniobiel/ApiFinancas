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
                    InitialDate = stockPurchase.PurchaseDate,
                    StockQt = stockPurchase.StockQt
                };

                stockPurchase.StockId = await _stockService.Add(stock);
            }
            else
            {
                stockPurchase.StockId = stock.StockId;
                stock.StockQt += stockPurchase.StockQt;
                await _stockService.Update(stock);
            }

            stockPurchase.UserId_Created = _appUser.GetUserId();
            stockPurchase.CreatedAt = DateTime.Now;

            await _stockPurchaseRepository.Add(stockPurchase);
        }

        public void Dispose()
        {
            _stockPurchaseRepository?.Dispose();
        }
    }
}
