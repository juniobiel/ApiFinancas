using Business.Interfaces.Repositories;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;


namespace Data.Repository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository( FinanceDbContext context ) : base(context) { }


        public async Task<IEnumerable<Transaction>> GetUserTransactions( Guid userId )
        {
            return await Db.Transactions.AsNoTracking()
                .Where(t => t.UserId_Created == userId).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetUserTransactionsByPeriod( Guid userId, DateTime? initialDate, DateTime? finalDate )
        {
            return await Db.Transactions.AsNoTracking()
                .Where(t => t.UserId_Created == userId
                && t.TransactionDate >= initialDate
                && t.TransactionDate <= finalDate).ToListAsync();
        }
        public async Task<Transaction> GetUserTransactionById( Guid userId, Guid transactionId )
        {
            return await Db.Transactions.AsNoTracking()
                .Where(t => t.UserId_Created == userId)
                .FirstOrDefaultAsync(t => t.TransactionId == transactionId);
        }

        public async Task Remove( Guid id )
        {
            Db.Transactions.Remove(new Transaction { TransactionId = id });
            await base.SaveChanges();
        }

    }
}
