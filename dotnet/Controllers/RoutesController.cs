using Microsoft.AspNetCore.Mvc;
using Vtex.Api.Context;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Net;
using System.Text;
using service.Models.DTO;
using service.Services;
using service.Util.Provider;

namespace service.Controllers
{
    public class RoutesController : Controller
    {
        private readonly IIOServiceContext  _context;
        private readonly IProductService _productService;
        private readonly IRequestProvider _requestProvider;

        public RoutesController(IIOServiceContext context, IProductService productService, IRequestProvider requestProvider)
        {
            _context = context;
            _productService = productService;
            _requestProvider = requestProvider;
        }

     public async Task<JsonResult> GetPrice()
        {
            try
            {
                var productDTO = await _requestProvider.ReadJsonStream<ProductDTO>(new MemoryStream(Encoding.UTF8.GetBytes("{}")));
                var quote = await _productService.GetPrice(productDTO);
                return new JsonResult(new {Message = "Price quoted successfully.", Schema = quote}) {StatusCode = (int)HttpStatusCode.OK};
            }
            catch (Exception ex)
            {
                _context.Vtex.Logger.Error("RouteController", "GetPrice", "Error finding product price,", ex);
                return new JsonResult("Unexpected error.") {StatusCode = 500};
            }
        }
    }
}
