using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cardinal_webservices.Data 
{
    public class Test 
    {
        [Key, Column("name")]
        public string Name { get; set; }
    }
}