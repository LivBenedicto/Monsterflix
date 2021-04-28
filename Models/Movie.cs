using System.Collections.Generic;

namespace Monsterflix.Api.Models
{
    public class Movie
    {
        public int IdMovie { get; set; }
        public int IdMovieService { get; set; }
        public List<MovieGenre> Genre { get; set; }
    }
}