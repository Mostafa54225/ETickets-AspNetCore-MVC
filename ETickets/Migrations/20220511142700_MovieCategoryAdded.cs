using Microsoft.EntityFrameworkCore.Migrations;

namespace ETickets.Migrations
{
    public partial class MovieCategoryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StarttDate",
                table: "Movies",
                newName: "StartDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Movies",
                newName: "StarttDate");
        }
    }
}
