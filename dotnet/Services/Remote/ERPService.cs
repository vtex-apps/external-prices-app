using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using service.Models;
using service.Util.Exceptions;
using service.Util.Provider;
using HttpMethod = System.Net.Http.HttpMethod;

namespace service.Services.Remote
{
    public class ErpService : IErpService
    {
        private readonly IRequestProvider _requestProvider;

        public ErpService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ErpQuoteDto> GetQuote(ErpQuoteDto erpQuoteDto)
        {
            using var responseMessage =
                await _requestProvider.SendAsync(string.Format(Constants.ErpPricesUrl), HttpMethod.Post, BuildQueryParams(erpQuoteDto));
            if (!_requestProvider.IsSuccess(responseMessage))
                throw new InvalidHttpResponseException("Error trying to quote price from the ERP service.",
                    responseMessage.StatusCode);
            var stream = await responseMessage.Content.ReadAsStreamAsync();
            return await _requestProvider.ReadJsonStream<ErpQuoteDto>(stream);
        }

        private Dictionary<string, string> BuildQueryParams(ErpQuoteDto erpQuoteDto)
        {
            var json = JsonConvert.SerializeObject(erpQuoteDto);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}