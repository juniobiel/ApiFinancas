using Business.Models;

namespace Business.Interfaces.Services
{
    public interface IAccountService : IDisposable
    {
        Task Add( Account account );

        Task Update( Account account );

        Task Remove( Guid id );

        Task<Account> GetAccountById( Guid? id );

        Task<IEnumerable<Account>> GetAccounts();

    }
}
