using Api.Controllers;
using Api.V1.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Services;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Controllers
{
    [ApiVersion("api/v{version:apiVersion}/StockPurchase")]
    public class StockPurchasesController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IStockPurchaseService _stockPurchaseService;
        public StockPurchasesController( INotificator notificator, IUser appUser,
            IMapper mapper,
            IStockPurchaseService stockPurchaseService ) : base(notificator, appUser)
        {
            _mapper = mapper;
            _stockPurchaseService = stockPurchaseService;
        }

        [HttpPost("new-purchase")]
        public async Task<ActionResult> NewPurchase( StockPurchaseViewModel createViewModel )
        {
            if (!UserAuthenticated) return BadRequest("Efetue o login novamente!");

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _stockPurchaseService.Add(_mapper.Map<StockPurchase>(createViewModel));

            return CustomResponse(createViewModel);
        }

    }
}
