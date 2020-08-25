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

        public async Task<ErpQuoteDto> GetQuote(QuoteDto quoteDto)
        {
            return await _erpService.GetQuote(ToErpQuoteDto(quoteDto));
        }

        private ErpQuoteDto ToErpQuoteDto(QuoteDto quoteDto)
        {
            return new ErpQuoteDto
            {
                Sku = quoteDto.Sku,
                Quantity = quoteDto.Quantity,
                Currency = quoteDto.Currency,
                Email = quoteDto.Email,
                State = quoteDto.State,
                OrderType = quoteDto.OrderType
            };
        }
    }
}