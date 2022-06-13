using Business.Interfaces.Repositories;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Data.Repository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository( FinanceDbContext context ) : base(context) { }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccount( Guid accountId )
        {
            return (IEnumerable<Transaction>)await Db.Transactions.AsNoTracking()
                                                .Include(a => a.Account)
                                                .FirstOrDefaultAsync(t => t.AccountId == accountId);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByCategoryId( int categoryId )
        {
            return (IEnumerable<Transaction>)await Db.Transactions.AsNoTracking()
                                               .Include(c => c.Category)
                                               .FirstOrDefaultAsync(t => t.CategoryId == categoryId);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDate( DateTime date )
        {
            return await Db.Transactions.AsNoTracking()
                            .Where(t => t.TransactionDate == date)
                            .ToListAsync();
        }

        public async Task<Transaction> GetTransactionById( Guid id )
        {
            return await Db.Transactions.AsNoTracking().FirstOrDefaultAsync(t => t.TransactionId == id);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByTransactionType( TransactionType transactionType )
        {
            return await Db.Transactions.AsNoTracking()
                            .Where(t => t.TransactionType == transactionType)
                            .ToListAsync();
        }

        public async Task Remove(Guid id)
        {
            Db.Transactions.Remove(new Transaction { TransactionId = id });
            await base.SaveChanges();
        }
    }
}
