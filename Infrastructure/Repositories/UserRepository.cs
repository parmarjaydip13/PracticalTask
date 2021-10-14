using Application.Interfaces;
using Dapper;
using Domain;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            var sql = "SELECT EmailAddress,PasswordHash FROM TblUser where EmailAddress = @email";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(sql, new { email = email });
                return user;
            }
        }

        public async Task SeedUser(User user)
        {
            try
            {
                var sql = "INSERT into TblUser(EmailAddress,PasswordHash) values(@EmailAddress,@PasswordHash)";
                using (var connection = _context.CreateConnection())
                {
                    var returnValue = await connection.ExecuteAsync(sql, user);
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task<User> GetAny()
        {
            try
            {
                var sql = "select TOP 1 * from TblUser";
                using (var connection = _context.CreateConnection())
                {
                    var user = await connection.QueryFirstOrDefaultAsync<User>(sql);
                    return user;
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
            
        }

    }
}
