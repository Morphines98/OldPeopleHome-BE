using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeerPflege.Persistence.Migrations
{
    public partial class addHomeIdOnWallItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HomeId",
                table: "WallItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WallItems_HomeId",
                table: "WallItems",
                column: "HomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WallItems_Homes_HomeId",
                table: "WallItems",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WallItems_Homes_HomeId",
                table: "WallItems");

            migrationBuilder.DropIndex(
                name: "IX_WallItems_HomeId",
                table: "WallItems");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "WallItems");
        }
    }
}
