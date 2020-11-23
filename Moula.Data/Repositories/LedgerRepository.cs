using Moula.Data.Dto;
using NPoco;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moula.Data.Repositories
{
    public class LedgerRepository : ILedgerRepository
    {
        private readonly IDatabase _database;

        public LedgerRepository(IDatabase database)
        {
            _database = database;
        }

        public Task<List<Ledger>> GetAccountLedgerAsync(int accountId)
        {
            return _database.FetchAsync<Ledger>("EXEC [dbo].[uspLedger_FetchByAccount] @AccountId=@0", accountId);
        }
    }
}
