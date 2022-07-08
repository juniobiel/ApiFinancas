using Business.Interfaces;
using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;
using Business.Services.Validations;

namespace Business.Services
{
    public class StockTransactionService : BaseService, IStockTransactionService
    {
        private readonly IStockTransactionRepository _stockTransactionRepository;
        private readonly IStockService _stockService;
        private readonly IUser _appUser;
        public StockTransactionService( INotificator notificator,
            IStockTransactionRepository stockPurchaseRepository,
            IStockService stockService,
            IUser appUser ) : base(notificator)
        {
            _stockTransactionRepository = stockPurchaseRepository;
            _stockService = stockService;
            _appUser = appUser;
        }

        public async Task Add( StockTransaction stockTransaction )
        {
            if (!ExecuteValidation(new StockTransactionValidation(), stockTransaction)) return;

            var stock = await _stockService.GetStockByTicker(stockTransaction.StockTicker);

            switch (stockTransaction.TransactionType)
            {
                case TransactionType.Purchase:
                    if (stock == null)
                    {
                        stock = new Stock()
                        {
                            StockTicker = stockTransaction.StockTicker,
                            InitialPrice = stockTransaction.StockPrice,
                            InitialDate = stockTransaction.TransactionDate,
                            StockQt = stockTransaction.StockQt
                        };

                        stockTransaction.StockId = await _stockService.Add(stock);
                    }
                    else
                    {
                        stockTransaction.StockId = stock.StockId;
                        stock.StockQt += stockTransaction.StockQt;
                        await _stockService.Update(stock);
                    }
                    break;

                case TransactionType.Sell:
                    if (stock == null)
                    {
                        base.Notify("Não foi possível selecionar este item");
                        return;
                    }

                    stockTransaction.StockId = stock.StockId;
                    
                    if(stock.StockQt == stockTransaction.StockQt)
                    {
                        //Vendeu todas as ações
                        stock.StockQt -= stockTransaction.StockQt;
                        //await stockService.remove(stock);
                    }

                    else if(stock.StockQt > stockTransaction.StockQt)
                    {
                        //Vendeu 1 ou mais ações, podendo manter o saldo de até 1
                        stock.StockQt -= stockTransaction.StockQt;
                        await _stockService.Update(stock);
                    }

                    else
                    {
                        //Tentou vender mais ações do que havia
                        base.Notify("Você não possui saldo para essa operação");
                        return;
                    }

                    break;

                default:
                    base.Notify("Não foi possível definir o tipo de transação.");
                    break;
            }

            stockTransaction.UserId_Created = _appUser.GetUserId();
            stockTransaction.CreatedAt = DateTime.Now;

            await _stockTransactionRepository.Add(stockTransaction);
        }

        public void Dispose()
        {
            _stockTransactionRepository?.Dispose();
        }
    }
}
