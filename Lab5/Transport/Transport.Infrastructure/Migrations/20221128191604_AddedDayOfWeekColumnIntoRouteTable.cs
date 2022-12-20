using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport.Infrastructure.Migrations
{
    public partial class AddedDayOfWeekColumnIntoRouteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "RouteStations");

            migrationBuilder.AddColumn<byte>(
                name: "DayOfWeek",
                table: "Routes",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f6e1271-329f-47cb-bd20-3b718a3fd906",
                column: "ConcurrencyStamp",
                value: "2b5bde62-6fe8-46ba-ac9e-1984c085cf5b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "976b252f-8101-47e4-9fd1-d9751274ffa3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ea01894b-031f-4770-b1fd-9ad3f008fd0a", "AQAAAAEAACcQAAAAEOFQehvAluRANELIcTKw7lnGqaUvFvKMvzyHUQvykrK/zxakkZJnoUiiCsca5yFyHQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Routes");

            migrationBuilder.AddColumn<byte>(
                name: "DayOfWeek",
                table: "RouteStations",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f6e1271-329f-47cb-bd20-3b718a3fd906",
                column: "ConcurrencyStamp",
                value: "9443747a-0d18-48be-8b06-407e2bcea71a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "976b252f-8101-47e4-9fd1-d9751274ffa3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f88b081c-a2b0-4953-b848-cd82e87cf96a", "AQAAAAEAACcQAAAAEPNQOLqGDqO4rWvvwHa1GgxOL9Zw1F+kqXNJ7HVFf1DIsCU2sZxKU1+qZBWjzR7feA==" });
        }
    }
}
