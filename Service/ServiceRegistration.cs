using Microsoft.Extensions.DependencyInjection;
using Service.Interface;

namespace Service
{
    public static class ServiceRegistration
    {

        //register service for that created extension method
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IHelperService, HelperService>();
        }
    }
}
