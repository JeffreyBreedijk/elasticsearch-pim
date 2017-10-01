using System.Collections.Generic;

namespace ElasticSearch.Models
{
    public class ProductSearchRequest
    {
        public Dictionary<string, string> Properties { get; set; }
        public string Category { get; set; }
        public List<string> Aggregations { get; set; }

    }
}