using Microsoft.EntityFrameworkCore.Migrations;

namespace Warehouse.Migrations
{
    public partial class seedtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Containers",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "T1" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "ContainerId", "Name", "Number" },
                values: new object[] { 1, 1, "Csavarhúzó", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Containers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
