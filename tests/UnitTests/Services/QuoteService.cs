using Moq;
using NUnit.Framework;
using service.Models;
using service.Models.DTO;
using service.Services;
using service.Services.Remote;

namespace UnitTests.Services
{
    [TestFixture]
    public class QuoteServiceTest
    {
        private ProductService _productService;
        private Mock<IERPService> _erpServiceMock;

        [SetUp]
        public void Setup()
        {
            _erpServiceMock = new Mock<IERPService>();
            _productService = new ProductService(_erpServiceMock.Object);
        }

        [Test]
        public void GetPrice_InputValid_ReturnQuote()
        {
            var quote = new Quote();
            _erpServiceMock.Setup(x => x.GetPrice(It.IsAny<Product>())).ReturnsAsync(quote);
            
            var ret = _productService.GetPrice(new ProductDTO()).Result;
            Assert.AreSame(quote, ret);
        }
        
    }
}