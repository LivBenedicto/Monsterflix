using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monsterflix.Api.Models;

namespace Monsterflix.Api.Data.Configuration
{
    public class ProfileMovieConfiguration : IEntityTypeConfiguration<ProfileMovie>
    {
        public void Configure(EntityTypeBuilder<ProfileMovie> builder)
        {
            builder.ToTable("ProfileMovies");

            builder.HasNoKey();

            builder.Property(profileMovie => profileMovie.IdProfile).IsRequired();

            builder.Property(profileMovie => profileMovie.IdMovie).IsRequired();

            builder.Property(profileMovie => profileMovie.StatusWatch).IsRequired();

            builder.HasOne(profileMovie => profileMovie.Profile).WithMany();
            
            builder.HasOne(profileMovie => profileMovie.Movie).WithMany();
        }
    }
}