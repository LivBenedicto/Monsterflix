using System.Threading.Tasks;

namespace Monsterflix.Api.Services.Contracts
{
    public interface ITheMovieDBService
    {
        Task<int> SearchMovie(string keyword);
    }
}