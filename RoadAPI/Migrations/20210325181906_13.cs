using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadAPI.Migrations
{
    public partial class _13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleModels_VehicleModelId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleModelId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleModels_VehicleModelId",
                table: "Vehicles",
                column: "VehicleModelId",
                principalTable: "VehicleModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleModels_VehicleModelId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleModelId",
                table: "Vehicles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleModels_VehicleModelId",
                table: "Vehicles",
                column: "VehicleModelId",
                principalTable: "VehicleModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
