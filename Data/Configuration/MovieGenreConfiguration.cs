// Configuração da tabela relacionamento entre Movie e Genre

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monsterflix.Api.Models;

namespace Monsterflix.Api.Data.Configuration
{
    public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
    {
        public void Configure(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenres");

            builder.HasKey(movieGenre => movieGenre.IdMovieGenre);

            builder.Property(movieGenre => movieGenre.IdMovie);

            builder.Property(genre => genre.IdGenreService);

            builder.HasOne(movieGenre => movieGenre.Movie).WithMany(movie => movie.Genre).HasForeignKey(movieGenre => movieGenre.IdMovie);
        }
    }
}