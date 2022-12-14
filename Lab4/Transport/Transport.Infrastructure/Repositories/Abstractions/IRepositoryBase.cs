namespace Transport.Infrastructure.Repositories.Abstractions;

public interface IRepositoryBase<T, TId>
    where T : class
{
    Task Delete(TId id);
    Task Update(T entity);
    Task Create(T entity);
    Task<T> GetById(TId id);
    Task<List<T>> GetEntities();
    IQueryable<T> QueryEntities();
}