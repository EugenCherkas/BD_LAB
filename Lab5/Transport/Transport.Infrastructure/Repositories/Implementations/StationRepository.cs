using System.Linq.Expressions;
using Transport.Infrastructure.Data;
using Transport.Infrastructure.Data.Entities;
using Transport.Infrastructure.Repositories.Abstractions;

namespace Transport.Infrastructure.Repositories.Implementations;

public class StationRepository : RepositoryBase<Station, int>, IStationRepository
{
    public StationRepository(TransportContext context) : base(context)
    {
    }

    protected override Expression<Func<Station, bool>> GetByIdExpression(int entityId)
    {
        return x => x.Id == entityId;
    }
}