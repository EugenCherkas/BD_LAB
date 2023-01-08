using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transport.Infrastructure.Data.Entities;

namespace Transport.Infrastructure.Data.EntityConfigurations;

public class RouteStationConfiguration : IEntityTypeConfiguration<RouteStation>
{
    public void Configure(EntityTypeBuilder<RouteStation> builder)
    {
        builder
            .HasKey(x => new { x.RouteId, x.StationId });

        builder
            .HasOne(x => x.Route)
            .WithMany(x => x.RouteStations)
            .HasForeignKey(x => x.RouteId);

        builder
            .HasOne(x => x.Station)
            .WithMany(x => x.RouteStations)
            .HasForeignKey(x => x.StationId);
    }
}