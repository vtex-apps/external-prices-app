﻿using Microsoft.AspNetCore.Mvc;
using Vtex.Api.Context;
using System.Threading.Tasks;
using System;
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

        public RoutesController(IIOServiceContext context, IProductService productService, IRequestProvider requestProvider)
        {
            _context = context;
            _productService = productService;
            _requestProvider = requestProvider;
        }

        [HttpPost]
        public async Task<JsonResult> GetPrice()
        {
            try
            {
                var quoteDto = await _requestProvider.ReadJsonStream<QuoteDto>(Request.Body);
                var result = await _productService.GetQuote(quoteDto);
                return new JsonResult(new {Message = "Price quoted successfully.", Schema = result})
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
    }
}