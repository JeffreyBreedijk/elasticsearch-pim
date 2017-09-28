﻿using System.Collections.Generic;
using ElasticSearch.Models;
using ElasticSearch.Services.ElasticSearch;
using Nest;
using static ElasticSearch.Services.ElasticSearch.ElasticAggregationHelper;
using static ElasticSearch.Services.ElasticSearch.ElasticProductSearchResultBuilder;
using static ElasticSearch.Services.ElasticSearch.ElasticQueryHelper;


namespace ElasticSearch.Services
{
    public interface IProductQueryService
    {
        Product GetProductById(string id);

        ProductSearchResult FindByStringAndProperty(string searchString, string lang, int from, int size,
            Dictionary<string, string> properties, List<string> stringAggregations);
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
            Dictionary<string, string> properties, List<string> stringAggregations)
        {
            return BuildProductSearchResult(_client.Search<Product>(new SearchRequest<Product>
            {
                Query = QueryBuilder(searchString, lang, properties),
                Aggregations = AggregationBuilder(stringAggregations),
                From = from,
                Size = size
            }));

        }

        }
    }
