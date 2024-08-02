using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByIdAsync(Guid accountId);
        Task<Account> GetAccountByNumberAsync(string accountNumber);
        Task<decimal> GetAccountBalanceAsync(Guid accountId);
        Task UpdateAccountAsync(Account account);
    }
}
