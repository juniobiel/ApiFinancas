﻿using Business.Interfaces.Repositories;
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

        public async Task<Category> GetCategoryById( int id )
        {
            return await Db.Categories.AsNoTracking().FirstOrDefaultAsync(t => t.CategoryId == id);
        }

        public async Task<IEnumerable<Category>> GetCategoriesByUserId(Guid userId)
        {
            return await Db.Categories.AsNoTracking()
                .Where(c => c.CategoryCreatedByUserId == userId).ToListAsync();
        }

        public async Task Remove(int id)
        {
            Db.Categories.Remove(new Category { CategoryId = id });
            await base.SaveChanges();
        }
    }
}
