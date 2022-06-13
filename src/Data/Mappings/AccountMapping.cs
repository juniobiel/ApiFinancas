using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure( EntityTypeBuilder<Account> builder )
        {
            builder.HasKey(a => a.AccountId);

            builder.Property(p => p.AccountName)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.AccountBalance)
                .HasDefaultValue(0);

            builder.HasMany(t => t.Transactions)
                .WithOne(a => a.Account)
                .HasForeignKey(a => a.AccountId);


            builder.ToTable("Accounts");
        }
    }
}
