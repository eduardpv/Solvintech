using Solvintech.API.Models;

namespace Solvintech.API.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByAccessTokenAsync(string accessToken);
        Task<bool> ExistsAsync(string email);
    }
}
