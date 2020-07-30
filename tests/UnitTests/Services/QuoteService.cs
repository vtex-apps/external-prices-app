using Moq;
using NUnit.Framework;
using service.Models;
using service.Models.DTO;
using service.Services.Impl;
using service.Services.Remote;

namespace UnitTests.Services
{
    [TestFixture]
    public class QuoteServiceTest
    {
        private QuoteService _quoteService;
        private Mock<IERPService> _erpServiceMock;

        [SetUp]
        public void Setup()
        {
            _erpServiceMock = new Mock<IERPService>();
            _quoteService = new QuoteService(_erpServiceMock.Object);
        }

        [Test]
        public void GetPrice_InputValid_ReturnQuote()
        {
            var quote = new Quote();
            _erpServiceMock.Setup(x => x.GetPrice(It.IsAny<Product>())).ReturnsAsync(quote);
            
            var ret = _quoteService.GetPrice(new ProductDTO()).Result;
            Assert.AreSame(quote, ret);
        }
        
    }
}