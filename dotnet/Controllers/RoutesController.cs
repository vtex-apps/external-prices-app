using Microsoft.AspNetCore.Mvc;
using Vtex.Api.Context;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using service.Models.Request;
using service.Models.HealthCheck;
using service.Models.Price;
using service.Services;
using service.Util.Provider;

namespace service.Controllers
{
    public class RoutesController : Controller
    {
        private readonly IIOServiceContext _context;
        private readonly IProductService _productService;
        private readonly IRequestProvider _requestProvider;

        public RoutesController(IIOServiceContext context, IProductService productService,
            IRequestProvider requestProvider)
        {
            _context = context;
            _productService = productService;
            _requestProvider = requestProvider;
        }

        [HttpGet]
        public async Task<ActionResult> HealthCheck()
        {
            return Ok(new HealthCheckResponse());
        }

        [HttpPost]
        public async Task<ActionResult> GetPrice([FromBody] Request request)
        {
            try
            {
                var result = await _productService.GetQuote(request.Item);
                return Ok(new PriceResponse(result));
            }
            catch (JsonSerializationException ex)
            {
                _context.Vtex.Logger.Error("RouteController", "GetPrice", "Error finding product price,", ex);
                return BadRequest("One or more query parameters are invalid.");
            }
            catch (Exception ex)
            {
                _context.Vtex.Logger.Error("RouteController", "GetPrice", "Error finding product price,", ex);
                return Problem("Unexpected error.");
            }
        }
    }
}