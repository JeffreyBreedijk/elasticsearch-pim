using System.Collections.Generic;
using Nest;

namespace ElasticSearch.Models
{
    public class Product
    {
        public string Id { get; set; }
        public Dictionary<string, string> ShortDescription { get; set; }
        public Dictionary<string, string> LongDescription { get; set; }
        public Dictionary<string, string> Unit { get; set; }
        public Dictionary<string, object> Properties { get; set; } 
        public List<string> Categories { get; set; }
        
    }
}