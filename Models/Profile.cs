using System.Collections.Generic;

namespace Monsterflix.Api.Models
{
    public class Profile
    {
        public int IdProfile { get; set; }
        public string Username { get; set; }
        public List<ProfileMovie> Movie { get; set; }

        // Foreign key
        public int IdAccount { get; set; }
        public Account Account { get; set; }
    }
}