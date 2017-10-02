using System.Collections.Generic;

namespace ElasticSearch.Models
{
    public class ProductSearchRequest
    {
        public Dictionary<string, string> StringProperties { get; set; }
        public Dictionary<string, NumericQuery> NumericProperties { get; set; }
        public string Category { get; set; }
        public List<string> Aggregations { get; set; }

    }

    public class NumericQuery
    {
        public double? GreaterThanOrEqual { get; set; }
        public double? LessThanOrEqual { get; set; }
    }
}