using Moula.Data.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moula.Data.Repositories
{
    public class LedgerRepository : ILedgerRepository
    {
        private readonly IDatabaseFactory _databaseFactory;

        public LedgerRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public async Task<List<Ledger>> GetAccountLedgerAsync(int accountId)
        {
            using (var db = _databaseFactory.CreateConnection())
            {
                return await db.FetchAsync<Ledger>("EXEC [dbo].[uspLedger_FetchByAccount] @@AccountId=@AccountId", new { AccountId = accountId }).ConfigureAwait(false);
            }
        }
    }
}
