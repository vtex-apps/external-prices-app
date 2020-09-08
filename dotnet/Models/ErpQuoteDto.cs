using System.Runtime.Serialization;

namespace service.Models
{
    [DataContract]
    public class ErpQuoteDto
    {
        [DataMember(Name = "sku")] public string Sku { get; set; }

        [DataMember(Name = "quantity")] public string Quantity { get; set; }

        [DataMember(Name = "state")] public string State { get; set; }

        [DataMember(Name = "orderType")] public string OrderType { get; set; }

        [DataMember(Name = "price")] public double Price { get; set; }
    }
}