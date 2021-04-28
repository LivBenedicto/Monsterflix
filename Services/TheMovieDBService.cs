// Integração com API The Movie DB

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Monsterflix.Api.Configurations;
using Monsterflix.Api.Models.Service;
using Monsterflix.Api.Services.Contracts;

namespace Monsterflix.Api.Services
{
    public class TheMovieDBService : ITheMovieDBService
    {
        private readonly HttpClient _httpClient;
        private readonly string _key;

        public TheMovieDBService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            _key = AppSettingsProvider.Settings["ApiKeyTMDB"];
        }

        // Criando get generico para requisição
        // TModel é transformado em classe para ser inicializado
        private async Task<TModel> _getServiceUrl<TModel>(string path) where TModel : class
        {
            path += (path.Contains('?') ? "&" : "?") + $"api_key={_key}";

            HttpResponseMessage response = await _httpClient.GetAsync(path);
            TModel result = null;
            
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                
                if (!string.IsNullOrEmpty(content))
                    result = JsonSerializer.Deserialize<TModel>(content);
            }
            
            return result;
        }

        // Requisição para pesquisar todos os filmes correspondentes a palavra-chave - lista
        public async Task<MovieList> SearchMovie(string keyword)
        {
            MovieList movieList = await _getServiceUrl<MovieList>($"search/movie?query={keyword}&include_adult=false");
            return movieList;
        }

        // Requisição para pesquisar detalhes do filme por ID TMDB - único
        public async Task<MovieDetail> SearchMovieById(int idMovieService)
        {
            MovieDetail movieDetail = await _getServiceUrl<MovieDetail>($"movie/{idMovieService}");
            return movieDetail;
        }
    }
}