using Moula.Contracts;
using System.Threading.Tasks;

namespace Moula.Business.Services
{
    /// <summary>
    /// The account service is responsible for talking to the data access layer in order to retrieve all things account related.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Retrieve an accounts ledger for a given accountId.
        /// This talks with the data access layer.
        /// </summary>
        /// <param name="accountId">The account to retrieve ledger of.<param>
        /// <returns>Returns an accounts ledger if found or null otherwise.</returns>
        Task<AccountLedger> GetAccountLedgerAsync(int accountId);
    }
}
