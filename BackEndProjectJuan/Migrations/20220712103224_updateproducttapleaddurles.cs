using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndProjectJuan.Migrations
{
    public partial class updateproducttapleaddurles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookUrl",
                table: "Products",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GooglePlusUrl",
                table: "Products",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PinterestUrl",
                table: "Products",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TwitterUrl",
                table: "Products",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GooglePlusUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PinterestUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TwitterUrl",
                table: "Products");
        }
    }
}
