using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RestSharp;
using UtilizePimDataMocker.Config;
using UtilizePimModels;

namespace UtilizePimDataMocker.Service
{
    public interface IMockProductSender
    {
        Task<HttpStatusCode> SendProduct(Product mockProduct);
    }
    
    public class MockProductSender : IMockProductSender
    {
        private readonly PimConfig _pimConfig;
        
        public MockProductSender(IOptions<PimConfig> pimConfig)
        {
            _pimConfig = pimConfig.Value;
        }
        
        public async Task<HttpStatusCode> SendProduct(Product mockProduct)
        {
            var client = new RestClient(_pimConfig.Uri);
            var request = new RestRequest("api/products", Method.PUT);
            request.AddJsonBody(mockProduct);
            var taskCompletion = new TaskCompletionSource<IRestResponse>();
            client.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            return (await taskCompletion.Task).StatusCode;

        }
    }
}