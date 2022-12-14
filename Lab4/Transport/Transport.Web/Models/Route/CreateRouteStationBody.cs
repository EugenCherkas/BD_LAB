namespace Transport.Web.Models.Route;

public class CreateRouteStationBody
{
    public Guid RouteId { get; set; }

    public int StationId { get; set; }

    public int Minutes { get; set; }

    public int Hours { get; set; }
}