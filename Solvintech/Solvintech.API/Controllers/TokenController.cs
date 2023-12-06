using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solvintech.API.Database;
using Solvintech.API.Interfaces;

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
            var accessToken = ControllerContext.HttpContext.Request.Headers["Authorization"].ToString();
            if (String.IsNullOrWhiteSpace(accessToken))
            {
                return new BadRequestObjectResult(new
                {
                    IsSuccess = false,
                    Message = "Invalid header 'Authorization'."
                });
            }

            var user = await _userService.GetByAccessTokenAsync(accessToken.Replace("Bearer ", string.Empty));
            if (user == null)
            {
                return new BadRequestObjectResult(new
                {
                    IsSuccess = false,
                    Message = "Invalid access token."
                });
            }

            var newGeneratedToken = _tokenService.CreateToken(user.Email);
            user.AccessToken = newGeneratedToken;

            await _dbContext.SaveChangesAsync();

            return new OkObjectResult(new
            {
                IsSuccess = true,
                Message = "Token updated successfull.",
                AccessToken = user.AccessToken
            });
        }
    }
}
