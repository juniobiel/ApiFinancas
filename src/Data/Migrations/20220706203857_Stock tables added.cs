using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Stocktablesadded : Migration
    {
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.RenameColumn(
                name: "TransactionUpdatedByUserId",
                table: "Transactions",
                newName: "UserId_Updated");

            migrationBuilder.RenameColumn(
                name: "TransactionCreatedByUserId",
                table: "Transactions",
                newName: "UserId_Created");

            migrationBuilder.RenameColumn(
                name: "CategoryUpdatedByUserId",
                table: "Categories",
                newName: "UserId_Updated");

            migrationBuilder.RenameColumn(
                name: "CategoryCreatedByUserId",
                table: "Categories",
                newName: "UserId_Created");

            migrationBuilder.RenameColumn(
                name: "AccountUpdatedByUserId",
                table: "Accounts",
                newName: "UserId_Updated");

            migrationBuilder.RenameColumn(
                name: "AccountCreatedByUserId",
                table: "Accounts",
                newName: "UserId_Created");

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stock_Name = table.Column<string>(type: "varchar(7)", maxLength: 7, nullable: false),
                    Initial_Value = table.Column<string>(type: "varchar(100)", nullable: false),
                    Inital_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId_Created = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId_Updated = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockId);
                });

            migrationBuilder.CreateTable(
                name: "Stock_Purchases",
                columns: table => new
                {
                    StockPurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    StockValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQtd = table.Column<int>(type: "int", nullable: false),
                    PurchaseTaxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId_Created = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId_Updated = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_Purchases", x => x.StockPurchaseId);
                    table.ForeignKey(
                        name: "FK_Stock_Purchases_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Purchases_StockId",
                table: "Stock_Purchases",
                column: "StockId");
        }

        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.DropTable(
                name: "Stock_Purchases");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.RenameColumn(
                name: "UserId_Updated",
                table: "Transactions",
                newName: "TransactionUpdatedByUserId");

            migrationBuilder.RenameColumn(
                name: "UserId_Created",
                table: "Transactions",
                newName: "TransactionCreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "UserId_Updated",
                table: "Categories",
                newName: "CategoryUpdatedByUserId");

            migrationBuilder.RenameColumn(
                name: "UserId_Created",
                table: "Categories",
                newName: "CategoryCreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "UserId_Updated",
                table: "Accounts",
                newName: "AccountUpdatedByUserId");

            migrationBuilder.RenameColumn(
                name: "UserId_Created",
                table: "Accounts",
                newName: "AccountCreatedByUserId");
        }
    }
}
