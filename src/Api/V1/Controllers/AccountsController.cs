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
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            createAccountViewModel.AccountCreatedByUserId = UserId;
            createAccountViewModel.CreatedAt = DateTime.Now;            

            await _accountService.Add(_mapper.Map<Account>(createAccountViewModel));

            return CustomResponse(createAccountViewModel);
        }

        
        [HttpGet("list")]
        public async Task<IEnumerable<AccountViewModel>> ListAll()
        {
            return _mapper.Map<IEnumerable<AccountViewModel>>(await _accountService.GetAll());
        }
    }
}
