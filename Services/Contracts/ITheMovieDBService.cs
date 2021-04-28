using System.Threading.Tasks;
using Monsterflix.Api.Models.Service;

namespace Monsterflix.Api.Services.Contracts
{
    public interface ITheMovieDBService
    {
        Task<MovieList> SearchMovie(string keyword);
        Task<MovieDetail> SearchMovieById(int idMovieService);
    }
}