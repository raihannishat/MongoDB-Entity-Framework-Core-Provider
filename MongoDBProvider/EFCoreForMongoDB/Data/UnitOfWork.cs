namespace EFCoreForMongoDB.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly IDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<Type, object> _repositories = [];

    public UnitOfWork(IDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public IRepository<T> GetRepository<T>() where T : class, IEntity
    {
        var type = typeof(T);

        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = _serviceProvider.GetRequiredService<IRepository<T>>();
        }

        return (IRepository<T>)_repositories[type];
    }

    public async Task<int> CommitChangesAsync()
    {
        return await _context.CommitChangesAsync();
    }

    public void Dispose() => _context.Dispose();
}