using Microsoft.Extensions.DependencyInjection;
using Moula.Business.Services;

namespace Moula.Business
{
    public static class Bindings
    {
        public static void RegisterBusiness(this IServiceCollection services)
        {
            services.AddTransient<ILedgerService, LedgerService>();
        }
    }
}
