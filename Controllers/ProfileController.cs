// Classe que controla acesso perfil

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monsterflix.Api.Enum;
using Monsterflix.Api.Models;
using Monsterflix.Api.Models.Request;
using Monsterflix.Api.Models.Service;
using Monsterflix.Api.Repositories.Contracts;
using Monsterflix.Api.Services.Contracts;

namespace Monsterflix.Api.Controllers
{
    [Route("v1/profiles")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IProfileMovieRepository _profileMovieRepository;
        private readonly ITheMovieDBService _theMovieDBService;

        public ProfileController(IProfileRepository profileRepository, IMovieRepository movieRepository, IProfileMovieRepository profileMovieRepository, ITheMovieDBService theMovieDBService)
        {
            _profileRepository = profileRepository;
            _movieRepository = movieRepository;
            _profileMovieRepository = profileMovieRepository;
            _theMovieDBService = theMovieDBService;
        }

        [HttpPost]
        public async Task<Profile> PostCreateNewProfile([FromBody] CreateProfileRequest createProfileRequest)
        {
            Profile profile = new Profile();
            profile.IdAccount = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "IdAccount").Value);
            profile.Username = createProfileRequest.Username;

            return await _profileRepository.CreateNewProfile(profile);
        }

        [HttpGet("{idProfile}")]
        public async Task<Profile> GetProfileById(int idProfile)
        {
            return await _profileRepository.GetProfileById(idProfile);
        }

        [HttpGet]
        public async Task<IList<Profile>> GetListProfiles()
        {
            return await _profileRepository.GetListProfiles();
        }

        // Adicionar filme na lista de "para assistir"
        [HttpPost("{idProfile}/WatchList/{idMovieService}")]
        public async Task PostAddingMovieWatchlist(int idProfile, int idMovieService)
        {
            // Procura pelo filme
            Movie movieResult = await _movieRepository.SearchMovieDB(idMovieService);

            // Adiciona na base, caso não exista
            if (movieResult == null)
                movieResult = await AddingNewMovieDB(idMovieService);

            // Cria um novo relacionamento no ProfileMovie
            ProfileMovie profileMovie = new ProfileMovie();
            profileMovie.IdProfile = idProfile;
            profileMovie.IdMovie = movieResult.IdMovie;
            profileMovie.StatusWatch = EStatusMovie.Watch;

            await _profileMovieRepository.AddingMovieWatchlist(profileMovie);
        }

        // Método complementar ao "PostAddingMovieWatchlist"
        private async Task<Movie> AddingNewMovieDB(int idMovieService)
        {
            Movie movieResult = new Movie();
            movieResult.IdMovieService = idMovieService;
            movieResult.Genre = new List<MovieGenre>();

            // Adicionar os gêneros do filme
            MovieDetail movieGenreDetail = await _theMovieDBService.SearchMovieById(idMovieService);
            foreach (Models.Service.Genre genre in movieGenreDetail.genres)
            {
                MovieGenre movieGenre = new MovieGenre();
                movieGenre.IdGenreService = genre.id;

                movieResult.Genre.Add(movieGenre);
            }

            await _movieRepository.AddingNewMovieDB(movieResult);

            return movieResult;
        }

        [HttpDelete("{idProfile}")]
        public async Task<Profile> DeleteProfile(int idProfile)
        {
            return await _profileRepository.DeleteProfile(idProfile);
        }
    }
}