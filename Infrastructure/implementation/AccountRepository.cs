using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Infrastructure.implementation
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository (DataContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetAccountBalanceAsync(Guid accountId)
        {
            var account = await GetAccountByIdAsync(accountId);
            return account?.Balance ?? 0;
        }

        public async Task<Account> GetAccountByIdAsync(Guid accountId)
        {
            return await _context.acounts.FindAsync(accountId);
        }

       public async Task<Account> GetAccountByNumberAsync(string accountNumber)
        {
            return await _context.acounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

       public async  Task UpdateAccountAsync(Account account)
        {
            _context.acounts.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}
