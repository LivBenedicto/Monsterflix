// Modelo para fazer login na conta

namespace Monsterflix.Api.Models.Request
{
    public class LoginAccountRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}