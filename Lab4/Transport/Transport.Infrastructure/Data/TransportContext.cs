using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Transport.Infrastructure.Data.Entities;

namespace Transport.Infrastructure.Data;

public class TransportContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public DbSet<Route> Routes { get; set; }

    public DbSet<RouteStation> RouteStations { get; set; }

    public DbSet<Station> Stations { get; set; }

    public DbSet<TransportType> TransportTypes { get; set; }

    public DbSet<Rank> Ranks { get; set; }

    public TransportContext(DbContextOptions<TransportContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder
            .SeedTransportTypes()
            .SeedRanks();
    }
}