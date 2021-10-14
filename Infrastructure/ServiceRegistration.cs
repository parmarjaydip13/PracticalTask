using Application.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceRegistration
    {

        //register service for that created extension method
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICityRepository, CityRepostiory>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<AppDbContext>();
        }
    }
}
