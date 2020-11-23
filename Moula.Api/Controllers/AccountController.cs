using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moula.Business.Services;
using Moula.Contracts;
using System.Threading.Tasks;

namespace Moula.Api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet]
        [Route("ledger/{accountId}")]
        public async Task<ActionResult<AccountLedger>> Get([FromRoute] int accountId)
        {
            var accountLedger = await _accountService.GetAccountLedgerAsync(accountId).ConfigureAwait(false);

            if (accountLedger == null)
                return NotFound();

            return accountLedger;
        }
    }
}
