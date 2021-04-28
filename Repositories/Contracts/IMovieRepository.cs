using System.Collections.Generic;
using System.Threading.Tasks;
using Monsterflix.Api.Enum;
using Monsterflix.Api.Models;

namespace Monsterflix.Api.Repositories.Contracts
{
    public interface IMovieRepository
    {
        Task<Movie> SearchMovieDB(int idMovieService);
        Task<Movie> AddingNewMovieDB(Movie movie);
        Task<IList<Movie>> GetListMoviesByStatus(int idProfile, EStatusMovie statusMovie);
    }
}