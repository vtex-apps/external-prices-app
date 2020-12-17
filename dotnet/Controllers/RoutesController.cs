using Microsoft.AspNetCore.Mvc;
using Vtex.Api.Context;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using service.Models;
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

        [HttpPost]
        public async Task<JsonResult> GetPrice([FromBody] QuoteDto quoteDto)
        {
            try
            {
                var result = await _productService.GetQuote(quoteDto);
                return new JsonResult(new {Message = "Price quoted successfully.", Schema = result})
                    {StatusCode = (int) HttpStatusCode.OK};
            }
            catch (JsonSerializationException ex)
            {
                _context.Vtex.Logger.Error("RouteController", "GetPrice", "Error finding product price,", ex);
                return new JsonResult("One or more query parameters are invalid.")
                    {StatusCode = (int) HttpStatusCode.BadRequest};
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _context.Vtex.Logger.Error("RouteController", "GetPrice", "Error finding product price,", ex);
                return new JsonResult("Unexpected error.") {StatusCode = (int) HttpStatusCode.InternalServerError};
            }
        }

        private async Task<string> GetRequestBody()
        {
            using var sr = new StreamReader(Request.Body);
            var body = await sr.ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(body)) Console.WriteLine("error");
            // throw new InvalidHttpParameterException("You must provide a valid body.", "request body");
            return body;
        }
    }
}