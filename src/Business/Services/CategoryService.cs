using Business.Interfaces;
using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;
using Business.Services.Validations;

namespace Business.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUser _appUser;
        public CategoryService( ICategoryRepository categoryRepository,
                                INotificator notificator,
                                IUser appUser ) : base(notificator)
        {
            _categoryRepository = categoryRepository;
            _appUser = appUser;
        }

        public async Task Add(Category category)
        {
            if (!ExecuteValidation(new CategoryValidation(), category)) return;

            category.CategoryCreatedByUserId = _appUser.GetUserId();
            category.CreatedAt = DateTime.Now;

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

        public async Task Update(Category category)
        {
            if (!ExecuteValidation(new CategoryValidation(), category)) return;

            var categoryAux = await GetCategoryById(category.CategoryId);

            category.CategoryCreatedByUserId = categoryAux.CategoryCreatedByUserId;
            category.CreatedAt = categoryAux.CreatedAt;
            category.CategoryUpdatedByUserId = _appUser.GetUserId();
            category.UpdatedAt = DateTime.Now;

            await _categoryRepository.Update(category);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _categoryRepository.GetCategoriesUser(_appUser.GetUserId());
        }

        public async Task<Category> GetCategoryById( int id )
        {
            return await _categoryRepository.GetCategoryUserById(_appUser.GetUserId(), id);
        }

        public Task Remove( int id )
        {
           return _categoryRepository.Remove(id);
        }

        public void Dispose()
        {
            _categoryRepository?.Dispose();
        }
    }
}
