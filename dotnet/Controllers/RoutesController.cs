using Microsoft.AspNetCore.Mvc;
using Vtex.Api.Context;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using service.Models;
using service.Services;

namespace service.Controllers
{
    public class RoutesController : Controller
    {
        private readonly IIOServiceContext _context;
        private readonly IProductService _productService;

        public RoutesController(IIOServiceContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<JsonResult> GetPrice()
        {
            try
            {
                var quote = await _productService.GetQuote(BuildQuoteDto());
                return new JsonResult(new {Message = "Price quoted successfully.", Schema = quote})
                    {StatusCode = (int) HttpStatusCode.OK};
            }
            catch (JsonSerializationException ex)
            {
                _context.Vtex.Logger.Error("RouteController", "GetPrice", "Error finding product price,", ex);
                return new JsonResult("One or more query parameters are invalid.") {StatusCode = (int)HttpStatusCode.BadRequest};
            }
            catch (Exception ex)
            {
                _context.Vtex.Logger.Error("RouteController", "GetPrice", "Error finding product price,", ex);
                return new JsonResult("Unexpected error.") {StatusCode = (int)HttpStatusCode.InternalServerError};
            }
        }

        private QuoteDto BuildQuoteDto()
        {
            var parameters = HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value);
            var parametersDict = parameters.AllKeys.ToDictionary(k => k, k => parameters[k]);
            var jsonString = JsonConvert.SerializeObject(parametersDict);
            return JsonConvert.DeserializeObject<QuoteDto>(jsonString);
        }
    }
}