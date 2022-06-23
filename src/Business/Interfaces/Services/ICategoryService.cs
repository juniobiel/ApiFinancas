using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesByTransactionType( TransactionType transactionType );

        Task<IEnumerable<Category>> GetCategoriesByUserId(Guid userId);

        Task Add( Category category );

        Task<Category> GetCategoryById( int id );

        Task Remove( int id );
    }
}
