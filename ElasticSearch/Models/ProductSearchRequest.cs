using System.Collections.Generic;

namespace ElasticSearch.Models
{
    public class ProductSearchRequest
    {
        public Dictionary<string, string> Properties;
        public List<string> Aggregations;

    }
}