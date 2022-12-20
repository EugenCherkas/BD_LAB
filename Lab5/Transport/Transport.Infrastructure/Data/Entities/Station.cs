using System.ComponentModel;

namespace Transport.Infrastructure.Data.Entities;

public class Station
{
    public int Id { get; set; }

    [DisplayName("Наименование")]
    public string Name { get; set; }

    [DisplayName("С диспетчерской")]
    public bool WithControlRoom { get; set; }


    public List<RouteStation> RouteStations { get; set; }
}