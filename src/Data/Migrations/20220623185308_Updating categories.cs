using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Updatingcategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountUpdatedByUserId",
                table: "Categories",
                newName: "CategoryUpdatedByUserId");

            migrationBuilder.RenameColumn(
                name: "AccountCreatedByUserId",
                table: "Categories",
                newName: "CategoryCreatedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryUpdatedByUserId",
                table: "Categories",
                newName: "AccountUpdatedByUserId");

            migrationBuilder.RenameColumn(
                name: "CategoryCreatedByUserId",
                table: "Categories",
                newName: "AccountCreatedByUserId");
        }
    }
}
