using System.Collections.Generic;
using System.Runtime.Serialization;

namespace service.Models.Request
{
    [DataContract]
    public class RequestContext
    {
        [DataMember(Name = "email")] public string Email { get; set; }

        [DataMember(Name = "sessionParams")]
        public Dictionary<string, string> CustomSessionKeys { get; set; } // from session
    }
}