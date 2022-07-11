using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class StockTransactionMapping : IEntityTypeConfiguration<StockTransaction>
    {
        public void Configure( EntityTypeBuilder<StockTransaction> builder )
        {
            builder.HasKey(s => s.StockTransactionId);

            builder.Property(s => s.StockTransactionId)
                .ValueGeneratedOnAdd();

            builder.Property(s => s.StockTicker)
                .HasMaxLength(7)
                .HasColumnType("varchar(7)");

            builder.Property(s => s.TransactionTaxes)
                .IsRequired();

            builder.Property(s => s.StockQt)
                .IsRequired();

            builder.Property(s => s.StockPrice)
                .IsRequired();

            builder.Property(s => s.TransactionDate)
                .IsRequired();

            builder.Property(s => s.TransactionType)
                .IsRequired();

            builder.Property(s => s.Total)
                .IsRequired();

            builder.HasOne(s => s.Stock)
                .WithMany(s => s.StockTransactions)
                .HasForeignKey(s => s.StockId);

            builder.ToTable("Stock_Transactions");
        }
    }
}
