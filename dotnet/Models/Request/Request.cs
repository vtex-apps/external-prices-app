using System.Runtime.Serialization;

namespace service.Models.Request
{
    [DataContract]
    public class Request
    {
        [DataMember(Name = "item")] public QuoteDto Item { get; set; }

        //TODO implement get function with session parameters - custom session keys
        [DataMember(Name = "context")] public RequestContext Context { get; set; }
    }
}