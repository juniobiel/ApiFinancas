using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class StockPurchaseMapping : IEntityTypeConfiguration<StockPurchase>
    {
        public void Configure( EntityTypeBuilder<StockPurchase> builder )
        {
            builder.HasKey(s => s.StockPurchaseId);

            builder.Property(s => s.StockPurchaseId)
                .ValueGeneratedOnAdd();

            builder.Property(s => s.StockTicker)
                .HasMaxLength(7)
                .HasColumnType("varchar(7)");

            builder.Property(s => s.PurchaseTaxes)
                .IsRequired();

            builder.Property(s => s.StockQt)
                .IsRequired();

            builder.Property(s => s.StockPrice)
                .IsRequired();

            builder.Property(s => s.PurchaseDate)
                .IsRequired();

            builder.HasOne(s => s.Stock)
                .WithMany(s => s.StockPurchases)
                .HasForeignKey(s => s.StockId);

            builder.ToTable("Stock_Purchases");
        }
    }
}
