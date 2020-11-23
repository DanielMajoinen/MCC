using Microsoft.Extensions.DependencyInjection;
using Moula.Business.Services;
using Moula.Data;

namespace Moula.Business
{
    public static class Bindings
    {
        public static void RegisterBusiness(this IServiceCollection services)
        {
            services.RegisterData(); // Register data bindings
            services.AddTransient<ILedgerService, LedgerService>();
        }
    }
}
