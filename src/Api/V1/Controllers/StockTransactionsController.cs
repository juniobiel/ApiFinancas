using Api.Controllers;
using Api.V1.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Services;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/StockPurchase")]
    [Authorize(Roles = "RegularUsers")]
    public class StockTransactionsController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IStockTransactionService _stockTransactionService;
        public StockTransactionsController( INotificator notificator, IUser appUser,
            IMapper mapper,
            IStockTransactionService stockPurchaseService ) : base(notificator, appUser)
        {
            _mapper = mapper;
            _stockTransactionService = stockPurchaseService;
        }

        [HttpPost("new-purchase")]
        public async Task<ActionResult> NewPurchase( StockTransactionViewModel createViewModel )
        {
            if (!UserAuthenticated) return BadRequest("Efetue o login novamente!");

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (createViewModel.TransactionType != 3) return BadRequest("Algo deu errado!");

            await _stockTransactionService.Add(_mapper.Map<StockTransaction>(createViewModel));

            return CustomResponse(createViewModel);
        }

        [HttpPost("new-sell")]
        public async Task<ActionResult> NewSell(StockTransactionViewModel createViewModel)
        {
            if (!UserAuthenticated) return BadRequest("Efetue o login novamente!");

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (createViewModel.TransactionType != 4) return BadRequest("Algo deu errado!");

            await _stockTransactionService.Add(_mapper.Map<StockTransaction>(createViewModel));

            return CustomResponse(createViewModel);
        }

        [HttpGet("medium-price/{ticker}")]
        public async Task<ActionResult> GetPrice(string ticker)
        {
            if (!UserAuthenticated) return BadRequest("Efetue o login novamente!");

            var result = await _stockTransactionService.GetMediumPrice(ticker);

            return Ok($"O valor médio de {ticker} é {decimal.Round(result,2)}");
        }
    }
}
