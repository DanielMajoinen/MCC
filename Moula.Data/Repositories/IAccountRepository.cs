using Moula.Data.Models;
using System.Threading.Tasks;

namespace Moula.Data.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountAsync(int id);
    }
}
