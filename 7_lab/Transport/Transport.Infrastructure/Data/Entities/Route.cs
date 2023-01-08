using System.ComponentModel;

namespace Transport.Infrastructure.Data.Entities;

public class Route
{
    public Guid Id { get; set; }

    [DisplayName("Наименование")]
    public string Name { get; set; }

    [DisplayName("День недели")]
    public byte DayOfWeek { get; set; }

    [DisplayName("Транспорт")]
    public int TransportTypeId { get; set; }

    [DisplayName("Экспресс")]
    public bool IsExpress { get; set; }

    [DisplayName("Длительность (мин)")]
    public int MinutesOnRoute { get; set; }

    [DisplayName("Длительность (км)")]
    public int KilometersOnRoute { get; set; }


    public List<Employee> Employees { get; set; }

    public List<RouteStation> RouteStations { get; set; }

    public TransportType TransportType { get; set; }
}