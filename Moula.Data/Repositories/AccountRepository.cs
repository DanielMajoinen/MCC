using Moula.Data.Dto;
using System.Threading.Tasks;

namespace Moula.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDatabaseFactory _databaseFactory;

        public AccountRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public async Task<Account> GetAccountAsync(int id)
        {
            using (var db = _databaseFactory.CreateConnection())
            {
                return await db.SingleOrDefaultAsync<Account>("EXEC [dbo].[uspAccount_Fetch] @@Id=@Id", new { Id = id }).ConfigureAwait(false);
            }
        }
    }
}
