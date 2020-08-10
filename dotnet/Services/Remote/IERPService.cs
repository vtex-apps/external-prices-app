using System.Threading.Tasks;
using service.Models;

namespace service.Services.Remote
{
    public interface IERPService
    {
        Task<Quote> GetPrice(Product product);
    }
}