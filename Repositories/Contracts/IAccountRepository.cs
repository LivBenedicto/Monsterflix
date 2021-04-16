using System.Threading.Tasks;
using Monsterflix.Api.Models;

namespace Monsterflix.Api.Repositories.Contracts
{
    public interface IAccountRepository
    {
        Task<Account> CreateNewAccount(Account account);
        Task<Account> GetAccountById(int idAccount);
    }
}