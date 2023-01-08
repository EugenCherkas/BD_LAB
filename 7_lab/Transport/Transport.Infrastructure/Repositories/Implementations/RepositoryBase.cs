using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Transport.Infrastructure.Data;
using Transport.Infrastructure.Repositories.Abstractions;

namespace Transport.Infrastructure.Repositories.Implementations;

public abstract class RepositoryBase<T, TId> : IRepositoryBase<T, TId>
    where T : class
{
    protected TransportContext Context;

    protected RepositoryBase(TransportContext context)
    {
        Context = context;
    }

    public async Task Delete(TId id)
    {
        var entityToDelete = await Context.Set<T>()
            .FirstOrDefaultAsync(GetByIdExpression(id));

        if (entityToDelete is not null)
        {
            Context.Remove(entityToDelete);
            await Context.SaveChangesAsync();
        }
    }

    public async Task Update(T entity)
    {
        Context.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task Create(T entity)
    {
        Context.Add(entity);
        await Context.SaveChangesAsync();
    }

    public async Task<T> GetById(TId id)
    {
        var entity = await Context.Set<T>()
            .FirstOrDefaultAsync(GetByIdExpression(id));

        return entity;
    }

    public async Task<List<T>> GetEntities()
    {
        var entities = await Context.Set<T>()
            .AsNoTracking()
            .ToListAsync();

        return entities;
    }

    public IQueryable<T> QueryEntities()
    {
        var query = Context.Set<T>()
            .AsNoTracking();

        return query;
    }

    protected abstract Expression<Func<T, bool>> GetByIdExpression(TId entityId);
}