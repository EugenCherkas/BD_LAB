using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport.Infrastructure.Migrations
{
    public partial class AddedSeedRanks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Ranks",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Техник" },
                    { 2, "Водитель" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ranks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ranks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f6e1271-329f-47cb-bd20-3b718a3fd906",
                column: "ConcurrencyStamp",
                value: "ed5f5320-27ed-4d96-a16b-0b0348ab56a4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "976b252f-8101-47e4-9fd1-d9751274ffa3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e46c4b8c-ce68-4198-a9d2-a5ec68618abf", "AQAAAAEAACcQAAAAEGtaBATyaUGS69hJsdp8dJruoab7g8sJ+WOp78vOcQUMsgTUXdb2N3vDh6kvjU/3aA==" });
        }
    }
}
