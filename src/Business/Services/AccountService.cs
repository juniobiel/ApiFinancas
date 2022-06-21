using Business.Interfaces;
using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;
using Business.Services.Validations;

namespace Business.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUser _appUser;
        public AccountService(IAccountRepository accountRepository,
                                INotificator notificator,
                                IUser appUser) : base(notificator)
        {
            _accountRepository = accountRepository;
            _appUser = appUser;
        }

        public async Task Add( Account account )
        {
            if (!ExecuteValidation(new AccountValidation(), account)) return;

            if(_accountRepository.Search(a => a.AccountName == account.AccountName).Result.Any())
            {
                Notify("Já existe uma conta com este nome!");
                return;
            }

            await _accountRepository.Add(account);
        }


        public async Task Remove( Guid id )
        {
            if (_accountRepository.GetAccountTransactions(id).Result.Any())
            {
                Notify("A conta possui transações cadastradas");
                return;
            }

            await _accountRepository.Remove(id);        }

        public async Task Update( Account account )
        {
            if (!ExecuteValidation(new AccountValidation(), account)) return;

            await _accountRepository.Update(account);
        }

        public async Task<Account> GetAccountById(Guid id)
        {
            return await _accountRepository.GetAccountById(id);
        }

        public async Task<IEnumerable<Account>> GetAccountTransactions(Guid id)
        {
            return await _accountRepository.GetAccountTransactions(id);
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _accountRepository.GetAll();
        }

        public void Dispose()
        {
            _accountRepository?.Dispose();
        }
    }
}
