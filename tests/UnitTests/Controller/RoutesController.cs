using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using service.Controllers;
using service.Models;
using service.Services;
using service.Util.Provider;

namespace UnitTests.Controller
{
    [TestFixture]
    public class RoutesControllerTest
    {
        
        private Mock<IProductService> _productServiceMock;
        private RoutesController _routesController;
        private Mock<IRequestProvider> _requestProvider;

        [SetUp]
        public void Setup()
        {
            _productServiceMock = new Mock<IProductService>();
            _requestProvider = new Mock<IRequestProvider>();
            _routesController = new RoutesController(null, _productServiceMock.Object, _requestProvider.Object);
            _routesController.ControllerContext = new ControllerContext();
            _routesController.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Test]
        public void GetPrice_InputValid_ReturnQuote()
        {
            
            var quoteDtoReq = new QuoteDto();
            var body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(quoteDtoReq)));
            _routesController.ControllerContext.HttpContext.Request.Body = body;
            _requestProvider.Setup(x => x.ReadJsonStream<QuoteDto>(body)).ReturnsAsync(quoteDtoReq);
            
            var quoteDtoResp = new QuoteDto();
            _productServiceMock.Setup(x => x.GetQuote(quoteDtoReq)).ReturnsAsync(quoteDtoResp);
            
            var ret = _routesController.GetPrice().Result;
            Assert.AreEqual(ret.StatusCode, (int)HttpStatusCode.OK);
        }
        
    }
}