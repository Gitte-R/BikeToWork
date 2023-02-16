using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeToWork.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BikeRides_Participants_participantId",
                table: "BikeRides");

            migrationBuilder.RenameColumn(
                name: "team",
                table: "Participants",
                newName: "Team");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Participants",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Participants",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "bikeClass",
                table: "Participants",
                newName: "BikeClass");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Participants",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "participantId",
                table: "BikeRides",
                newName: "ParticipantId");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "BikeRides",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "BikeRides",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_BikeRides_participantId",
                table: "BikeRides",
                newName: "IX_BikeRides_ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_BikeRides_Participants_ParticipantId",
                table: "BikeRides",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BikeRides_Participants_ParticipantId",
                table: "BikeRides");

            migrationBuilder.RenameColumn(
                name: "Team",
                table: "Participants",
                newName: "team");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Participants",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Participants",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "BikeClass",
                table: "Participants",
                newName: "bikeClass");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Participants",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                table: "BikeRides",
                newName: "participantId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "BikeRides",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BikeRides",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_BikeRides_ParticipantId",
                table: "BikeRides",
                newName: "IX_BikeRides_participantId");

            migrationBuilder.AddForeignKey(
                name: "FK_BikeRides_Participants_participantId",
                table: "BikeRides",
                column: "participantId",
                principalTable: "Participants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
