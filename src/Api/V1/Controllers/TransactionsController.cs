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
            if (!UserAuthenticated)
                return BadRequest("Efetue o login novamente!");

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _transactionService.Add(_mapper.Map<Transaction>(createTransactionViewModel));

            return CustomResponse(createTransactionViewModel);
        }

        [HttpGet("list")]
        public async Task<IEnumerable<TransactionViewModel>> ListTransactions()
        {
            return  _mapper.Map<IEnumerable<TransactionViewModel>>(await _transactionService.GetTransactions());
        }

        [HttpGet("filter-date")]
        public async Task<ActionResult> ListTransactionByPeriod(TransactionFilterViewModel transactionPeriodViewModel)
        {
            if (!UserAuthenticated)
                return BadRequest("Efetue o login novamente!");

            var result = await _transactionService
                .GetUserTransactionsByPeriod(transactionPeriodViewModel.InitialDate, transactionPeriodViewModel.FinalDate);

            if (result == null) return NotFound("Verifique o período selecionado!");

            return Ok(result);
        }


        [HttpPut("edit")]
        public async Task<ActionResult> EditTransaction(TransactionViewModel transactionEditViewModel)
        {
            if (!UserAuthenticated)
                return BadRequest("Efetue o login novamente!");


            if (transactionEditViewModel.TransactionId == null) return BadRequest("Informe o ID da transação");

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var transaction = await _transactionService.GetTransactionById((Guid)transactionEditViewModel.TransactionId);

            if (transaction == null)
            {
                return NotFound("Não é uma transação válida");
            }

            await _transactionService.Update(_mapper.Map<Transaction>(transactionEditViewModel));

            return StatusCode(200, new
            {
                message = "Transação atualizada com sucesso!",
                transactionEditViewModel,
                transaction
            });

        }
    }
}
