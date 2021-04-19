using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadAPI.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Car",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "VehicleBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleModel_VehicleBrand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "VehicleBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    VehicleModelId = table.Column<int>(type: "int", nullable: true),
                    RegistrationMark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicle_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicle_VehicleModel_VehicleModelId",
                        column: x => x.VehicleModelId,
                        principalTable: "VehicleModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_OwnerId",
                table: "Vehicle",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_VehicleModelId",
                table: "Vehicle",
                column: "VehicleModelId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModel_BrandId",
                table: "VehicleModel",
                column: "BrandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "VehicleModel");

            migrationBuilder.DropTable(
                name: "VehicleBrand");

            migrationBuilder.AddColumn<string>(
                name: "Car",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
