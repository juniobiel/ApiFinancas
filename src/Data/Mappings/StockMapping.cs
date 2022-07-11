using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class StockMapping : IEntityTypeConfiguration<Stock>
    {
        public void Configure( EntityTypeBuilder<Stock> builder )
        {
            builder.HasKey(s => s.StockId);

            builder.Property(s => s.StockId)
                .ValueGeneratedOnAdd();

            builder.Property(s => s.StockTicker)
                .HasMaxLength(7)
                .HasColumnType("varchar(7)")
                .IsRequired();

            builder.Property(s => s.InitialPrice)
                .IsRequired();

            builder.Property(s => s.InitialDate)
                .IsRequired();

            builder.Property(s => s.StockQt)
                .IsRequired();

            builder.HasMany(s => s.StockPurchases)
                .WithOne(s => s.Stock);


            builder.ToTable("Stocks");

        }
    }
}
