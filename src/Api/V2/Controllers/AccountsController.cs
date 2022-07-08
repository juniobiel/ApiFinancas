using Api.Controllers;
using Api.V2.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Services;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.V2.Controllers
{
    [Authorize(Roles = "RegularUsers")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/accounts")]
    public class AccountsController : MainController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountsController( INotificator notificator, IUser appUser,
            IAccountService accountService,
            IMapper mapper ) : base(notificator, appUser)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost("create-account")]
        public async Task<ActionResult> CreateAccount( AccountViewModel createAccountViewModel )
        {
            if (!UserAuthenticated)
                return BadRequest("Efetue o login novamente!");

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _accountService.Add(_mapper.Map<Account>(createAccountViewModel));

            return CustomResponse(createAccountViewModel);
        }

        [HttpPut("edit")]
        public async Task<ActionResult> EditAccount( AccountViewModel accountUpdateViewModel )
        {
            var account = await _accountService.GetAccountById(accountUpdateViewModel.AccountId);

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (account == null)
            {
                return NotFound("Não é uma conta válida");
            }

            await _accountService.Update(_mapper.Map<Account>(accountUpdateViewModel));

            return StatusCode(200, new
            {
                message = "Conta atualizada com sucesso!",
                accountUpdateViewModel,
                account
            });
        }

        [HttpGet("list")]
        public async Task<IEnumerable<AccountViewModel>> ListAccounts()
        {
            return _mapper.Map<IEnumerable<AccountViewModel>>(await _accountService.GetAccounts());
        }

        [HttpDelete("delete/{accountId:guid}")]
        public async Task<ActionResult> DeleteAccount( Guid accountId )
        {
            var account = await _accountService.GetAccountById(accountId);

            if (account == null) return NotFound("Não foi possível excluir a conta selicionada");

            await _accountService.Remove(accountId);
            return StatusCode(202, "A conta foi excluída!");
        }
    }
}
