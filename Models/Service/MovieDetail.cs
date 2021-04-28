// Modelo para deserializar Json - detalhes de um único filme

using System.Collections.Generic;

namespace Monsterflix.Api.Models.Service
{
    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class MovieDetail
    {
        public string backdrop_path { get; set; }
        public List<Genre> genres { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public object poster_path { get; set; }
        public string title { get; set; }
    }
}