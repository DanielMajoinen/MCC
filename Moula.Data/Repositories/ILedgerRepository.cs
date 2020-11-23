using Moula.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moula.Data.Repositories
{
    /// <summary>
    /// Repository class for the Ledger table.
    /// </summary>
    public interface ILedgerRepository
    {
        /// <summary>
        /// Retrieve a list of the accounts payment history, ordered by most recent.
        /// </summary>
        /// <param name="accountId">The ID of the account to retrieve the ledger for.</param>
        /// <returns>Returns the specified account ledger or null if it's not found.</returns>
        Task<List<Ledger>> GetLedgerByAccountAsync(int accountId);
    }
}
