// Gerar token

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Monsterflix.Api.Configurations;
using Monsterflix.Api.Models;
using Monsterflix.Api.Services.Contracts;

namespace Monsterflix.Api.Services
{
    public class TokenJwtService : ITokenJwtService
    {
        private readonly string _key;
        private readonly string _expirationToken;

        public TokenJwtService()
        {
            _key = AppSettingsProvider.Settings["KeyTokenJwtBearer"];
            _expirationToken = AppSettingsProvider.Settings["TimeExpirationTokenJwtBearer"];
        }

        public string GenerateToken(Account account)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_key);
            
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("IdAccount", account.IdAccount.ToString()),
                    new Claim(ClaimTypes.Name, account.Username.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(int.Parse(_expirationToken)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}