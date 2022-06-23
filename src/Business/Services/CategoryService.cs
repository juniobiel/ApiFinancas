using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;
using Business.Services.Validations;

namespace Business.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService( ICategoryRepository categoryRepository,
                                INotificator notificator ) : base(notificator)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Add(Category category)
        {
            if (!ExecuteValidation(new CategoryValidation(), category)) return;

            var categoryVerify = _categoryRepository
                                                    .Search(a => a.CategoryName == category.CategoryName
                                                    && a.TransactionType == category.TransactionType
                                                    && a.CategoryCreatedByUserId == category.CategoryCreatedByUserId);
            if (categoryVerify.Result.Any())
            {
                Notify("Já existe esta categoria!");
                return;
            }

            await _categoryRepository.Add(category);
        }

        public async Task<IEnumerable<Category>> GetCategoriesByTransactionType( TransactionType transactionType )
        {
            return await _categoryRepository.GetCategoriesByTransactionType(transactionType);
        }

        public async Task<IEnumerable<Category>> GetCategoriesByUserId(Guid userId)
        {
            return await _categoryRepository.GetCategoriesByUserId(userId);
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
