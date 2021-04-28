using System.Threading.Tasks;
using Monsterflix.Api.Models;

namespace Monsterflix.Api.Repositories.Contracts
{
    public interface IProfileMovieRepository
    {
        Task<ProfileMovie> AddingMovieWatchlist(ProfileMovie profileMovie);
        Task<ProfileMovie> SearchProfileMovie(int idProfile, int idMovieService);
        Task<ProfileMovie> Update(ProfileMovie profileMovie);
    }
}