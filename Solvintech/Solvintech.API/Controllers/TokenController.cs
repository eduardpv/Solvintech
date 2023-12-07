using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solvintech.API.Database;
using Solvintech.API.Extensions;
using Solvintech.API.Interfaces;
using Solvintech.API.Сommon;

namespace Solvintech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public TokenController(ApplicationDbContext dbContext,
                               IUserService userService,
                               ITokenService tokenService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [Authorize]
        [HttpPost]
        [Route("generate")]
        public async Task<IActionResult> UpdateToken()
        {
            var accessToken = HttpContext.Request.Headers[Constants.Configuration.Authorization].ToString();
            if (String.IsNullOrWhiteSpace(accessToken))
            {
                return new BadRequestObjectResult(new
                {
                    IsSuccess = false,
                    Message = Constants.Token.InvalidHeaderAuthorization
                });
            }

            var user = await _userService.GetByAccessTokenAsync(accessToken.Replace($"{Constants.Configuration.Bearer} ",
                                                                string.Empty,
                                                                StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                return new BadRequestObjectResult(new
                {
                    IsSuccess = false,
                    Message = Constants.Token.InvalidAccessToken
                });
            }

            var newGeneratedToken = _tokenService.CreateToken(user.Email);
            user.AccessToken = newGeneratedToken;

            await _dbContext.SaveChangesAsync();

            return new OkObjectResult(new
            {
                IsSuccess = true,
                Message = Constants.Token.UpdateSuccess,
                AccessToken = user.AccessToken
            });
        }
    }
}
