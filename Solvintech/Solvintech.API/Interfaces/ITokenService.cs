using Solvintech.API.Models;

namespace Solvintech.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(string email);
    }
}
