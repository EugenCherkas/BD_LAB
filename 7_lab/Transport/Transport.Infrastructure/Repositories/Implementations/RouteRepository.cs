using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Transport.Infrastructure.Data;
using Transport.Infrastructure.Data.Entities;
using Transport.Infrastructure.Repositories.Abstractions;

namespace Transport.Infrastructure.Repositories.Implementations;

public class RouteRepository : RepositoryBase<Route, Guid>, IRouteRepository
{
    public RouteRepository(TransportContext context) : base(context)
    {
    }

    protected override Expression<Func<Route, bool>> GetByIdExpression(Guid entityId)
    {
        return entity => entity.Id == entityId;
    }

    public async Task AppendRouteStation(RouteStation routeStation)
    {
        var maxOrderRouteStation = await Context.RouteStations
            .OrderByDescending(x => x.Order)
            .FirstOrDefaultAsync();

        routeStation.Order = maxOrderRouteStation == null
            ? 1
            : maxOrderRouteStation.Order + 1;

        Context.RouteStations.Add(routeStation);
        await Context.SaveChangesAsync();
    }

    public async Task RemoveRouteStation(Guid routeId, int stationId)
    {
        var routeStationToRemove = await Context.RouteStations
            .FirstOrDefaultAsync(x => x.RouteId == routeId && x.StationId == stationId);

        if (routeStationToRemove is not null)
        {
            Context.RouteStations.Remove(routeStationToRemove);
            await Context.SaveChangesAsync();
        }
    }

    public async Task<List<RouteStation>> GetRouteStations(Guid routeId)
    {
        var routeStations = await Context.RouteStations
            .Include(x => x.Station)
            .Where(x => x.RouteId == routeId)
            .AsNoTracking()
            .ToListAsync();

        return routeStations;
    }
}