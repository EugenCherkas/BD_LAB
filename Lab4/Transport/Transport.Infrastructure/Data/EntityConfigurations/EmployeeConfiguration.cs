using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transport.Infrastructure.Data.Entities;

namespace Transport.Infrastructure.Data.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .HasOne(x => x.Route)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.RouteId);

        builder
            .HasOne(x => x.Rank)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.RankId);
    }
}