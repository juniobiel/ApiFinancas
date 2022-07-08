using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdatingStockPurchasetablenametoStockTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Purchases_Stocks_StockId",
                table: "Stock_Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock_Purchases",
                table: "Stock_Purchases");

            migrationBuilder.RenameTable(
                name: "Stock_Purchases",
                newName: "Stock_Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_Purchases_StockId",
                table: "Stock_Transactions",
                newName: "IX_Stock_Transactions_StockId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock_Transactions",
                table: "Stock_Transactions",
                column: "StockTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Transactions_Stocks_StockId",
                table: "Stock_Transactions",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Transactions_Stocks_StockId",
                table: "Stock_Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock_Transactions",
                table: "Stock_Transactions");

            migrationBuilder.RenameTable(
                name: "Stock_Transactions",
                newName: "Stock_Purchases");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_Transactions_StockId",
                table: "Stock_Purchases",
                newName: "IX_Stock_Purchases_StockId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock_Purchases",
                table: "Stock_Purchases",
                column: "StockTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Purchases_Stocks_StockId",
                table: "Stock_Purchases",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
