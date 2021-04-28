using Microsoft.EntityFrameworkCore;
using Monsterflix.Api.Data.Configuration;
using Monsterflix.Api.Models;

namespace Monsterflix.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileMovie> ProfileMovies { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileMovieConfiguration());
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
            modelBuilder.ApplyConfiguration(new MovieGenreConfiguration());
        }
    }
}