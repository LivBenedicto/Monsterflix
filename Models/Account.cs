using System.Collections.Generic;

namespace Monsterflix.Api.Models
{
    public class Account
    {
        public int IdAccount { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Birthday { get; set; }

        // Entidade relacionada
        public List<Profile> Profile { get; set; }
    }
}