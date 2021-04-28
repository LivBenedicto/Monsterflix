// Modelo para deserializar Json - lista de filmes

using System.Collections.Generic;

namespace Monsterflix.Api.Models.Service
{
    public class Result
    {
        public List<int> genre_ids { get; set; }
        public int id { get; set; }
        public string original_title { get; set; }
        public string poster_path { get; set; }
        public string title { get; set; }
    }

    public class MovieList
    {
        public int page { get; set; }
        public List<Result> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}