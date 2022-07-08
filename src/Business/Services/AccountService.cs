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
        public AccountService( IAccountRepository accountRepository,
                                INotificator notificator,
                                IUser appUser ) : base(notificator)
        {
            _accountRepository = accountRepository;
            _appUser = appUser;
        }

        public async Task Add( Account account )
        {
            if (!ExecuteValidation(new AccountValidation(), account)) return;

            account.UserId_Created = _appUser.GetUserId();
            account.CreatedAt = DateTime.Now;

            if (_accountRepository.Search(a => a.AccountName == account.AccountName
            && a.UserId_Created == account.UserId_Created).Result.Any())
            {
                Notify("Já existe uma conta com este nome!");
                return;
            }

            await _accountRepository.Add(account);
        }

        public async Task Update( Account account )
        {
            if (!ExecuteValidation(new AccountValidation(), account)) return;

            var accountAux = await _accountRepository.GetAccountUserById(_appUser.GetUserId(), account.AccountId);

            account.UserId_Created = accountAux.UserId_Created;
            account.CreatedAt = accountAux.CreatedAt;

            account.UpdatedAt = DateTime.Now;
            account.UserId_Updated = _appUser.GetUserId();

            await _accountRepository.Update(account);
        }

        public async Task<Account> GetAccountById( Guid? id )
        {
            return await _accountRepository.GetAccountUserById(_appUser.GetUserId(), id);
        }

        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await _accountRepository.GetUserAccounts(_appUser.GetUserId());
        }
        public async Task Remove( Guid id )
        {
            await _accountRepository.Remove(id);
        }

        public void Dispose()
        {
            _accountRepository?.Dispose();
        }
    }
}
