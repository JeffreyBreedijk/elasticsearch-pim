using System.Collections.Generic;
using Nest;
using UtilizePim.Models;
using UtilizePim.Services.ElasticSearch;

namespace UtilizePim.Services
{
    public interface IProductQueryService
    {
        Product GetProductById(string id);

        ProductSearchResult FindByStringAndProperty(string searchString, string lang, int from, int size,
            Dictionary<string, string> stringProperties, 
            Dictionary<string, NumericQuery> numericProperties, 
            string category, List<string> stringAggregations, List<string> numericAggregations);
    }

    public class ProductQueryServiceElastic : IProductQueryService
    {
        private readonly ElasticClient _client;

        public ProductQueryServiceElastic(IElasticSearchClientManager clientManager)
        {
            _client = clientManager.GetClient();
        }

        public Product GetProductById(string id)
        {
            return _client.Get<Product>(id).Source;
        }

        public ProductSearchResult FindByStringAndProperty(string searchString, string lang, int from, int size,
            Dictionary<string, string> stringProperties, 
            Dictionary<string, NumericQuery> numericProperties, 
            string category, 
            List<string> stringAggregations, List<string> numericAggregations)
        {
            var x = _client.Search<Product>(new SearchRequest<Product>
            {
                Query = ElasticQueryHelper.QueryBuilder(searchString, lang, stringProperties, numericProperties, category),
                Aggregations = ElasticAggregationHelper.AggregationBuilder(stringAggregations, numericAggregations),
                From = from,
                Size = size
            });
            return ElasticProductSearchResultBuilder.BuildProductSearchResult(x);

        }

        }
    }
