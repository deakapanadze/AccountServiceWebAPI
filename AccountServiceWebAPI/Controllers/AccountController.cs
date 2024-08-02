using AccountServiceWebAPI.Models;
using AutoMapper;
using Core.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace AccountServiceWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {

        private readonly GetAccountBalance _getAccountBalance;
        private readonly GetAccountDetails _getAccountDetails;
        private readonly IMapper _mapper;


        public AccountController(GetAccountBalance accountBalance, GetAccountDetails getAccountDetails, IMapper mapper)
        {
            _getAccountBalance = accountBalance;
            _getAccountDetails = getAccountDetails;
            _mapper = mapper;
        }
        [HttpGet("{id/balance}")]
        public async Task<IActionResult>GetBalance (Guid id)
        {
            var balance = await _getAccountBalance.ExecuteAsync(id);
            return Ok(balance);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(Guid id)
        {
            var account = await _getAccountDetails.ExecuteAsync(id);
            var accountDto = _mapper.Map<AccountDto>(account);
            return Ok(accountDto);
        }
        

    }
}
