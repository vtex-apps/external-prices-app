using System.Threading.Tasks;
using service.Models;
using service.Util.Exceptions;
using service.Util.Provider;
using service.Util.Provider.Impl;
using Vtex.Api.Context;
using HttpMethod = System.Net.Http.HttpMethod;

namespace service.Services.Remote.Impl
{
    public class ERPService : IERPService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IIOServiceContext _context;

        public ERPService(IIOServiceContext context, IRequestProvider requestProvider)
        {
            _context = context;
            _requestProvider = requestProvider;
        }

        public async Task<Quote> GetPrice(Product product)
        {
            using var responseMessage = await _requestProvider.SendAsync(string.Format(Constants.ERPPricesUrl, _context.Vtex.Account), HttpMethod.Post);
            if (!_requestProvider.IsSuccess(responseMessage))
                throw new InvalidHttpResponseException("Error trying to save orderFormConfig on checkout service.", responseMessage.StatusCode);
            var stream = await responseMessage.Content.ReadAsStreamAsync();
            return await _requestProvider.ReadJsonStream<Quote>(stream);
        }
    }
}