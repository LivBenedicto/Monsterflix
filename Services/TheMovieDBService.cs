// Integração com API The Movie DB

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Monsterflix.Api.Configurations;
using Monsterflix.Api.Models;
using Monsterflix.Api.Services.Contracts;

namespace Monsterflix.Api.Services
{
    public class TheMovieDBService : ITheMovieDBService
    {
        private readonly HttpClient _httpClient;
        private readonly string _key;

        public TheMovieDBService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

        // Requisição para pesquisar na plataforma 
        public async Task<int> SearchMovie(string keyword)
        {
            MovieService movieService = await _getServiceUrl<MovieService>($"search/movie?query={keyword}&include_adult=false");
            return movieService.id;
        }
    }
}