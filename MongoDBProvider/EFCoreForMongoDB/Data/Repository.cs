namespace EFCoreForMongoDB.Data;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    private readonly DbSet<T> _dbSet;

    public Repository(IDbContext context)
    {
        _dbSet = context.GetDbSet<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        return await _dbSet.FindAsync(id) 
            ?? throw new KeyNotFoundException(
                $"Entity with ID '{id}' not found.");
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
}