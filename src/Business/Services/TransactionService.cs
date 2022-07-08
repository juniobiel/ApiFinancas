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

            transaction.UserId_Created = _appUser.GetUserId();
            transaction.CreatedAt = DateTime.Now;

            var account = await _accountService.GetAccountById(transaction.AccountId);

            if (account == null)
            {
                base.Notify("A conta selecionada é inválida");
                return;
            }

            await TransactionAddingValidation(transaction, account);

            await _transactionRepository.Add(transaction);
        }


        public async Task Update( Transaction transaction )
        {
            var transactionAux = await GetTransactionById(transaction.TransactionId);

            transaction.UserId_Created = transactionAux.UserId_Created;
            transaction.CreatedAt = transactionAux.CreatedAt;

            transaction.UserId_Updated = _appUser.GetUserId();
            transaction.UpdatedAt = DateTime.Now;

            if (transaction == null)
            {
                base.Notify("O ID é inválido!");
                return;
            }

            if (transaction.TransactionType != transactionAux.TransactionType)
            {
                base.Notify("Não é possível editar o tipo da transação");
                return;
            }

            var category = await _categoryService.GetCategoryById((int)transaction.CategoryId);

            if (category == null)
            {
                base.Notify("Não foi possível alterar para a categoria selecionada");
                return;
            }

            if (category.TransactionType != transaction.TransactionType)
            {
                base.Notify("Esta categoria não é permitida para este tipo de operação");
                return;
            }

            var account = await _accountService.GetAccountById(transaction.AccountId);

            if (account == null)
            {
                base.Notify("A conta selecionada não é valida!");
                return;
            }

            await TransactionUpdatingValidation(transaction, transactionAux, account);


            await _transactionRepository.Update(transaction);
        }

        public async Task DeleteTransaction( Transaction transaction )
        {
            var transactionAux = await GetTransactionById(transaction.TransactionId);

            if (transactionAux == null)
            {
                base.Notify("Não foi possível deletar esta transação");
                return;
            }

            await _transactionRepository.Remove(transaction.TransactionId);
        }


        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await _transactionRepository.GetUserTransactions(_appUser.GetUserId());
        }

        public async Task<IEnumerable<Transaction>> GetUserTransactionsByPeriod( DateTime? initialDate, DateTime? finalDate )
        {
            if (initialDate == null || finalDate == null)
            {
                base.Notify("Não é possível filtrar o período selecionado");
            }

            return await _transactionRepository.GetUserTransactionsByPeriod(_appUser.GetUserId(), initialDate, finalDate);
        }
        public async Task<Transaction> GetTransactionById( Guid transactionId )
        {
            return await _transactionRepository.GetUserTransactionById(_appUser.GetUserId(), transactionId);
        }

        public void Dispose()
        {
            _transactionRepository?.Dispose();
        }

        private async Task TransactionAddingValidation( Transaction transaction, Account account )
        {
            switch (transaction.TransactionType)
            {
                case TransactionType.Revenue:
                    var categoryRevenue = await _categoryService.GetCategoryById((int)transaction.CategoryId);

                    if (categoryRevenue == null)
                    {
                        base.Notify("Categoria selecionada é inexistente");
                        return;
                    }

                    if (categoryRevenue.UserId_Created != transaction.UserId_Created
                        || categoryRevenue.TransactionType != transaction.TransactionType)
                    {
                        base.Notify("Categoria inválida");
                        return;
                    }

                    if (transaction.AccountReceiverId != null)
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

                    if (categoryExpense.UserId_Created != transaction.UserId_Created
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

                    if (transaction.CategoryId != null)
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
                    return;
            }
        }

        private async Task TransactionUpdatingValidation( Transaction transaction, Transaction transactionAux, Account account )
        {
            switch (transaction.TransactionType)
            {
                case TransactionType.Revenue:

                    if (transaction.Value != transactionAux.Value)
                    {
                        if (transaction.AccountId != transactionAux.AccountId)
                        {
                            var accountAux = await _accountService.GetAccountById(transactionAux.AccountId);

                            accountAux.AccountBalance -= transactionAux.Value;
                            account.AccountBalance += transaction.Value;

                            await _accountService.Update(account);
                            await _accountService.Update(accountAux);
                        }

                        account.AccountBalance -= transactionAux.Value;
                        account.AccountBalance += transaction.Value;

                        await _accountService.Update(account);
                    }

                    break;

                case TransactionType.Expense:

                    if (transaction.Value != transactionAux.Value)
                    {
                        if (transaction.AccountId != transactionAux.AccountId)
                        {
                            var accountAux = await _accountService.GetAccountById(transactionAux.AccountId);

                            accountAux.AccountBalance += transactionAux.Value;
                            account.AccountBalance -= transaction.Value;

                            await _accountService.Update(account);
                            await _accountService.Update(accountAux);
                        }

                        account.AccountBalance -= transactionAux.Value;
                        account.AccountBalance += transaction.Value;

                        await _accountService.Update(account);
                    }

                    break;

                case TransactionType.Transfer:
                    //valor diferente
                    if (transaction.Value != transactionAux.Value)
                    {
                        //Conta origem diferente
                        if (transaction.AccountId != transactionAux.AccountId)
                        {
                            var accountAux = await _accountService.GetAccountById(transactionAux.AccountId);

                            accountAux.AccountBalance += transactionAux.Value;
                            account.AccountBalance -= transaction.Value;

                            await _accountService.Update(accountAux);
                            await _accountService.Update(account);
                        }

                        //conta de destino diferente
                        if (transaction.AccountReceiverId != transactionAux.AccountReceiverId)
                        {
                            var destinationAccount = await _accountService.GetAccountById(transaction.AccountReceiverId);
                            var destinationAccountAux = await _accountService.GetAccountById(transactionAux.AccountReceiverId);

                            destinationAccountAux.AccountBalance -= transaction.Value;
                            destinationAccount.AccountBalance += transaction.Value;

                            await _accountService.Update(destinationAccountAux);
                            await _accountService.Update(destinationAccount);

                        }

                        account.AccountBalance -= transactionAux.Value;
                        account.AccountBalance += transaction.Value;

                        await _accountService.Update(account);
                    }

                    break;

                default:
                    base.Notify("Não consegui atualizar os dados da transferência");
                    return;
            }
        }
    }
}
