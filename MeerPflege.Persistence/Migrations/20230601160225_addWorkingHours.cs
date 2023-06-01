using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeerPflege.Persistence.Migrations
{
    public partial class addWorkingHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkingHoursForDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HomeId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHoursForDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingHoursForDays_Homes_HomeId",
                        column: x => x.HomeId,
                        principalTable: "Homes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkingIntervals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkingHoursForDayId = table.Column<int>(type: "int", nullable: false),
                    StartHours = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EndHours = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingIntervals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingIntervals_WorkingHoursForDays_WorkingHoursForDayId",
                        column: x => x.WorkingHoursForDayId,
                        principalTable: "WorkingHoursForDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHoursForDays_HomeId",
                table: "WorkingHoursForDays",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingIntervals_WorkingHoursForDayId",
                table: "WorkingIntervals",
                column: "WorkingHoursForDayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingIntervals");

            migrationBuilder.DropTable(
                name: "WorkingHoursForDays");
        }
    }
}
