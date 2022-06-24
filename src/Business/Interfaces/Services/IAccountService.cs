using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Services
{
    public interface IAccountService : IDisposable
    {
        Task Add( Account account );

        Task Update( Account account );

        Task Remove( Guid id );

        Task<Account> GetAccountById( Guid? id );

        Task<IEnumerable<Account>> GetAccountTransactions( Guid id );

        Task<IEnumerable<Account>> GetAll();

        Task<IEnumerable<Account>> GetAccountsByUserId(Guid userId );

    }
}
