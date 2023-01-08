using System.Linq.Expressions;
using Transport.Infrastructure.Data;
using Transport.Infrastructure.Data.Entities;
using Transport.Infrastructure.Repositories.Abstractions;

namespace Transport.Infrastructure.Repositories.Implementations;

public class RankRepository : RepositoryBase<Rank, int>, IRankRepository
{
    public RankRepository(TransportContext context) : base(context)
    {
    }

    protected override Expression<Func<Rank, bool>> GetByIdExpression(int entityId)
    {
        return x => x.Id == entityId;
    }
}