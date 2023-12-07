using Microsoft.IdentityModel.Tokens;
using Solvintech.API.Interfaces;
using Solvintech.API.Сommon;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Solvintech.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[Constants.Configuration.SecretKey]
                ?? throw new InvalidOperationException(Constants.ConfigurationErrors.SecretKetNotFound)));
        }

        public string CreateToken(string email)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.NameId, email)
                }),
                SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
