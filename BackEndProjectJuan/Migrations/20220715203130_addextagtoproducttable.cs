using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndProjectJuan.Migrations
{
    public partial class addextagtoproducttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Extax",
                table: "Products",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extax",
                table: "Products");
        }
    }
}
