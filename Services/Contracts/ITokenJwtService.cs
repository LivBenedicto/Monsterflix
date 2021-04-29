using Monsterflix.Api.Models;

namespace Monsterflix.Api.Services.Contracts
{
    public interface ITokenJwtService
    {
        string GenerateToken(Account account);
    }
}