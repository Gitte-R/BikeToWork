using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeToWork.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    team = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bikeClass = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BikeRides",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    participantId = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeRides", x => x.id);
                    table.ForeignKey(
                        name: "FK_BikeRides_Participants_participantId",
                        column: x => x.participantId,
                        principalTable: "Participants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BikeRides_participantId",
                table: "BikeRides",
                column: "participantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BikeRides");

            migrationBuilder.DropTable(
                name: "Participants");
        }
    }
}
