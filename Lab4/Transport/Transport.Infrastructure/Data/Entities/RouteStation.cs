namespace Transport.Infrastructure.Data.Entities;

public class RouteStation
{
    public Guid RouteId { get; set; }

    public int StationId { get; set; }

    public int Hours { get; set; }

    public int Minutes { get; set; }

    public int Order { get; set; }


    public Route Route { get; set; }

    public Station Station { get; set; }
}