using System.Net;
using System.Net.Http;
using Moq;
using NUnit.Framework;
using service.Models;
using service.Services.Remote.Impl;
using service.Util.Provider;
using service.Util.Provider.Impl;

namespace UnitTests.Services.Remote
{
    
    [TestFixture]
    public class ERPServiceTest
    {
        
        private Mock<IRequestProvider> _requestProviderMock;
        private ERPService _erpService;

        [SetUp]
        public void Setup()
        {
            _requestProviderMock = new Mock<IRequestProvider>();
            _erpService = new ERPService(null, _requestProviderMock.Object);
        }

        [Test]
        public void GetPrice_InputValid_ReturnQuote()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Accepted) {Content = new StringContent("{}")};
            _requestProviderMock.Setup(x => x.SendAsync(It.IsAny<string>(), It.IsAny<HttpMethod>(), null)).ReturnsAsync(response);
            
            var quote = _erpService.GetPrice(new Product());
            Assert.IsNotNull(quote);
        }
        
    }
}