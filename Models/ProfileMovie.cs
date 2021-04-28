// Tabela relacionamento entre Profile e Movie
using Monsterflix.Api.Enum;

namespace Monsterflix.Api.Models
{
    public class ProfileMovie
    {
        public int IdProfileMovie { get; set; }
        public int IdProfile { get; set; }
        public int IdMovie { get; set; }
        public EStatusMovie StatusWatch { get; set; }
        public Profile Profile { get; set; }
        public Movie Movie { get; set; }
    }
}