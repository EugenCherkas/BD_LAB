using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport.Infrastructure.Migrations
{
    public partial class AddedRanksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RankId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RankId",
                table: "Employees",
                column: "RankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Ranks_RankId",
                table: "Employees",
                column: "RankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Ranks_RankId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Ranks");

            migrationBuilder.DropIndex(
                name: "IX_Employees_RankId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RankId",
                table: "Employees");

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
        }
    }
}
