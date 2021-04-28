// Controle de manipulação de filmes

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monsterflix.Api.Enum;
using Monsterflix.Api.Models;
using Monsterflix.Api.Models.Service;
using Monsterflix.Api.Repositories.Contracts;
using Monsterflix.Api.Services.Contracts;

namespace Monsterflix.Api.Controllers
{
    [Route("v1/movies")]
    public class MovieController
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IProfileMovieRepository _profileMovieRepository;
        private readonly ITheMovieDBService _theMovieDBService;

        public MovieController(IMovieRepository movieRepository, IProfileMovieRepository profileMovieRepository, ITheMovieDBService theMovieDBService)
        {
            _movieRepository = movieRepository;
            _profileMovieRepository = profileMovieRepository;
            _theMovieDBService = theMovieDBService;
        }

        // Pesquisar filme(s) por palavra-chave no Serviço da The Movie DB
        [HttpGet]
        public async Task<MovieList> GetSearchMoviesByKeyword([FromQuery] string keyword)
        {
            return await _theMovieDBService.SearchMovie(keyword);
        }

        // Retornar lista de filmes por status
        [HttpGet("{idProfile}/ListByStatus")]
        public async Task<IList<Movie>> GetSearchMovieByStatus(int idProfile, [FromQuery] EStatusMovie statusMovie)
        {
            return await _movieRepository.GetListMoviesByStatus(idProfile, statusMovie);
        }

        // Retornar detalhe do filme por ID
        [HttpGet("{idProfile}/{idMovieService}")]
        public async Task<Movie> GetSearchMovieById(int idProfile, int idMovieService)
        {
            return await _movieRepository.SearchMovieDB(idMovieService);
        }

        // Alterar o status para "já assistido"
        [HttpPost("{idProfile}/{idMovieService}/SetWatched")]
        public async Task UpdateStatusMovieToWatched(int idProfile, int idMovieService)
        {
            ProfileMovie profileMovie = await _profileMovieRepository.SearchProfileMovie(idProfile, idMovieService);
            profileMovie.StatusWatch = EStatusMovie.Watched;

            await _profileMovieRepository.Update(profileMovie);
        }
    }
}