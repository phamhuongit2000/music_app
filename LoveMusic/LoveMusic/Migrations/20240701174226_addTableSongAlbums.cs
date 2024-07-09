using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveMusic.Migrations
{
    public partial class addTableSongAlbums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SongAlbums",
                columns: table => new
                {
                    SongAlbumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongId = table.Column<int>(type: "int", nullable: false),
                    AlbumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongAlbums", x => x.SongAlbumId);
                    table.ForeignKey(
                        name: "FK_SongAlbums_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "AlbumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongAlbums_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "SongId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongAlbums_AlbumId",
                table: "SongAlbums",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_SongAlbums_SongId",
                table: "SongAlbums",
                column: "SongId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongAlbums");
        }
    }
}
