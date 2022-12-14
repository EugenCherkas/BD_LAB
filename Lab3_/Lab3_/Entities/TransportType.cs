using System.Collections.Generic;
using System.ComponentModel;

namespace Transport.Infrastructure.Data.Entities;

public class TransportType
{
    public int Id { get; set; }

    [DisplayName("Наименование")]
    public string Name { get; set; }


    public List<Route> Routes { get; set; }
}