using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solvintech.API.Interfaces;
using Solvintech.API.Utils;
using Solvintech.API.Сommon;
using System.Text;

namespace Solvintech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IUserService _userService;

        public QuotationController(HttpClient httpClient,
                                   IUserService userService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [Authorize]
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<string>> GetByDate(DateTime date)
        {
            var accessToken = HttpContext.Request.Headers[Constants.Configuration.Authorization].ToString();
            if (String.IsNullOrWhiteSpace(accessToken))
            {
                return Unauthorized(new
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
                return Unauthorized(new
                {
                    IsSuccess = false,
                    Message = Constants.Token.InvalidAccessToken
                });
            }

            var apiUrl = Constants.Quotation.GetQuotationsUrl(date);

            var response = await _httpClient.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var content = await response.Content.ReadAsStringAsync();

            var result = XmlJsonUtils.XmlToJson(content);
            return result == null
                ? NotFound()
                : new OkObjectResult(result);
        }
    }
}
