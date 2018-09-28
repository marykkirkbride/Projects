using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventsWebMvc.Infrastructure
{
    public class CustomHttpClient : IHttpClient
    {
        private HttpClient _client;
        private ILogger<CustomHttpClient> _logger;

        public CustomHttpClient( ILogger<CustomHttpClient> logger)
        {
            _client = new HttpClient();
            _logger = logger;
        }

        public async Task<HttpResponseMessage> DeleteAsync<T>(string uri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await _client.SendAsync(requestMessage);
        }

            public async Task<string> GetStringAsync(string uri)
        {
           var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await _client.SendAsync(requestMessage);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item)
        {
            return await DoPostPutAsync(HttpMethod.Post, uri, item);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item)
        {
            return await DoPostPutAsync(HttpMethod.Put, uri, item);
        }

        private async Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, T item)
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be eithe post or put.", nameof(method));
            }

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item),
                    System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(requestMessage);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }
            return response;
        }

    }
}
