using System.Linq.Expressions;
using Transport.Infrastructure.Data;
using Transport.Infrastructure.Data.Entities;
using Transport.Infrastructure.Repositories.Abstractions;

namespace Transport.Infrastructure.Repositories.Implementations;

public class EmployeeRepository : RepositoryBase<Employee, Guid>, IEmployeeRepository
{
    public EmployeeRepository(TransportContext context) : base(context)
    {
    }

    protected override Expression<Func<Employee, bool>> GetByIdExpression(Guid entityId)
    {
        return x => x.Id == entityId;
    }
}