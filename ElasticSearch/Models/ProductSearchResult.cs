using System.Collections.Generic;

namespace ElasticSearch.Models
{
    public class ProductSearchResult
    {
        public long TotalItems { get; set; }
        public List<Product> Products { get; set; }
        public Dictionary<string, Dictionary<string, long>> Aggregations { get; set; }
    }
}