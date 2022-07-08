using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Stockinitialversionupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stock_Name",
                table: "Stocks",
                newName: "StockTicker");

            migrationBuilder.RenameColumn(
                name: "StockQtd",
                table: "Stock_Purchases",
                newName: "StockQt");

            migrationBuilder.AlterColumn<decimal>(
                name: "InitialPrice",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AddColumn<int>(
                name: "StockQt",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StockTicker",
                table: "Stock_Purchases",
                type: "varchar(7)",
                maxLength: 7,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockQt",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "StockTicker",
                table: "Stock_Purchases");

            migrationBuilder.RenameColumn(
                name: "StockTicker",
                table: "Stocks",
                newName: "Stock_Name");

            migrationBuilder.RenameColumn(
                name: "StockQt",
                table: "Stock_Purchases",
                newName: "StockQtd");

            migrationBuilder.AlterColumn<string>(
                name: "InitialPrice",
                table: "Stocks",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
