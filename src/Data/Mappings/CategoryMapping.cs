using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure( EntityTypeBuilder<Category> builder )
        {
            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.CategoryId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.CategoryName)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.TransactionType)
                .IsRequired();

            builder.ToTable("Categories");
        }
    }
}
