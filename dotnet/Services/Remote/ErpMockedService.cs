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
                SkuId = erpQuoteDto.SkuId,
                Quantity = erpQuoteDto.Quantity,
                State = erpQuoteDto.State,
                OrderType = erpQuoteDto.OrderType,
                Price = int.Parse(erpQuoteDto.SkuId) * 10,
            };
        }
    }
}