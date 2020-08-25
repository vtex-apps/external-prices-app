using System.Runtime.Serialization;

namespace service.Models
{
    [DataContract]
    public class ErpQuoteDto
    {
        [DataMember(Name = "sku")] public string Sku { get; set; }

        [DataMember(Name = "quantity")] public string Quantity { get; set; }

        [DataMember(Name = "currency")] public string Currency { get; set; }

        [DataMember(Name = "email")] public string Email { get; set; }

        [DataMember(Name = "state")] public string State { get; set; }
        
        [DataMember(Name = "orderType")] public string OrderType { get; set; }
    }
}