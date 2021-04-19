using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadAPI.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Users_OwnerId",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleModel_VehicleModelId",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Vehicles");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_VehicleModelId",
                table: "Vehicles",
                newName: "IX_Vehicles_VehicleModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_OwnerId",
                table: "Vehicles",
                newName: "IX_Vehicles_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_OwnerId",
                table: "Vehicles",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleModel_VehicleModelId",
                table: "Vehicles",
                column: "VehicleModelId",
                principalTable: "VehicleModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_OwnerId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleModel_VehicleModelId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehicle");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_VehicleModelId",
                table: "Vehicle",
                newName: "IX_Vehicle_VehicleModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicle",
                newName: "IX_Vehicle_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Users_OwnerId",
                table: "Vehicle",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleModel_VehicleModelId",
                table: "Vehicle",
                column: "VehicleModelId",
                principalTable: "VehicleModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
