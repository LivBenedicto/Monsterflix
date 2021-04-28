namespace Monsterflix.Api.Models
{
    public class MovieGenre
    {
        public int IdMovieGenre { get; set; }
        public int IdMovie { get; set; }
        public int IdGenreService { get; set; }
        public Movie Movie { get; set; }
    }
}