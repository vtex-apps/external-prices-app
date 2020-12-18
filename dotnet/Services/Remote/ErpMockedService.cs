using System.Threading.Tasks;
using service.Models;

namespace service.Services.Remote
{
    public class ErpMockedService : IErpService
    {
        public async Task<ErpQuoteDto> GetQuote(ErpQuoteDto erpQuoteDto)
        {
            return new ErpQuoteDto
            {
                Sku = erpQuoteDto.Sku,
                Quantity = erpQuoteDto.Quantity,
                State = erpQuoteDto.State,
                OrderType = erpQuoteDto.OrderType,
                Price = erpQuoteDto.Price / 100 * int.Parse(erpQuoteDto.Sku),
            };
        }
    }
}