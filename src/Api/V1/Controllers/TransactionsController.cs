using Api.Controllers;
using Api.V1.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Services;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/transactions")]
    public class TransactionsController : MainController
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        public TransactionsController( INotificator notificator, IUser appUser, 
                    ITransactionService transactionService, 
                    IMapper mapper ) : base(notificator, appUser)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpPost("create-transaction")]
        public  async Task<ActionResult> CreateTransaction(TransactionViewModel createTransactionViewModel)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            await _transactionService.Add(_mapper.Map<Transaction>(createTransactionViewModel));

            return CustomResponse(createTransactionViewModel);
        }
    }
}
