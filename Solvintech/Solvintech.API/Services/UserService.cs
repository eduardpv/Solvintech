using Microsoft.EntityFrameworkCore;
using Solvintech.API.Database;
using Solvintech.API.Interfaces;
using Solvintech.API.Models;

namespace Solvintech.API.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _dbContext.ApplicationUsers
                     .SingleOrDefaultAsync(u => u.Email == email.ToLower());
        }

        public async Task<ApplicationUser> GetByAccessTokenAsync(string accessToken)
        {
            return await _dbContext.ApplicationUsers
                  .SingleOrDefaultAsync(u => u.AccessToken == accessToken);
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _dbContext.ApplicationUsers
                .AnyAsync(u => u.Email == email.ToLower());
        }
    }
}
