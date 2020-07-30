using System.Threading.Tasks;
using service.Models;
using service.Models.DTO;
using service.Services.Remote;

namespace service.Services.Impl
{
    public class QuoteService : IQuoteService
    {

        private readonly IERPService _erpService;

        public QuoteService(IERPService erpService)
        {
            _erpService = erpService;
        }

        public async Task<Quote> GetPrice(ProductDTO productDTO)
        {
            return await _erpService.GetPrice(Product.From(productDTO));
        }
        
    }
}