using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Product_Management_Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_Core.Services.Impl
{
    public class RestClient : IRestClient
    {
        private readonly ILogger<RestClient> _logger;

        public RestClient(ILogger<RestClient> logger)
        {
            _logger = logger;
        }
        private HttpClient CreateClient(string url)
        {
            var result = new HttpClient() { BaseAddress = new Uri(url) };

            result.DefaultRequestHeaders.Accept.Clear();
            result.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return result;
        }
        public async Task<HttpResponseMessage> Get(string url)
        {
            using (var client = CreateClient(url))
            {
                client.DefaultRequestHeaders.Connection.Clear();
                client.DefaultRequestHeaders.ConnectionClose = false;
                client.DefaultRequestHeaders.Connection.Add("Keep-Alive");
                client.Timeout = new TimeSpan(0, 20, 0);

                _logger.LogDebug("URL GET: " + url);

                return await client.GetAsync(url);
            }
        }

        public async Task<HttpResponseMessage> Post<TContent>(string url, TContent body)
        {
            using (var client = CreateClient(url))
            {
                client.DefaultRequestHeaders.Connection.Clear();
                client.DefaultRequestHeaders.ConnectionClose = false;
                client.DefaultRequestHeaders.Connection.Add("Keep-Alive");
                client.Timeout = new TimeSpan(0, 20, 0);

                var myContent = JsonConvert.SerializeObject(body);
                var stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");

                _logger.LogDebug("URL POST: " + url);
                _logger.LogDebug("BODY POST: " + myContent);

                return await client.PostAsync(url, stringContent);
            }
        }

        public async Task<string> PostAsyncToString<TContent>(string url, TContent body)
        {
            using (var response = await Post(url, body))
            {
                response.EnsureSuccessStatusCode();
                var resultContent = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("RESULT POST: " + resultContent);
                return resultContent;
            }
        }

        public async Task<TResult> PostAsyncToResult<TContent, TResult>(string url, TContent body)
        {
            using (var response = await Post(url, body))
            {
                response.EnsureSuccessStatusCode();
                var resultContent = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("RESULT POST: " + resultContent);
                var result = JsonConvert.DeserializeObject<TResult>(resultContent);
                return result;
            }
        }

        public async Task<TResult> GetAsyncToResult<TResult>(string url)
        {
            using (var response = await Get(url))
            {
                response.EnsureSuccessStatusCode();
                var resultContent = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("RESULT GET: " + resultContent);
                var result = JsonConvert.DeserializeObject<TResult>(resultContent);
                return result;
            }
        }
    }
}
