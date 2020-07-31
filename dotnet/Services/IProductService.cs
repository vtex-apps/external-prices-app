using System.Threading.Tasks;
using service.Models;
using service.Models.DTO;

namespace service.Services
{
    public interface IProductService
    {
        Task<Quote> GetPrice(ProductDTO productDTO);
    }
}