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
        return await _unitOfWork.GetRepository<Product>().GetAllAsync();
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await _unitOfWork.GetRepository<Product>().GetByIdAsync(id);
    }

    public async Task AddProductAsync(Product product)
    {
        await _unitOfWork.GetRepository<Product>().AddAsync(product);
        await _unitOfWork.CommitChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        var existingProduct = await _unitOfWork.GetRepository<Product>().GetByIdAsync(product.Id);

        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            _unitOfWork.GetRepository<Product>().Update(existingProduct);
            await _unitOfWork.CommitChangesAsync();
        }
    }

    public async Task DeleteProductAsync(string id)
    {
        var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(id);

        if (product != null)
        {
            _unitOfWork.GetRepository<Product>().Delete(product);
            await _unitOfWork.CommitChangesAsync();
        }
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _unitOfWork.GetRepository<Product>().FindAsync(predicate);
    }
}