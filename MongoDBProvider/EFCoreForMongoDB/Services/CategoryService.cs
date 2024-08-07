namespace EFCoreForMongoDB.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _unitOfWork.GetRepository<Category>().GetAllAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(string id)
    {
        return await _unitOfWork.GetRepository<Category>().GetByIdAsync(id);
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _unitOfWork.GetRepository<Category>().AddAsync(category);
        await _unitOfWork.CommitChangesAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        var existingCategory = await _unitOfWork.GetRepository<Category>().GetByIdAsync(category.Id);

        if (existingCategory != null)
        {
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            _unitOfWork.GetRepository<Category>().Update(existingCategory);
            await _unitOfWork.CommitChangesAsync();
        } 
    }

    public async Task DeleteCategoryAsync(string id)
    {
        var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(id);

        if (category != null)
        {
            _unitOfWork.GetRepository<Category>().Delete(category);
            await _unitOfWork.CommitChangesAsync();
        }
    }

    public async Task<IEnumerable<Category>> SearchCategoriesAsync(Expression<Func<Category, bool>> predicate)
    {
        return await _unitOfWork.GetRepository<Category>().FindAsync(predicate);
    }
}
