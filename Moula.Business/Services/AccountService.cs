using Moula.Contracts;
using Moula.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moula.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILedgerRepository _ledgerRepository;

        public AccountService(IAccountRepository accountRepository, ILedgerRepository ledgerRepository)
        {
            _accountRepository = accountRepository;
            _ledgerRepository = ledgerRepository;
        }

        public async Task<AccountLedger> GetAccountLedgerAsync(int accountId)
        {
            var accountTask = _accountRepository.GetAccountAsync(accountId);
            var ledgerTask = _ledgerRepository.GetLedgerByAccountAsync(accountId);

            await Task.WhenAll(accountTask, ledgerTask).ConfigureAwait(false); // Avoid deadlocks

            var account = await accountTask;
            var ledger = await ledgerTask;

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
