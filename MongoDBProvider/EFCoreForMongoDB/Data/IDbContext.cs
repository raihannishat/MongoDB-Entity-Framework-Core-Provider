namespace EFCoreForMongoDB.Data;

public interface IDbContext
{
    DbSet<T> GetDbSet<T>() where T : class, IEntity;
    Task<int> CommitChangesAsync();
    void Dispose();
}
