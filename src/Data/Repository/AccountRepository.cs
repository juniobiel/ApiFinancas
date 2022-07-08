using Business.Interfaces.Repositories;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository( FinanceDbContext context ) : base(context) { }

        public async Task<Account> GetAccountUserById( Guid userId, Guid? id )
        {
            return await Db.Accounts.AsNoTracking()
                .Where(a => a.UserId_Created == userId)
                .FirstOrDefaultAsync(t => t.AccountId == id);
        }


        public async Task<IEnumerable<Account>> GetUserAccounts( Guid userId )
        {
            return await Db.Accounts.AsNoTracking()
                .Where(a => a.UserId_Created == userId)
                .ToListAsync();
        }

        public async Task Remove( Guid id )
        {
            Db.Accounts.Remove(new Account { AccountId = id });
            await SaveChanges();
        }
    }
}
