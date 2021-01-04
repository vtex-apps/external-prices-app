using System.Runtime.Serialization;

namespace service.Models.Request
{
    [DataContract]
    public class Request
    {
        public QuoteDto Item { get; set; }

        //TODO implement get function with session parameters - custom session keys
        public RequestContext Context { get; set; }
    }
}