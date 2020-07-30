using Microsoft.AspNetCore.Mvc;
using Vtex.Api.Context;
using System.Threading.Tasks;
using System;
using service.Models;
using service.Models.DTO;
using service.Services;
using service.Services.Impl;
using service.Util.Provider;
using service.Util.Provider.Impl;

namespace service.Controllers
{
    public class RoutesController : Controller
    {
        private readonly IIOServiceContext  _context;
        private readonly IQuoteService _quoteService;
        private readonly IRequestProvider _requestProvider;

        public RoutesController(IIOServiceContext context, IQuoteService quoteService, IRequestProvider requestProvider)
        {
            _context = context;
            _quoteService = quoteService;
            _requestProvider = requestProvider;
        }

        [HttpGet]
        private async Task<JsonResult> GetQuotes()
        {
            try
            {
                var productDTO = await _requestProvider.ReadJsonStream<ProductDTO>(Request.Body);
                var quote = await _quoteService.GetPrice(productDTO);
                return new JsonResult(new {Message = "Schema created/updated successfully.", Schema = quote}) {StatusCode = 200};
            }
            catch (Exception ex)
            {
                _context.Vtex.Logger.Error("RouteController", "SaveSchema", "Error saving schema", ex);
                return new JsonResult("Unexpected error.") {StatusCode = 500};
            }
        }
    }
}
