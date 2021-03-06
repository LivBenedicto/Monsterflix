// Modelo para armazenar o token de autenticação pós login

using System;

namespace Monsterflix.Api.Models.Result
{
    public class AccountLoginResult
    {
        public string Token { get; set; }
        public string Type { get; set; }
        public DateTime Expires { get; set; }
    }
}