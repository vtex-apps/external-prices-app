using System.IO;
using System.Net;
using Moq;
using NUnit.Framework;
using service.Controllers;
using service.Models;
using service.Models.DTO;
using service.Services;
using service.Services.Remote;
using service.Util.Provider;

namespace UnitTests.Controller
{
    [TestFixture]
    public class RoutesControllerTest
    {
        
        private Mock<IProductService> _productServiceMock;
        private Mock<IRequestProvider> _requestProviderMock;
        private RoutesController _routesController;

        [SetUp]
        public void Setup()
        {
            _requestProviderMock = new Mock<IRequestProvider>();
            _productServiceMock = new Mock<IProductService>();
            _routesController = new RoutesController(null, _productServiceMock.Object, _requestProviderMock.Object);
        }

        [Test]
        public void GetPrice_InputValid_ReturnQuote()
        {
            var productDTO = new ProductDTO();
            var quote = new Quote();
            _requestProviderMock.Setup(x => x.ReadJsonStream<ProductDTO>(It.IsAny<Stream>())).ReturnsAsync(productDTO);
            _productServiceMock.Setup(x => x.GetPrice(productDTO)).ReturnsAsync(quote);

            var ret = _routesController.GetPrice().Result;
            Assert.AreEqual(ret.StatusCode, (int)HttpStatusCode.OK);
        }
        
    }
}