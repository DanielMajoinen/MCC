using Moula.Data.Models;
using System.Threading.Tasks;

namespace Moula.Data.Repositories
{
    /// <summary>
    /// Repository class for the Account table.
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly IDatabaseFactory _databaseFactory;

        public AccountRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        /// <summary>
        /// Retrieve an account by ID.
        /// </summary>
        /// <param name="id">The ID of the account to retrieve.</param>
        /// <returns>Returns the specified account or null if it's not found.</returns>
        public async Task<Account> GetAccountAsync(int id)
        {
            using (var db = _databaseFactory.CreateConnection())
            {
                return await db.SingleOrDefaultAsync<Account>("EXEC [dbo].[uspAccount_Fetch] @@Id=@Id", new { Id = id }).ConfigureAwait(false);
            }
        }
    }
}
