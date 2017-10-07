using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using UtilizePim.Config;

namespace UtilizePim.Services.ElasticSearch
{
    public interface IElasticSearchClientManager
    {
        ElasticClient GetClient();
    }

    public class ElasticSearchClientManager : IElasticSearchClientManager
    {
        private readonly ElasticConfig _elasticConfig;
        private readonly ElasticClient _client;

        public ElasticSearchClientManager(IOptions<ElasticConfig> elasticConfig)
        {
            _elasticConfig = elasticConfig.Value;
            _client = InitClient();
        }

        public ElasticClient GetClient()
        {
            return _client;
        }
        
        private ElasticClient InitClient()
        {
            var settings = new ConnectionSettings(InitConnectionPool(_elasticConfig.Hosts))
                .DefaultIndex("products").BasicAuthentication(_elasticConfig.Username, _elasticConfig.Password);
            return new ElasticClient(settings);
        }

        private IConnectionPool InitConnectionPool(IReadOnlyList<string> hosts)
        {
            if (hosts.Count == 1)
            {
                return new SingleNodeConnectionPool(new Uri(hosts[0]));
            }
            return new SniffingConnectionPool(_elasticConfig.Hosts.Select(h => new Uri(h)).ToArray());    
        }
    }
}