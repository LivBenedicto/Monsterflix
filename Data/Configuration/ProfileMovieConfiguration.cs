// Configuração da tabela relacionamento entre Profile e Movie

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

            builder.HasKey(profileMovie => profileMovie.IdProfileMovie);

            builder.Property(profileMovie => profileMovie.IdProfile).IsRequired();

            builder.Property(profileMovie => profileMovie.IdMovie).IsRequired();

            builder.Property(profileMovie => profileMovie.StatusWatch).IsRequired();

            builder.HasOne(profileMovie => profileMovie.Profile).WithMany(profile => profile.Movie).HasForeignKey(profileMovie => profileMovie.IdProfile);
            
            builder.HasOne(profileMovie => profileMovie.Movie).WithMany().HasForeignKey(profileMovie => profileMovie.IdMovie);
        }
    }
}