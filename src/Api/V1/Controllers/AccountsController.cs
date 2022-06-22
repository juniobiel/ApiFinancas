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
    [Authorize(Roles = "RegularUsers")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/accounts")]
    public class AccountsController : MainController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountsController(INotificator notificator, IUser appUser, 
            IAccountService accountService, 
            IMapper mapper) : base(notificator, appUser)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost("create-account")]
        public async Task<ActionResult> CreateAccount(AccountViewModel createAccountViewModel)
        {
            createAccountViewModel.AccountId = Guid.NewGuid();

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            createAccountViewModel.AccountCreatedByUserId = UserId;
            createAccountViewModel.CreatedAt = DateTime.Now;            

            await _accountService.Add(_mapper.Map<Account>(createAccountViewModel));

            return CustomResponse(createAccountViewModel);
        }

        [HttpPut("edit/{accountId:guid}")]
        public async Task<ActionResult> EditAccount(Guid accountId, AccountViewModel accountUpdateViewModel)
        {
            if(accountId != accountUpdateViewModel.AccountId)
            {
                NotifyError("O ID informado é inválido");
                return CustomResponse(accountUpdateViewModel);
            }

            var account = await GetAccountByUserId(accountId);

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if(account == null)
            {
                return NotFound("Não é uma conta válida");
            }

            accountUpdateViewModel.CreatedAt = account.CreatedAt;
            accountUpdateViewModel.AccountCreatedByUserId = account.AccountCreatedByUserId;
            accountUpdateViewModel.UpdatedAt = DateTime.Now;
            accountUpdateViewModel.AccountUpdatedByUserId = UserId;

            await _accountService.Update(_mapper.Map<Account>(accountUpdateViewModel));

            return StatusCode(200, new 
            { 
                message = "Conta atualizada com sucesso!", 
                accountUpdateViewModel 
            });
        }
        
        [HttpGet("list")]
        public async Task<IEnumerable<AccountViewModel>> ListAll()
        {
            return _mapper.Map<IEnumerable<AccountViewModel>>(await _accountService.GetAccountsByUserId(UserId));
        }

        [HttpDelete("delete/{accountId:guid}")]
        public async Task<ActionResult> DeleteAccount(Guid accountId)
        {
            var account = await GetAccountByUserId(accountId);

            if (account == null) return NotFound("Não foi possível excluir a conta selicionada");

            await _accountService.Remove(accountId);
            return StatusCode(202, "A conta foi excluída!");
        }


        private async Task<Account> GetAccountByUserId(Guid accountId)
        {
            var account = await _accountService.GetAccountById(accountId);

            if (account.AccountCreatedByUserId != UserId)
            {
                return null;
            }

            return account;
        }
    }
}
