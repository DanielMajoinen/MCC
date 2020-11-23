using Moula.Contracts;
using Moula.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moula.Business.Services
{
    /// <summary>
    /// The account service is responsible for talking to the data access layer in order to retrieve all things account related.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILedgerRepository _ledgerRepository;

        public AccountService(IAccountRepository accountRepository, ILedgerRepository ledgerRepository)
        {
            _accountRepository = accountRepository;
            _ledgerRepository = ledgerRepository;
        }

        /// <summary>
        /// Retrieve an accounts ledger for a given accountId.
        /// This talks with the data access layer.
        /// </summary>
        /// <param name="accountId">The account to retrieve ledger of.<param>
        /// <returns>Returns an accounts ledger if found or null otherwise.</returns>
        public async Task<AccountLedger> GetAccountLedgerAsync(int accountId)
        {
            // Parallel async calls to save time
            var accountTask = _accountRepository.GetAccountAsync(accountId);
            var ledgerTask = _ledgerRepository.GetLedgerByAccountAsync(accountId);

            await Task.WhenAll(accountTask, ledgerTask).ConfigureAwait(false); // Avoid deadlocks

            var account = await accountTask;
            var ledger = await ledgerTask;

            // Map results to return type
            return account == null
                ? null
                : new AccountLedger
                {
                    Account = account.Id,
                    Balance = account.Balance,
                    PaymentHistory = !ledger.Any()
                        ? new List<Payment>()
                        : ledger.Select(l => new Payment
                        {
                            Id = l.Id,
                            Date = l.Date,
                            Amount = l.Amount,
                            Status = l.Status,
                            ClosedReason = l.ClosedReason
                        })
                };
        }
    }
}
