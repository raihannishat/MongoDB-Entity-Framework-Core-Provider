namespace EFCoreForMongoDB.Data;

public interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : class, IEntity;
    Task<int> CommitChangesAsync();
}