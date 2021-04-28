using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monsterflix.Api.Data;
using Monsterflix.Api.Enum;
using Monsterflix.Api.Models;
using Monsterflix.Api.Repositories.Contracts;

namespace Monsterflix.Api.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _context;

        public MovieRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Movie> SearchMovieDB(int idMovieService)
        {
            return await _context.Set<Movie>().Where(movie => movie.IdMovieService == idMovieService).FirstOrDefaultAsync();
        }

        public async Task<Movie> AddingNewMovieDB(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<IList<Movie>> GetListMoviesByStatus(int idProfile, EStatusMovie statusMovie)
        {
            return await _context.ProfileMovies.Where(profileMovie => profileMovie.IdProfile == idProfile && profileMovie.StatusWatch == statusMovie).Select(profileMovie => profileMovie.Movie).ToListAsync();
        }
    }
}