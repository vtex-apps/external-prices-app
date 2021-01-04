using System.Threading.Tasks;
using service.Models;
using service.Services.Remote;

namespace service.Services
{
    public class PriceService : IProductService
    {
        private readonly IErpService _erpService;

        public PriceService(IErpService erpService)
        {
            _erpService = erpService;
        }

        public async Task<QuoteDto> GetQuote(QuoteDto quoteDto)
        {
            var erpQuoteDtoReq = ToErpQuoteDto(quoteDto);
            var erpQuoteDtoResp = await _erpService.GetQuote(erpQuoteDtoReq);
            return ToQuoteDto(erpQuoteDtoResp);
        }

        private ErpQuoteDto ToErpQuoteDto(QuoteDto quoteDto)
        {
            return new ErpQuoteDto
            {
                SkuId = quoteDto.SkuId,
                Quantity = quoteDto.Quantity,
                Price = quoteDto.Price
            };
        }

        private QuoteDto ToQuoteDto(ErpQuoteDto erpQuoteDto)
        {
            return new QuoteDto()
            {
                SkuId = erpQuoteDto.SkuId,
                Quantity = erpQuoteDto.Quantity,
                Price = erpQuoteDto.Price
            };
        }
    }
}