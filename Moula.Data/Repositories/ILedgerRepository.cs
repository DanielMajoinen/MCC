using Moula.Data.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moula.Data.Repositories
{
    interface ILedgerRepository
    {
        Task<IEnumerable<Ledger>> GetAccountLedger(int accountId);
    }
}
