using System.Collections.Generic;
using System.Runtime.Serialization;

namespace service.Models
{
    public class Request
    {
        public QuoteDto Item { get; set; }

        //TODO implement get function with session parameters - custom session keys
        public RequestContext Context { get; set; }
        

        [DataContract]
        public class RequestContext
        {
            [DataMember(Name = "email")] public string Email { get; set; }

            [DataMember(Name = "sessionParams")]
            public Dictionary<string, string> CustomSessionKeys { get; set; } // from session
        }
    }
}