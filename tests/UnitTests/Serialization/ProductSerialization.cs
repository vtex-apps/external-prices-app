using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using service.Models;

namespace UnitTests.Serialization
{
    [TestFixture]
    public class ProductSerialization
    {
        
        [Test]
        public void Deserialize_InputRandomValues_ReturnProduct()
        {
            var product = new Fixture().Create<Product>();
            var json = JsonConvert.SerializeObject(product);
            var deserializedMessage = JsonConvert.DeserializeObject<Product>(json);
            deserializedMessage.Should().BeEquivalentTo(product);
        }
        
    }
}