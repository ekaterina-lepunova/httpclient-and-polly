using Microsoft.AspNetCore.Mvc;
using HttpClients_and_Polly.Clients;

namespace HttpClients_and_Polly.Controllers
{
    [Route("api/external")]
    [ApiController]
    public class ExternalController : ControllerBase
    {
        private readonly ISteavegordonPageClient _steavegordonClient;
        private readonly ITypicodeClient _typicodeClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public ExternalController(
            ISteavegordonPageClient steavegordonClient,
            ITypicodeClient typicodeClient,
            IHttpClientFactory httpClientFactory)
        {
            _steavegordonClient = steavegordonClient;
            _typicodeClient = typicodeClient;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("steavegordon")]
        public async Task<string> GetSteavegordonPage(string relativeUri)
        {
            return await _steavegordonClient.GetContent(relativeUri);
        }

        [HttpGet("typicode")]
        public async Task<string> GetTypicodeData(string relativeUri)
        {
            return await _typicodeClient.GetDummyJson(relativeUri);
        }

        [HttpGet("google")]
        public async Task<string> GetGooglePage()
        {
            var client = _httpClientFactory.CreateClient("google");
            var response = await client.GetAsync("/");
            return await response.Content.ReadAsStringAsync();
        }

        [HttpGet("yandex")]
        public async Task<string> GetYandexPage()
        {
            var client = _httpClientFactory.CreateClient("yandex");
            var response = await client.GetAsync("/");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
