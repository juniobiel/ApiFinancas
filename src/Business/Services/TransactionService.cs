using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;
using Business.Services.Validations;

namespace Business.Services
{
    public class TransactionService : BaseService, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;
        public TransactionService( INotificator notificator,
                ITransactionRepository transactionRepository,
                IAccountService accountService,
                ICategoryService categoryService ) : base(notificator)
        {
            _transactionRepository = transactionRepository;
            _accountService = accountService;
            _categoryService = categoryService;
        }

        public async Task Add( Transaction transaction )
        {
            if (!ExecuteValidation(new TransactionValidation(), transaction)) return;

            var account = await _accountService.GetAccountById(transaction.AccountId);

            if(account == null)
            {
                base.Notify("A conta selecionada é inválida");
                return;
            }
            
            //se categoryid for nulo deve ser do tipo transferencia
            //se categoryid não for nulo, deve verificar se o transactiontype da categoria é compatível


            switch(transaction.TransactionType)
            {
                case TransactionType.Revenue:
                    account.AccountBalance += transaction.Value;
                    await _accountService.Update(account);
                    break;

                case TransactionType.Expense:
                    account.AccountBalance -= transaction.Value;
                    await _accountService.Update(account);
                    break;

                case TransactionType.Transfer:
                    var accountReceiver = await _accountService.GetAccountById(transaction.AccountReceiverId);

                    if (transaction.AccountReceiverId == null || accountReceiver == null)
                    {
                        base.Notify("É preciso selecionar uma conta de destino válida");
                        return;
                    }

                    account.AccountBalance -= transaction.Value;
                    accountReceiver.AccountBalance += transaction.Value;
                    await _accountService.Update(account);
                    await _accountService.Update(accountReceiver);
                    break;

                default:
                    base.Notify("Não foi possível completar esta operação!");
                    break;
            }

            await _transactionRepository.Add( transaction );
        }

        public void Dispose()
        {
            _transactionRepository?.Dispose();
        }
    }
}
