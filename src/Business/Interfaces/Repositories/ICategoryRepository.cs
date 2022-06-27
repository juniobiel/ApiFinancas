﻿using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesUser(Guid userId);

        Task<Category> GetCategoryUserById(Guid userId, int id );

        Task Remove( int id );
    }
}
