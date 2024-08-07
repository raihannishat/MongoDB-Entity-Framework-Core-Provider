using System.Threading.Tasks;

namespace EFCoreForMongoDB.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(string id);
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(string id);
    Task<IEnumerable<Category>> SearchCategoriesAsync(Expression<Func<Category, bool>> predicate);
}
