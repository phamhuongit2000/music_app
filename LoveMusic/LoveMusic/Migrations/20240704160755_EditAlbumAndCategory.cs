using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveMusic.Migrations
{
    public partial class EditAlbumAndCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Albums");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categorys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Categorys",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Categorys");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
