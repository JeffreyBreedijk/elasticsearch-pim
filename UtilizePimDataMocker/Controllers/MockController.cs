using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UtilizePimDataMocker.Service;

namespace UtilizePimDataMocker.Controllers
{
    [Route("api/[controller]")]
    public class MockController : Controller
    {
        private readonly IMockProductSender _productSender;

        public MockController(IMockProductSender sender)
        {
            _productSender = sender;
        }
        
        
        [HttpPost]
        public async Task<HttpStatusCode> Generate([FromQuery] int count)
        {
            var product = MockProductBuilder.GenerateProduct();
            var result = await _productSender.SendProduct(product);
            return result;
        }
    }
}