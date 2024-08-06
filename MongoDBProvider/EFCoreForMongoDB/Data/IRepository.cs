namespace EFCoreForMongoDB.Data;

public interface IRepository<T> where T : class, IEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(string id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}