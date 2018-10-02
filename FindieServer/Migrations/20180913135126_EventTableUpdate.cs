using Microsoft.EntityFrameworkCore.Migrations;

namespace FindieServer.Migrations
{
    public partial class EventTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventName",
                table: "Events",
                nullable: true,
                maxLength:200);

            migrationBuilder.AddColumn<string>(
                name: "ImageUri",
                table: "Events",
                nullable: true,
                maxLength:700);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventName",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ImageUri",
                table: "Events");
        }
    }
}
