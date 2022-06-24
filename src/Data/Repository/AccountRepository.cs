using Business.Interfaces.Repositories;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(FinanceDbContext context) : base(context) { }

        public async Task<IEnumerable<Account>> GetAccountTransactions(Guid id)
        {
            return await Db.Accounts.AsNoTracking()
                .Include(t => t.Transactions)
                .Where(t => t.AccountId == id)
                .ToListAsync();
        }

        public async Task<Account> GetAccountById(Guid? id)
        {
            return await Db.Accounts.AsNoTracking().FirstOrDefaultAsync(t => t.AccountId == id);
        }


        public async Task<IEnumerable<Account>> GetAccountsByUserId(Guid userId)
        {
            return await Db.Accounts.AsNoTracking()
                .Where(a => a.AccountCreatedByUserId == userId)
                .ToListAsync();
        }

        public async Task Remove( Guid id )
        {
            Db.Accounts.Remove(new Account { AccountId = id });
            await SaveChanges();
        }
    }
}
