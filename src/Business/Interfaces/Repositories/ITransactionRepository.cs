using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetUserTransactions( Guid userId );
        Task<IEnumerable<Transaction>> GetUserTransactionsByPeriod(Guid userId, DateTime? initialDate, DateTime? finalDate );
        Task<Transaction> GetUserTransactionById( Guid userId, Guid transactionId );
        Task Remove( Guid id );
    }
}
