using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetTransactionsByAccount( Guid accountId );
        Task<IEnumerable<Transaction>> GetTransactionsByDate( DateTime date );
        Task<Transaction> GetTransactionById( Guid transactionId );
        Task<IEnumerable<Transaction>> GetTransactionsByCategoryId( int categoryId );
        Task<IEnumerable<Transaction>> GetTransactionsByTransactionType( TransactionType transactionType );
        Task Remove( Guid id );

    }
}
