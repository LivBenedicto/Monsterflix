using Microsoft.EntityFrameworkCore;
using Monsterflix.Api.Models;

namespace Monsterflix.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ProfileMovie> ProfileMovies { get; set; }
    }
}