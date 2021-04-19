using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadAPI.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Events_AuthorId",
                table: "Events",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_AuthorId",
                table: "Events",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_AuthorId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_AuthorId",
                table: "Events");
        }
    }
}
