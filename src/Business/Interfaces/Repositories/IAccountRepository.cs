using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<IEnumerable<Account>> GetAccountTransactions( Guid id );

        Task<Account> GetAccountById( Guid id );

        Task Remove( Guid id );
    }
}
