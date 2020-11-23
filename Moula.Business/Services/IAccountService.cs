using Moula.Contracts;
using System.Threading.Tasks;

namespace Moula.Business.Services
{
    public interface IAccountService
    {
        Task<AccountLedger> GetAccountLedgerAsync(int accountId);
    }
}
