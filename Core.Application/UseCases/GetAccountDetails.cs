using Core.Domain.Entities;
using Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.UseCases
{
    public class GetAccountDetails
    {

        private readonly IAccountRepository _accountRepository;

        public GetAccountDetails (IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> ExecuteAsync (Guid accountId)
        {
            return await _accountRepository.GetAccountByIdAsync (accountId);
        }
    }
}
