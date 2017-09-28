using System.Collections.Generic;
using System.Linq;
using ElasticSearch.Models;
using Nest;

namespace ElasticSearch.Services.ElasticSearch
{
    public static class ElasticProductSearchResultBuilder
    {
        public static ProductSearchResult BuildProductSearchResult(ISearchResponse<Product> searchResponse)
        {
            return new ProductSearchResult()
            {
                TotalItems = searchResponse.Total,
                Products = searchResponse.Documents.ToList(),
                Aggregations = ExtractAggregations(searchResponse.Aggregations)
            };
        }

        private static Dictionary<string, Dictionary<string, long>> ExtractAggregations(IReadOnlyDictionary<string, IAggregate> aggregates)
        {
            var aggs = new Dictionary<string, Dictionary<string, long>>();
            foreach (var y in aggregates)
            {
                if (y.Value.GetType() != typeof(Nest.BucketAggregate)) continue;
                var agg = new Dictionary<string, long>();
                foreach (var bucket in ((Nest.BucketAggregate) y.Value).Items)
                {
                    if (bucket.GetType() != typeof(KeyedBucket<object>)) continue;
                    var docCount = ((KeyedBucket<object>) bucket).DocCount;
                    if (docCount == null) continue;
                    agg.Add((string) ((KeyedBucket<object>) bucket).Key, docCount.Value);
                }
                aggs.Add(y.Key, agg);
            }
            return aggs;
        }
    }
}