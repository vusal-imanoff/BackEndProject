using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndProjectJuan.Migrations
{
    public partial class AddBlogTableAddIsdeletedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubDescription",
                table: "Blogs",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blogs",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MainDescription",
                table: "Blogs",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Blogs",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Blacknote",
                table: "Blogs",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Blogs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Blogs");

            migrationBuilder.AlterColumn<string>(
                name: "SubDescription",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "MainDescription",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Blacknote",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);
        }
    }
}
