using Moula.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moula.Data.Repositories
{
    /// <summary>
    /// Repository class for the Ledger table.
    /// </summary>
    public class LedgerRepository : ILedgerRepository
    {
        private readonly IDatabaseFactory _databaseFactory;

        public LedgerRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        /// <summary>
        /// Retrieve a list of the accounts payment history, ordered by most recent.
        /// </summary>
        /// <param name="accountId">The ID of the account to retrieve the ledger for.</param>
        /// <returns>Returns the specified account ledger or null if it's not found.</returns>
        public async Task<List<Ledger>> GetLedgerByAccountAsync(int accountId)
        {
            using (var db = _databaseFactory.CreateConnection())
            {
                return await db.FetchAsync<Ledger>("EXEC [dbo].[uspLedger_FetchByAccount] @@AccountId=@AccountId", new { AccountId = accountId }).ConfigureAwait(false);
            }
        }
    }
}
