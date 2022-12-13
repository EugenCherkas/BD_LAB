using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transport.Infrastructure.Data.Entities;

namespace Transport.Infrastructure.Data.EntityConfigurations
{


    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder
                .HasOne(x => x.TransportType)
                .WithMany(x => x.Routes)
                .HasForeignKey(x => x.TransportTypeId);
        }
    }
}