using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monsterflix.Api.Models;

namespace Monsterflix.Api.Data.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(account => account.IdAccount);

            builder.Property(account => account.Email).IsRequired();
            builder.HasIndex(account => account.Email).IsUnique();

            builder.Property(account => account.Password).HasMaxLength(20).IsRequired();

            builder.Property(account => account.Username).HasMaxLength(20).IsRequired();

            builder.Property(account => account.Birthday).HasMaxLength(11).IsRequired();

            builder.HasMany(account => account.Profile);
        }
    }
}