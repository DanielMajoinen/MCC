using Microsoft.Extensions.DependencyInjection;
using Moula.Business;

namespace Moula.Api
{
    public static class Bindings
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.RegisterBusiness(); // Register business bindings
            return services;
        }
    }
}
