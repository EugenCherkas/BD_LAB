using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport.Infrastructure.Migrations
{
    public partial class AddedTransportTypeSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f6e1271-329f-47cb-bd20-3b718a3fd906",
                column: "ConcurrencyStamp",
                value: "0da50950-fee6-4cc5-927e-f489fd1cffd7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "976b252f-8101-47e4-9fd1-d9751274ffa3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "720be145-e488-4281-b3ef-cb0cc36c5118", "AQAAAAEAACcQAAAAEPDOYVKE3CW63kHu99QZGowc5BqxOB0jMCHqJKAq4WHlTl5vS1UNcy/lx/HLvdi8+w==" });

            migrationBuilder.InsertData(
                table: "TransportTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Троллейбус" },
                    { 2, "Автобус" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransportTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TransportTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f6e1271-329f-47cb-bd20-3b718a3fd906",
                column: "ConcurrencyStamp",
                value: "d983378e-1c65-46aa-a43a-37b853310853");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "976b252f-8101-47e4-9fd1-d9751274ffa3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "564cc43e-4fed-4fe6-a35a-ba0db4834086", "AQAAAAEAACcQAAAAEJQI+pLC4MH6GUDVyKmEgUI7bc5KKVMSm7zdnDV+gomLZJR+crQpp/iV9LDIbbwsoQ==" });
        }
    }
}
