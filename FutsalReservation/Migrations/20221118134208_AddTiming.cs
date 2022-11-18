using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutsalReservation.Migrations
{
    public partial class AddTiming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Timing",
                columns: table => new
                {
                    TimingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    endTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timing", x => x.TimingId);
                    table.ForeignKey(
                        name: "FK_Timing_Court_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Court",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Timing_CourtId",
                table: "Timing",
                column: "CourtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timing");
        }
    }
}
