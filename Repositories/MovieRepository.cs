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

        public async Task<IList<Movie>> GetListMoviesByStatus(int idProfile, EStatusMovie statusMovie)
        {
            return await _context.ProfileMovies.Where(profileMovie => profileMovie.IdProfile == idProfile && profileMovie.StatusWatch == statusMovie).Select(profileMovie => profileMovie.Movie).ToListAsync();
        }

        public async Task<ProfileMovie> UpdateMovieStatus(int idProfile, int idMovie)
        {
            ProfileMovie profileMovie = await _context.ProfileMovies.Where(profileMovie => profileMovie.IdProfile == idProfile && profileMovie.IdMovie == idMovie).FirstOrDefaultAsync();
            profileMovie.StatusWatch = EStatusMovie.Watched;
            _context.ProfileMovies.Update(profileMovie);
            
            await _context.SaveChangesAsync();
            
            return profileMovie;
        }
    }
}