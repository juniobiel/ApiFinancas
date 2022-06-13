using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;

namespace Business.Services
{
    internal class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService( ICategoryRepository categoryRepository,
                                INotificator notificator ) : base(notificator)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetCategoriesByTransactionType( TransactionType transactionType )
        {
            return await _categoryRepository.GetCategoriesByTransactionType(transactionType);
        }

        public async Task<Category> GetCategoryById( int id )
        {
            return await _categoryRepository.GetCategoryById(id);
        }

        public Task Remove( int id )
        {
           return _categoryRepository.Remove(id);
        }
    }
}
