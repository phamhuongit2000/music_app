using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveMusic.Migrations
{
    public partial class EditHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Historys",
                newName: "DateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Historys",
                newName: "DateOfBirth");
        }
    }
}
