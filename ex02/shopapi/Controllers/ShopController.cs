using Microsoft.AspNetCore.Mvc;
using shopapi.Facades;
using shopapi.Models;
using System.Collections.Generic;

namespace shopapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private static IList<ProductInfo> products = new List<ProductInfo>();

        [HttpGet]
        public IList<ProductInfo> Get() => products;

        [HttpPost]
        public IList<ProductInfo> Post([FromBody] ProductInfo req) => new ShopFacade().AddNewProduct(products, req);
    }
}
