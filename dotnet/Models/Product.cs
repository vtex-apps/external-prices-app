using System.Runtime.Serialization;
using service.Models.DTO;

namespace service.Models
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public string SKU { get; set; }

        public static Product From(ProductDTO dto)
        {
            return new Product();
        }
    }
}