using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Web.Consumer
{
    public class TwitterConsumer : ITwitterConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        private string? _token;

        public TwitterConsumer(IConfiguration configuration)
        {
            _configuration = configuration;

            //setup reusable http client
            _client = new HttpClient();
            Uri baseUri = new Uri(_configuration.GetValue<string>("TwitterSettings:BaseUrl"));
            _client.BaseAddress = baseUri;
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.ConnectionClose = true;
        }

        /// <summary>
        /// Get token
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetTokenAsync()
        {
            var clientId = _configuration.GetValue<string>("TwitterSettings:ClientId");
            var clientSecret = _configuration.GetValue<string>("TwitterSettings:ClientSecret");
            var authenticationString = $"{clientId}:{clientSecret}";
            var encodedAuthentication = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));

            // Post body content
            var urlContent = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };

            var content = new FormUrlEncodedContent(urlContent);

            // Add authentication header
            var request = new HttpRequestMessage(HttpMethod.Post, "/oauth2/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedAuthentication);
            request.Content = content;

            var responseMessage = await _client.SendAsync(request);
            responseMessage.EnsureSuccessStatusCode();

            var body = await responseMessage.Content.ReadFromJsonAsync<TokenModel>();
            return body?.AccessToken;
        }


        public async Task<Stream> GetStreamAsync()
        {
            // Get token
            if (_token == null)
            {
                _token = await GetTokenAsync();
            }

            var client = new HttpClient();
            Uri baseUri = new Uri(_configuration.GetValue<string>("TwitterSettings:BaseUrl"));
            client.BaseAddress = baseUri;
            // Add authentication header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            return await client.GetStreamAsync("/2/tweets/sample/stream");
        }
    }
}
