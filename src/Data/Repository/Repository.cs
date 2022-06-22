using Business.Interfaces.Repositories;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly FinanceDbContext Db;

        protected readonly DbSet<TEntity> DbSet;

        protected Repository(FinanceDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task Add( TEntity entity )
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task Update( TEntity entity )
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task<IEnumerable<TEntity>> Search( Expression<Func<TEntity, bool>> predicate )
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

        
    }
}
