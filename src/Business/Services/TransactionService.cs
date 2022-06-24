using Business.Interfaces;
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
        private readonly IUser _appUser;
        public TransactionService( INotificator notificator,
                ITransactionRepository transactionRepository,
                IAccountService accountService,
                ICategoryService categoryService,
                IUser appUser ) : base(notificator)
        {
            _transactionRepository = transactionRepository;
            _accountService = accountService;
            _categoryService = categoryService;
            _appUser = appUser;
        }

        public async Task Add( Transaction transaction )
        {
            if (!ExecuteValidation(new TransactionValidation(), transaction)) return;

            transaction.TransactionCreatedByUserId = _appUser.GetUserId();
            transaction.CreatedAt = DateTime.Now;

            var account = await _accountService.GetAccountById(transaction.AccountId);

            if (account == null)
            {
                base.Notify("A conta selecionada é inválida");
                return;
            }

            switch(transaction.TransactionType)
            {
                case TransactionType.Revenue:
                    var categoryRevenue = await _categoryService.GetCategoryById((int)transaction.CategoryId);

                    if(categoryRevenue == null)
                    {
                        base.Notify("Categoria selecionada é inexistente");
                        return;
                    }

                    if(categoryRevenue.CategoryCreatedByUserId != transaction.TransactionCreatedByUserId
                        || categoryRevenue.TransactionType != transaction.TransactionType)
                    {
                        base.Notify("Categoria inválida");
                        return;
                    }

                    if(transaction.AccountReceiverId != null)
                    {
                        base.Notify("Não é possível concluir a operação");
                    }

                    account.AccountBalance += transaction.Value;
                    await _accountService.Update(account);

                    break;

                case TransactionType.Expense:
                    var categoryExpense = await _categoryService.GetCategoryById((int)transaction.CategoryId);

                    if (categoryExpense == null)
                    {
                        base.Notify("Categoria selecionada é inexistente");
                        return;
                    }

                    if (categoryExpense.CategoryCreatedByUserId != transaction.TransactionCreatedByUserId
                        || categoryExpense.TransactionType != transaction.TransactionType)
                    {
                        base.Notify("Categoria inválida");
                        return;
                    }

                    if (transaction.AccountReceiverId != null)
                    {
                        base.Notify("Não é possível concluir a operação");
                    }

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

                    if(transaction.CategoryId != null)
                    {
                        base.Notify("Não é possível associar uma transferência a uma categoria");
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
