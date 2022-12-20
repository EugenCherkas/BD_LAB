using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport.Infrastructure.Migrations
{
    public partial class AddedUserRolesSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6f6e1271-329f-47cb-bd20-3b718a3fd906", "d983378e-1c65-46aa-a43a-37b853310853", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "976b252f-8101-47e4-9fd1-d9751274ffa3", 0, "564cc43e-4fed-4fe6-a35a-ba0db4834086", "admin@admin.com", false, false, null, "ADMIN", "ADMIN", "AQAAAAEAACcQAAAAEJQI+pLC4MH6GUDVyKmEgUI7bc5KKVMSm7zdnDV+gomLZJR+crQpp/iV9LDIbbwsoQ==", null, false, "ecd71cf1-f6a2-4627-86ba-f657eb8fbdcc", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6f6e1271-329f-47cb-bd20-3b718a3fd906", "976b252f-8101-47e4-9fd1-d9751274ffa3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6f6e1271-329f-47cb-bd20-3b718a3fd906", "976b252f-8101-47e4-9fd1-d9751274ffa3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f6e1271-329f-47cb-bd20-3b718a3fd906");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "976b252f-8101-47e4-9fd1-d9751274ffa3");
        }
    }
}
