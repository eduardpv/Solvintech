using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solvintech.API.Utils;
using System.Text;

namespace Solvintech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public QuotationController(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        [Authorize]
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<string>> GetByDate(DateTime date)
        {
            var apiUrl = $"http://www.cbr.ru/scripts/XML_daily.asp?date_req={date.ToString("MM/dd/yyyy")}";

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
