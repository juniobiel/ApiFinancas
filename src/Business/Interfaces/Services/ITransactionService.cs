using Business.Models;

namespace Business.Interfaces.Services
{
    public interface ITransactionService : IDisposable
    {
        Task Add( Transaction transaction );
    }
}
