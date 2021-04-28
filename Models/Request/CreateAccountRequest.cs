namespace Monsterflix.Api.Models.Request
{
    public class CreateAccountRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Birthday { get; set; }
    }
}