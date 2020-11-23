using Moula.Data.Models;
using System.Threading.Tasks;

namespace Moula.Data.Repositories
{
    /// <summary>
    /// Repository class for the Account table.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Retrieve an account by ID.
        /// </summary>
        /// <param name="id">The ID of the account to retrieve.</param>
        /// <returns>Returns the specified account or null if it's not found.</returns>
        Task<Account> GetAccountAsync(int id);
    }
}
