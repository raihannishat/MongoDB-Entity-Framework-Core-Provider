namespace EFCoreForMongoDB.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(string id);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(string id);
    Task<string> GetProductTypeAsync(string productId);
    Task<IEnumerable<Product>> SearchProductsAsync(Expression<Func<Product, bool>> predicate);
}