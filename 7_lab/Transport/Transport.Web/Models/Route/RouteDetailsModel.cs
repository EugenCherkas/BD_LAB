using Transport.Infrastructure.Data.Entities;

namespace Transport.Web.Models.Route;

public class RouteDetailsModel
{
    public List<RouteStation> RouteStations { get; set; }

    public Guid RouteId { get; set; }

    public List<Station> Stations { get; set; }
}