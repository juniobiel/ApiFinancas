using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAccountUserById( Guid userId, Guid? id );
        Task<IEnumerable<Account>> GetUserAccounts( Guid userId );
        Task Remove( Guid id );
    }
}
