using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monsterflix.Api.Data;
using Monsterflix.Api.Models;
using Monsterflix.Api.Repositories.Contracts;

namespace Monsterflix.Api.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Account> CreateNewAccount(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> GetAccountById(int idAccount)
        {
            return await _context.Accounts.Where(account => account.IdAccount == idAccount).FirstOrDefaultAsync();
        }
    }
}