using Business.Models;
using System.Linq.Expressions;

namespace Business.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add( TEntity entity );
        Task<List<TEntity>> GetAll();
        Task Update( TEntity entity );
        Task<IEnumerable<TEntity>> Search( Expression<Func<TEntity, bool>> predicate );
        Task<int> SaveChanges();

    }
}
