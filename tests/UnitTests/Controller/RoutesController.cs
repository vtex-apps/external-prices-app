using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using service.Controllers;
using service.Models;
using service.Services;

namespace UnitTests.Controller
{
    [TestFixture]
    public class RoutesControllerTest
    {
        
        private Mock<IProductService> _productServiceMock;
        private RoutesController _routesController;

        [SetUp]
        public void Setup()
        {
            _productServiceMock = new Mock<IProductService>();
            _routesController = new RoutesController(null, _productServiceMock.Object);
            _routesController.ControllerContext = new ControllerContext();
            _routesController.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Test]
        public void GetPrice_InputValid_ReturnQuote()
        {
            var queryParams = new Dictionary<string, StringValues>();
            queryParams["email"] = "test";
            _routesController.ControllerContext.HttpContext.Request.Query = new QueryCollection(queryParams);
            
            var quoteDto = new QuoteDto();
            var erpQuoteDto = new ErpQuoteDto();
            _productServiceMock.Setup(x => x.GetQuote(quoteDto)).ReturnsAsync(erpQuoteDto);
            
            var ret = _routesController.GetPrice().Result;
            Assert.AreEqual(ret.StatusCode, (int)HttpStatusCode.OK);
        }
        
    }
}