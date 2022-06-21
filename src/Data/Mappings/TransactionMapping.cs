using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure( EntityTypeBuilder<Transaction> builder )
        {
            builder.HasKey(pk => new { pk.TransactionId, pk.AccountId } );

            builder.Property(p => p.TransactionType)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(c => c.CategoryId);

            builder.Property(p => p.Value)
                .IsRequired();

            builder.Property(p => p.TransactionDate)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnType("varchar(200)")
                .HasDefaultValue("Nova movimentação");

            builder.HasOne(a => a.Account)
                .WithMany(t => t.Transactions)
                .HasForeignKey(a => a.AccountId);

            builder.Property(p => p.AccountReceiverId)
                .HasColumnName("AccountReceiverId");

            builder.HasOne(a => a.AccountReceiver)
                .WithMany(t => t.ReceivedTransactions)
                .HasForeignKey(a => a.AccountReceiverId)
                .IsRequired(false);

            builder.ToTable("Transactions");
        }
    }
}
