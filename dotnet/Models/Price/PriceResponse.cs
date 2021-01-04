using System.Runtime.Serialization;

namespace service.Models.Price
{
    [DataContract]
    public class PriceResponse
    {
        public PriceResponse(QuoteDto quoteDto)
        {
            Item = quoteDto;
            Message = "Price quoted successfully.";
        }

        [DataMember(Name = "message")] public string Message { get; set; }
        [DataMember(Name = "item")] public QuoteDto Item { get; set; }
    }
}