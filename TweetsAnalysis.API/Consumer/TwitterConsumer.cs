﻿using System.Net.Http.Headers;
using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.API.Consumer
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
            Uri baseUri = new Uri(_configuration.GetValue<string>("Twitter:BaseUrl"));
            _client.BaseAddress = baseUri;
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.ConnectionClose = true;
        }

        public async Task RefreshTokenAsync()
        {
            var clientId = _configuration.GetValue<string>("Twitter:ClientId");
            var clientSecret = _configuration.GetValue<string>("Twitter:ClientSecret");
            var authenticationString = $"{clientId}:{clientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));

            // Post body content
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            var content = new FormUrlEncodedContent(values);

            // Add authentication header
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/oauth2/token");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Content = content;

            var responseMessage = await _client.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode();

            var body = await responseMessage.Content.ReadFromJsonAsync<TokenModel>();
            _token = body?.AccessToken;
        }


        public async Task<Stream> GetStreamAsync()
        {
            // Get authentication token
            if (_token == null)
            {
                await RefreshTokenAsync();
            }

            var client = new HttpClient();
            Uri baseUri = new Uri(_configuration.GetValue<string>("Twitter:BaseUrl"));
            client.BaseAddress = baseUri;
            // Add authentication header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            return await client.GetStreamAsync("/2/tweets/sample/stream");
        }
    }
}
