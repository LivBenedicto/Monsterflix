using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monsterflix.Api.Models;

namespace Monsterflix.Api.Data.Configuration
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profiles");
            
            builder.HasKey(profile => profile.IdProfile);

            builder.Property(profile => profile.Username).HasMaxLength(20).IsRequired();

            builder.HasOne(profile => profile.Account).WithMany(account => account.Profile).HasForeignKey(profile => profile.IdAccount).HasPrincipalKey(account => account.IdAccount);
        }
    }
}