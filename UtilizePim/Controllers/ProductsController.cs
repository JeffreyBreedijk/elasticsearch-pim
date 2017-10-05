using ElasticSearch.Services;
using Microsoft.AspNetCore.Mvc;
using UtilizePimModels;

namespace ElasticSearch.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductQueryService _productQueryService;
        private readonly IProductWriteService _productWriteService;

        public ProductsController(IProductQueryService productQueryService, IProductWriteService productWriteService)
        {
            _productQueryService = productQueryService;
            _productWriteService = productWriteService;
        }

        [HttpGet]
        [Route("{productId}")]
        public object GetSingle(string productId)
        {

            return _productQueryService.GetProductById(productId);
        }
        
        [HttpPost]
        public object GetBySearchStringAndProperties([FromQuery] string query, [FromQuery] int from, 
            [FromQuery] int size, [FromQuery] string lang, [FromBody] ProductSearchRequest requestBody)
        {
            if (requestBody == null) requestBody = new ProductSearchRequest();
            return _productQueryService.FindByStringAndProperty(query, string.IsNullOrEmpty(lang) ? "default" : 
                lang, from, size == 0 ? 10 : size, requestBody.StringProperties, requestBody.NumericProperties, requestBody.Category, requestBody.StringAggregations, requestBody.NumericAggregations);
        }
 

        [HttpPut]
        public void UpdateProduct([FromBody] Product product)
        {
            _productWriteService.StoreProduct(product);
        }
        
    }
}