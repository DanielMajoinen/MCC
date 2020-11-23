using Microsoft.Extensions.DependencyInjection;
using Moula.Data.Repositories;

namespace Moula.Data
{
    public static class Bindings
    {
        public static void RegisterData(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseFactory>(new DatabaseFactory("server=.,1433;database=Moula;user id=SA;password=P@ssword1"));
            services.AddTransient<ILedgerRepository, LedgerRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
        }
    }
}
