using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Interfaces;

namespace Core.Application.UseCases
{
    public class GetAccountBalance
    {
        private readonly IAccountRepository _accountRepository;


        public GetAccountBalance(IAccountRepository  accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<decimal> ExecuteAsync(Guid accountId)
        {
            return await _accountRepository.GetAccountBalanceAsync(accountId);
        }
    }
}
