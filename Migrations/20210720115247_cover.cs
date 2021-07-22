using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCMusic.Migrations
{
    public partial class cover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlbumCover",
                table: "Album",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumCover",
                table: "Album");
        }
    }
}
