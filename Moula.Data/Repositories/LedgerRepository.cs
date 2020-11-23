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

        public Task<List<Ledger>> GetAccountLedgerAsync(int accountId)
        {
            using (var db = _databaseFactory.CreateConnection())
            {
                return db.FetchAsync<Ledger>("EXEC [dbo].[uspLedger_FetchByAccount] @AccountId=@0", accountId);
            }
        }
    }
}
