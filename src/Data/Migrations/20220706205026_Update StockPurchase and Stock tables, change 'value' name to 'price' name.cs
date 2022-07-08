using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateStockPurchaseandStocktableschangevaluenametopricename : Migration
    {
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.RenameColumn(
                name: "Inital_Date",
                table: "Stocks",
                newName: "InitialDate");

            migrationBuilder.RenameColumn(
                name: "Initial_Value",
                table: "Stocks",
                newName: "InitialPrice");

            migrationBuilder.RenameColumn(
                name: "StockValue",
                table: "Stock_Purchases",
                newName: "StockPrice");
        }

        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.RenameColumn(
                name: "InitialDate",
                table: "Stocks",
                newName: "Inital_Date");

            migrationBuilder.RenameColumn(
                name: "InitialPrice",
                table: "Stocks",
                newName: "Initial_Value");

            migrationBuilder.RenameColumn(
                name: "StockPrice",
                table: "Stock_Purchases",
                newName: "StockValue");
        }
    }
}
