// Controle de manipulação de filmes

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monsterflix.Api.Enum;
using Monsterflix.Api.Models;
using Monsterflix.Api.Repositories.Contracts;
using Monsterflix.Api.Services.Contracts;

namespace Monsterflix.Api.Controllers
{
    public class MovieController
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ITheMovieDBService _theMovieDBService;

        public MovieController(IMovieRepository movieRepository, ITheMovieDBService theMovieDBService)
        {
            _movieRepository = movieRepository;
            _theMovieDBService = theMovieDBService;
        }

        // Pesquisar filme por palavra-chave no Serviço da The Movie DB
        public async Task<int> GetSearchMovieByKeyword(string keyword)
        {
            return await _theMovieDBService.SearchMovie(keyword);
        }

        // Retornar lista de filmes por status
        public async Task<IList<Movie>> GetSearchMovieById(int idProfile, EStatusMovie statusMovie)
        {
            return await _movieRepository.GetListMoviesByStatus(idProfile, statusMovie);
        }

        // Alterar o status para "já assistido"
        public async Task UpdateStatusMovie(int idProfile, int idMovie)
        {
            await _movieRepository.UpdateMovieStatus(idProfile, idMovie);
        }
    }
}