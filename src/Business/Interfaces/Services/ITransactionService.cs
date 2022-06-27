using Business.Models;

namespace Business.Interfaces.Services
{
    public interface ITransactionService : IDisposable
    {
        Task<IEnumerable<Transaction>> GetTransactions();

        Task<IEnumerable<Transaction>> GetUserTransactionsByPeriod( DateTime? initialDate, DateTime? finalDate );
        Task<Transaction> GetTransactionById( Guid transactionId );
        Task Update( Transaction transaction );
        Task Add( Transaction transaction );
    }
}
