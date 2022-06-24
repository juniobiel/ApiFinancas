using Business.Models;

namespace Business.Interfaces.Services
{
    public interface ICategoryService : IDisposable
    {       
        Task<IEnumerable<Category>> GetCategoriesByUserId(Guid userId);

        Task Add( Category category );

        Task Update(Category category );

        Task<Category> GetCategoryById( int id );

        Task Remove( int id );
    }
}
