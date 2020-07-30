using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace service.Util.Provider
{
    public interface IRequestProvider
    {
        Task<HttpResponseMessage> SendAsync(string url, HttpMethod verb, string body = null);

        Task<T> ReadJsonStream<T>(Stream stream);

        bool IsSuccess(HttpResponseMessage responseMessage);
    }
}