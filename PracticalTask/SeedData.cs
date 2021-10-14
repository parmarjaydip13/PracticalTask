using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace PracticalTask
{

    public static class DbInitializer
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            var userRepository = applicationBuilder.ApplicationServices.GetRequiredService<IUserRepository>();

            var existingUser = await userRepository.GetAny();
            
            if(existingUser == null)
            {
                var user = new User
                {
                    EmailAddress = "abc@gmail.com",
                    PasswordHash = Crypto.HashPassword("12345"),
                };
                await userRepository.SeedUser(user);
            }
        }
    }
}
