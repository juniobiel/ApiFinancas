using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesUser( Guid userId );

        Task<Category> GetCategoryUserById( Guid userId, int id );

        Task Remove( int id );
    }
}
