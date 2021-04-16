// Modelo para deserializar Json

using System.Collections.Generic;

namespace Monsterflix.Api.Models
{
    public class MovieService
    {
        public string poster_path { get; set; }
        public List<Genre> genres { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_title { get; set; }
    }

    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}