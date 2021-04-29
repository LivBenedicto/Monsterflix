// Classe que controla acesso à conta

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monsterflix.Api.Configurations;
using Monsterflix.Api.Models;
using Monsterflix.Api.Models.Request;
using Monsterflix.Api.Models.Result;
using Monsterflix.Api.Repositories.Contracts;
using Monsterflix.Api.Services.Contracts;

namespace Monsterflix.Api.Controllers
{
    [Route("v1/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenJwtService _tokenJwtService;

        public AccountController(IAccountRepository accountRepository, ITokenJwtService tokenJwtService)
        {
            _accountRepository = accountRepository;
            _tokenJwtService = tokenJwtService;
        }

        [HttpGet]
        [Authorize]
        public async Task<Account> GetAuthorizeTokenAccountById()
        {
            int idAccount = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "IdAccount").Value);
            return await _accountRepository.GetAccountById(idAccount);
        }

        [HttpPost]
        public async Task<Account> PostCreateNewAccount([FromBody] CreateAccountRequest createAccountRequest)
        {
            Account account = new Account();
            account.Email = createAccountRequest.Email;
            account.Password = createAccountRequest.Password;
            account.Username = createAccountRequest.Username;
            account.Birthday = createAccountRequest.Birthday;

            return await _accountRepository.CreateNewAccount(account);
        }

        [HttpPost("login")]
        public async Task<AccountLoginResult> PostLoginAccount([FromBody] LoginAccountRequest loginAccountRequest)
        {
            Account account = await _accountRepository.LoginAccount(loginAccountRequest.Email, loginAccountRequest.Password);

            AccountLoginResult accountLoginResult = new AccountLoginResult();
            accountLoginResult.Token = _tokenJwtService.GenerateToken(account);
            accountLoginResult.Expires = DateTime.UtcNow.AddHours(int.Parse(AppSettingsProvider.Settings["TimeExpirationTokenJwtBearer"]));
            accountLoginResult.Type = "Bearer";

            return accountLoginResult;
        }
    }
}