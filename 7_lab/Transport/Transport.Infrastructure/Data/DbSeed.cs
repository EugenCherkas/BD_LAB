using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Transport.Infrastructure.Data.Entities;

namespace Transport.Infrastructure.Data;

public static class DbSeed
{
    public static ModelBuilder SeedRanks(this ModelBuilder builder)
    {
        builder.Entity<Rank>()
            .HasData(new List<Rank>
            {
                new()
                {
                    Id = 1,
                    Name = "Техник"
                },
                new()
                {
                    Id = 2,
                    Name = "Водитель"
                }
            });

        return builder;
    }

    public static ModelBuilder SeedTransportTypes(this ModelBuilder builder)
    {
        builder.Entity<TransportType>()
            .HasData(new List<TransportType>
            {
                new()
                {
                    Id = 1,
                    Name = "Троллейбус"
                },
                new()
                {
                    Id = 2,
                    Name = "Автобус"
                }
            });

        return builder;
    }

    public static ModelBuilder SeedUserRoles(this ModelBuilder builder)
    {
        const string adminRoleId = "6f6e1271-329f-47cb-bd20-3b718a3fd906";
        const string adminRoleName = "Admin";

        const string adminUserId = "976b252f-8101-47e4-9fd1-d9751274ffa3";
        const string adminUserName = "admin@admin.com";
        const string adminUserPassword = "Derq_1asddsa";

        var userToCreate = new IdentityUser
        {
            Id = adminUserId,
            UserName = adminRoleName,
            NormalizedUserName = adminRoleName.ToUpper(),
            Email = adminUserName,
            NormalizedEmail = adminRoleName.ToUpper(),
            SecurityStamp = "ecd71cf1-f6a2-4627-86ba-f657eb8fbdcc",
        };

        var password = new PasswordHasher<IdentityUser>();
        var hashed = password.HashPassword(userToCreate, adminUserPassword);
        userToCreate.PasswordHash = hashed;

        builder.Entity<IdentityUser>()
            .HasData(new List<IdentityUser>
            {
                userToCreate
            });

        builder.Entity<IdentityRole>()
            .HasData(new List<IdentityRole>
            {
                new()
                {
                    Id = adminRoleId,
                    Name = adminRoleName,
                    NormalizedName = adminRoleName.ToUpper()
                }
            });

        builder.Entity<IdentityUserRole<string>>()
            .HasData(new List<IdentityUserRole<string>>
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                }
            });

        return builder;
    }
}