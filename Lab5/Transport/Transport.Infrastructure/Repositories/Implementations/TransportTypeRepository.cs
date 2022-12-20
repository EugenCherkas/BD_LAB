using System.Linq.Expressions;
using Transport.Infrastructure.Data;
using Transport.Infrastructure.Data.Entities;
using Transport.Infrastructure.Repositories.Abstractions;

namespace Transport.Infrastructure.Repositories.Implementations;

public class TransportTypeRepository : RepositoryBase<TransportType, int>, ITransportTypeRepository
{
    public TransportTypeRepository(TransportContext context) : base(context)
    {
    }

    protected override Expression<Func<TransportType, bool>> GetByIdExpression(int entityId)
    {
        return x => x.Id == entityId;
    }
}