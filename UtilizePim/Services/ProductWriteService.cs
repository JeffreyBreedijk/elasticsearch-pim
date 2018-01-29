using Nest;
using UtilizePim.Models;
using UtilizePim.Services.ElasticSearch;

namespace UtilizePim.Services
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
            return x.Result.Equals(Nest.Result.Created) || x.Result.Equals(Nest.Result.Updated);
        }
       
    }
}