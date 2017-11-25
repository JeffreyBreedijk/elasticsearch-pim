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
        public HttpResponseMessage Generate([FromQuery] int count)
        {
            for (var i = 0; i < count; i++)
            {
                var product = MockProductBuilder.GenerateProduct();
                var result = _productSender.SendProduct(product);
            }
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}