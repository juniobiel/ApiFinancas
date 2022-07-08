using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdatingStockPurchasetoStockTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PurchaseTaxes",
                table: "Stock_Purchases",
                newName: "TransactionTaxes");

            migrationBuilder.RenameColumn(
                name: "PurchaseDate",
                table: "Stock_Purchases",
                newName: "TransactionDate");

            migrationBuilder.RenameColumn(
                name: "StockPurchaseId",
                table: "Stock_Purchases",
                newName: "StockTransactionId");

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Stock_Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Stock_Purchases");

            migrationBuilder.RenameColumn(
                name: "TransactionTaxes",
                table: "Stock_Purchases",
                newName: "PurchaseTaxes");

            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                table: "Stock_Purchases",
                newName: "PurchaseDate");

            migrationBuilder.RenameColumn(
                name: "StockTransactionId",
                table: "Stock_Purchases",
                newName: "StockPurchaseId");
        }
    }
}
