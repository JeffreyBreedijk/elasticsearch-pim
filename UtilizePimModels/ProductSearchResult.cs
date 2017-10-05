using System.Collections.Generic;

namespace UtilizePimModels
{
    public class ProductSearchResult
    {
        public long TotalItems { get; set; }
        public List<Product> Products { get; set; }
        public Dictionary<string, Dictionary<string, long>> StringAggregations { get; set; }
        public Dictionary<string, NumericAggregation> NumericAggregations { get; set; }
    }

    public class NumericAggregation
    {
        public double? Min { get; set; }
        public double? Max { get; set; }
        public double? Avg { get; set; }
    }
}