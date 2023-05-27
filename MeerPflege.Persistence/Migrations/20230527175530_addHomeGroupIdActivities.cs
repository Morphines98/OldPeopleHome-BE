using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeerPflege.Persistence.Migrations
{
    public partial class addHomeGroupIdActivities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HomeId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_HomeId",
                table: "Activities",
                column: "HomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Homes_HomeId",
                table: "Activities",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Homes_HomeId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_HomeId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "Activities");
        }
    }
}
