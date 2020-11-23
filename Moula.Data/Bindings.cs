using Microsoft.Extensions.DependencyInjection;
using Moula.Data.Repositories;

namespace Moula.Data
{
    public static class Bindings
    {
        public static void RegisterData(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseFactory, DatabaseFactory>();
            services.AddTransient<ILedgerRepository, LedgerRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
        }
    }
}
