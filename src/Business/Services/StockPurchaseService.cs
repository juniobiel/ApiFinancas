using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;

namespace Business.Services
{
    public class StockPurchaseService
    {
        private readonly IStockPurchaseRepository _stockPurchaseRepository;

        public StockPurchaseService(IStockPurchaseRepository stockPurchaseRepository)
        {
            _stockPurchaseRepository = stockPurchaseRepository;
        }

        public async Task<int> NewPurchase( PurchaseData data )
        {
            var result = await _stockPurchaseRepository.AddNewPurchase(data);
            if (!result)
                return StatusCodes.Status400BadRequest;

            return StatusCodes.Status201Created;
        }
    }
}
