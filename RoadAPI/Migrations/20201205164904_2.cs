using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace RoadAPI.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Events");

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "Events",
                type: "geography",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Events");

            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "Events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Lng",
                table: "Events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
