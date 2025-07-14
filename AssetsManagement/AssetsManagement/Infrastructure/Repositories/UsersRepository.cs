using AssetsManagement.Configuration;
using AssetsManagement.Domain.Entities;
using AssetsManagement.Infrastructure.Data;
using Microsoft.Extensions.Options;

namespace AssetsManagement.Infrastructure.Repositories
{
    public class UsersRepository : BaseRepository
    {
        public UsersRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> FindUser(string username)
        {
            string query = "SELECT * FROM Users WHERE username = @username";

            return await Find<User?>(query, new { username });
        }
    }
}
