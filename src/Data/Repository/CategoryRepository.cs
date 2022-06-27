using Business.Interfaces.Repositories;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository( FinanceDbContext context ) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetCategoriesUser(Guid userId)
        {
            return await Db.Categories.AsNoTracking()
                .Where(c => c.CategoryCreatedByUserId == userId).ToListAsync();
        }
        public async Task<Category> GetCategoryUserById(Guid userId, int id )
        {
            return await Db.Categories.AsNoTracking()
                .Where(c => c.CategoryCreatedByUserId == userId)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task Remove(int id)
        {
            Db.Categories.Remove(new Category { CategoryId = id });
            await base.SaveChanges();
        }
    }
}
