// <auto-generated />
using System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(FinanceDbContext))]
    [Migration("20220706203857_Stock tables added")]
    partial class Stocktablesadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Business.Models.Account", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AccountBalance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId_Created")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId_Updated")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts", (string)null);
                });

            modelBuilder.Entity("Business.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId_Created")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId_Updated")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Business.Models.Stock", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockId"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InitialDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("Inital_Date");

                    b.Property<string>("InitialValue")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Initial_Value");

                    b.Property<string>("StockName")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("varchar(7)")
                        .HasColumnName("Stock_Name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId_Created")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId_Updated")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StockId");

                    b.ToTable("Stocks", (string)null);
                });

            modelBuilder.Entity("Business.Models.StockPurchase", b =>
                {
                    b.Property<Guid>("StockPurchaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PurchaseTaxes")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<int>("StockQtd")
                        .HasColumnType("int");

                    b.Property<decimal>("StockValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId_Created")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId_Updated")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StockPurchaseId");

                    b.HasIndex("StockId");

                    b.ToTable("Stock_Purchases", (string)null);
                });

            modelBuilder.Entity("Business.Models.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountReceiverId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AccountReceiverId");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(200)")
                        .HasDefaultValue("Nova movimentação");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId_Created")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId_Updated")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("TransactionId", "AccountId");

                    b.HasIndex("AccountId");

                    b.HasIndex("AccountReceiverId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("Business.Models.StockPurchase", b =>
                {
                    b.HasOne("Business.Models.Stock", "Stock")
                        .WithMany("StockPurchases")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Business.Models.Transaction", b =>
                {
                    b.HasOne("Business.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Business.Models.Account", "AccountReceiver")
                        .WithMany("ReceivedTransactions")
                        .HasForeignKey("AccountReceiverId");

                    b.HasOne("Business.Models.Category", "Category")
                        .WithMany("Transactions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Account");

                    b.Navigation("AccountReceiver");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Business.Models.Account", b =>
                {
                    b.Navigation("ReceivedTransactions");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Business.Models.Category", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Business.Models.Stock", b =>
                {
                    b.Navigation("StockPurchases");
                });
#pragma warning restore 612, 618
        }
    }
}
