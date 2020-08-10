using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vtex.Api.Context;

namespace service.Util.Provider
{
    public class RequestProvider : IRequestProvider
    {
        private readonly IIOServiceContext _context;
        private readonly IHttpClientFactory _clientFactory;

        public RequestProvider(IIOServiceContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
        }

        public async Task<HttpResponseMessage> SendAsync(string url, HttpMethod verb, string body = null)
        {
            var requestMessage = CreateRequest(url, verb, body);
            return await _clientFactory.CreateClient().SendAsync(requestMessage);
        }
        
        private HttpRequestMessage CreateRequest(string url, HttpMethod verb, string body = null)
        {
            var request = new HttpRequestMessage(verb, "") {RequestUri = new Uri(url)};
            if (body != null)
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            return request;
        }

        public async Task<T> ReadJsonStream<T>(Stream stream)
        {
            using var reader = new StreamReader(stream);
            var body = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }

        public bool IsSuccess(HttpResponseMessage responseMessage)
        {
            return responseMessage.IsSuccessStatusCode || responseMessage.StatusCode == HttpStatusCode.NotModified;
        } 
    }
}