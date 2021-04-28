using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monsterflix.Api.Data;
using Monsterflix.Api.Models;
using Monsterflix.Api.Repositories.Contracts;

namespace Monsterflix.Api.Repositories
{
    public class ProfileMovieRepository : IProfileMovieRepository
    {
        private readonly DataContext _context;

        public ProfileMovieRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ProfileMovie> AddingMovieWatchlist(ProfileMovie profileMovie)
        {
            _context.ProfileMovies.Add(profileMovie);
            await _context.SaveChangesAsync();

            return profileMovie;
        }

        public async Task<ProfileMovie> SearchProfileMovie(int idProfile, int idMovieService)
        {
            return await _context.ProfileMovies.Where(profileMovie => profileMovie.IdProfile == idProfile && profileMovie.Movie.IdMovieService == idMovieService).FirstOrDefaultAsync();
        }

        public async Task<ProfileMovie> Update(ProfileMovie profileMovie)
        {
            _context.ProfileMovies.Update(profileMovie);
            await _context.SaveChangesAsync();

            return profileMovie;
        }
    }
}