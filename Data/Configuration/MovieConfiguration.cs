using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monsterflix.Api.Models;

namespace Monsterflix.Api.Data.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");

            builder.HasKey(movie => movie.IdMovie);

            builder.Property(genre => genre.IdMovieService);
        }
    }
}