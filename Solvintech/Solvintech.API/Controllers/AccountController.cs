using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solvintech.API.Database;
using Solvintech.API.Interfaces;
using Solvintech.API.Models;
using Solvintech.API.Models.DTO;
using System.Security.Cryptography;
using System.Text;

namespace Solvintech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AccountController(ApplicationDbContext dbContext,
                                 IUserService userService,
                                 ITokenService tokenService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SignUp([FromBody] SignUpCredentialsDto model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new
                {
                    IsSuccess = false,
                    Message = "User signup failed."
                });
            }

            var isUserExists = await _userService.ExistsAsync(model.Email);
            if (isUserExists)
            {
                return new BadRequestObjectResult(new
                {
                    IsSuccess = false,
                    Message = "User is already exists."
                });
            }

            var hmac = new HMACSHA512();
            var user = new ApplicationUser
            {
                Email = model.Email.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password)),
                PasswordSalt = hmac.Key,
                AccessToken = _tokenService.CreateToken(model.Email)
            };

            await _dbContext.ApplicationUsers.AddAsync(user);
            var result = await _dbContext.SaveChangesAsync();

            return new OkObjectResult(new
            {
                IsSuccess = true,
                Message = "User sign up sucessfull."
            })
            { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SignIn([FromBody] SignInCredentialsDto model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new
                {
                    IsSuccess = false,
                    Message = "User sign in failed."
                });
            }

            var user = await _userService.GetByEmailAsync(model.Email);
            if (user == null)
            {
                return new BadRequestObjectResult(new
                {
                    IsSuccess = false,
                    Message = "Invalid email."
                });
            }

            if (!CheckPassword(user.PasswordSalt, user.PasswordHash, model.Password))
            {
                return new BadRequestObjectResult(new
                {
                    IsSuccess = false,
                    Message = "Invalid password."
                });
            }

            user.AccessToken = _tokenService.CreateToken(user.Email);
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();

            return new OkObjectResult(new
            {
                IsSuccess = true,
                Message = "User sign in successfull.",
                AccessToken = user.AccessToken
            });
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Logout([FromBody] LogoutCredentialsDto model)
        {
            var user = await _userService.GetByAccessTokenAsync(model.AccessToken);
            if (user == null)
            {
                return new BadRequestObjectResult(new
                {
                    IsSuccess = false,
                    Message = "User log out failed."
                });
            }

            user.AccessToken = string.Empty;

            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();

            return new OkObjectResult(new
            {
                IsSuccess = true,
                Message = "User log out successfull.",
                AccessToken = string.Empty
            });
        }

        private bool CheckPassword(byte[] userPasswordSalt, byte[] userPasswordHash, string enteredPassword)
        {
            var hmac = new HMACSHA512(userPasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));

            for (var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != userPasswordHash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
