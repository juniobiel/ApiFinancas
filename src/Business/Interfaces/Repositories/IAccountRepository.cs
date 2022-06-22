using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<IEnumerable<Account>> GetAccountTransactions( Guid id );

        Task<Account> GetAccountById( Guid id );

        Task<IEnumerable<Account>> GetAccountsByUserId( Guid userId );
        Task Remove( Guid id );
    }
}
