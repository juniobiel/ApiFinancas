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

            try
            {
                var tickerSemiFinalSelector = int.Parse(stock.StockTicker.Substring(stock.StockTicker.Length - 2, 1));
                var tickerFinalSelector = int.Parse(stock.StockTicker.Substring(stock.StockTicker.Length - 1, 1));

                var tickerSelector = int.Parse((tickerSemiFinalSelector.ToString() + tickerFinalSelector.ToString()));

                if(tickerSelector == 11)
                {
                    stock.StockType = StockType.RealState;
                }
                else if(tickerSelector >= 32 && tickerSelector <= 35)
                {
                    stock.StockType = StockType.BDR;
                }
            }
            catch(FormatException)
            {
                try
                {
                    var tickerFinalSelector = int.Parse(stock.StockTicker.Substring(stock.StockTicker.Length - 1, 1));

                    if(tickerFinalSelector >= 3 && tickerFinalSelector <= 8)
                    {
                        stock.StockType = StockType.Stock;
                    }
                        
                }
                catch(FormatException)
                {
                    base.Notify("Confira o ticker informado e tente novamente");
                    return -1;
                }
            }

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
