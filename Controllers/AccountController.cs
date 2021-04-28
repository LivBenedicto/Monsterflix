// Classe que controla acesso à conta

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monsterflix.Api.Models;
using Monsterflix.Api.Models.Request;
using Monsterflix.Api.Repositories.Contracts;

namespace Monsterflix.Api.Controllers
{
    [Route("v1/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("{idAccount}")]
        public async Task<Account> GetAccountById(int idAccount)
        {
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
    }
}