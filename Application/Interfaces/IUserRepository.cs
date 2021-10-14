using Domain;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
        Task SeedUser(User user);
        Task<User> GetAny();
    }
}
