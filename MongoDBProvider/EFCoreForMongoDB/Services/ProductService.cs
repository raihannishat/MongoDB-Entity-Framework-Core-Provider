namespace EFCoreForMongoDB.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _unitOfWork.Repository<Product>().GetAllAsync();
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await _unitOfWork.Repository<Product>().GetByIdAsync(id);
    }

    public async Task AddProductAsync(Product product)
    {
        await _unitOfWork.Repository<Product>().AddAsync(product);
        await _unitOfWork.CommitChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        var productEntity = await _unitOfWork.Repository<Product>().GetByIdAsync(product.Id);

        if (productEntity != null)
        {
            _unitOfWork.Repository<Product>().Update(product);
            await _unitOfWork.CommitChangesAsync();
        }
    }

    public async Task DeleteProductAsync(string id)
    {
        var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);

        if (product != null)
        {
            _unitOfWork.Repository<Product>().Delete(product);
            await _unitOfWork.CommitChangesAsync();
        }
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _unitOfWork.Repository<Product>().FindAsync(predicate);
    }
}