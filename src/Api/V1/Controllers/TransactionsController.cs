using Api.Controllers;
using Api.V1.ViewModels;
using Business.Interfaces;
using Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion/transactions")]
    public class TransactionsController : MainController
    {
        public TransactionsController(INotificator notificator, IUser appUser) : base(notificator, appUser)
        {
        }

        //[HttpPost("create-transaction")]
        //public async Task<ActionResult> CreateTransaction(TransactionViewModel transactionViewModel)
        //{

        //}
    }
}
