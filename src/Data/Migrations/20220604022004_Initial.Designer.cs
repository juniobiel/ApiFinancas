﻿// <auto-generated />
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
    [Migration("20220604022004_Initial")]
    partial class Initial
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

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Business.Models.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountReceiverId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AccountTransferReceiverId");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(200)")
                        .HasDefaultValue("Nova movimentação");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("TransactionId", "AccountId");

                    b.HasIndex("AccountId");

                    b.HasIndex("AccountReceiverId")
                        .IsUnique()
                        .HasFilter("[AccountTransferReceiverId] IS NOT NULL");

                    b.HasIndex("CategoryId");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("Business.Models.Transaction", b =>
                {
                    b.HasOne("Business.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .IsRequired();

                    b.HasOne("Business.Models.Account", "AccountReceiver")
                        .WithOne("TransferTransaction")
                        .HasForeignKey("Business.Models.Transaction", "AccountReceiverId");

                    b.HasOne("Business.Models.Category", "Category")
                        .WithMany("Transactions")
                        .HasForeignKey("CategoryId")
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("AccountReceiver");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Business.Models.Account", b =>
                {
                    b.Navigation("Transactions");

                    b.Navigation("TransferTransaction");
                });

            modelBuilder.Entity("Business.Models.Category", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
