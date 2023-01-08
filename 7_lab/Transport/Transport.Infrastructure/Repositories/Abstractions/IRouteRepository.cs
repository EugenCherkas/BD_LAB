using Transport.Infrastructure.Data.Entities;

namespace Transport.Infrastructure.Repositories.Abstractions;

public interface IRouteRepository : IRepositoryBase<Route, Guid>
{
    Task AppendRouteStation(RouteStation routeStation);
    Task RemoveRouteStation(Guid routeId, int stationId);
    Task<List<RouteStation>> GetRouteStations(Guid routeId);
}