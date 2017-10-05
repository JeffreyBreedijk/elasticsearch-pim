using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using ElasticSearch.Config;
using ElasticSearch.Services.ElasticSearch;
using Microsoft.Extensions.Options;
using Nest;
using UtilizePimModels;

namespace ElasticSearch.Services
{
    public interface IProductWriteService
    {
        bool StoreProduct(Product product);
    }
    
    public class ProductWriteServiceElastic : IProductWriteService
    {
        private readonly ElasticClient _client;

        public ProductWriteServiceElastic(IElasticSearchClientManager clientManager)
        {
            _client = clientManager.GetClient();
        }
        
        public bool StoreProduct(Product product)
        {
            
            var x =  _client.Index(product);
                return x.Created;
        }
       
    }
}