using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeerPflege.Persistence.Migrations
{
    public partial class addTitlwOnWall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "WallItems",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "WallItems");
        }
    }
}
