using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moula.Business.Services;
using Moula.Contracts;
using System;
using System.Threading.Tasks;

namespace Moula.Api.Controllers
{
    /// <summary>
    /// Entry point for all things account related.
    /// </summary>
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

        /// <summary>
        /// Retrieve an accounts ledger, including the balance and payment history.
        /// This talks with the service layer and performs some minor error handling.
        /// </summary>
        /// <param name="accountId">The account to retrieve ledger of.</param>
        /// <returns>Returns an accounts ledger if found or 404 when the account does not exist.</returns>
        [HttpGet]
        [Route("ledger/{accountId}")]
        public async Task<ActionResult<AccountLedger>> Get([FromRoute] int accountId)
        {
            AccountLedger accountLedger;

            try
            {
                accountLedger = await _accountService.GetAccountLedgerAsync(accountId).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                // Catch any exception, log, then return 500
                _logger.LogError(ex, "Failed to retrieve accounts ledger.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // Return a 404 if the account does not exist
            if (accountLedger == null)
                return NotFound();

            // JSON serialisation automatically occurs
            return accountLedger;
        }
    }
}
