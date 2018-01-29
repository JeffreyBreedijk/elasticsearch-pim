using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using UtilizePim.Models;
using UtilizePim.Services;

namespace UtilizePim.Controllers
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
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            return _productWriteService.StoreProduct(product) ? StatusCode(200) : StatusCode(500);
        }
        
        [HttpDelete]
        [Route("{productId}")]
        public IActionResult DeleteProduct(string productId)
        {
            return _productWriteService.DeleteProduct(productId) ? StatusCode(200) : StatusCode(500);
        }
    }
}