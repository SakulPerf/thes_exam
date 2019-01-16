using System.Linq;
using Microsoft.AspNetCore.Mvc;
using shopapi.Facades;
using shopapi.Models;
using System.Collections.Generic;

namespace shopapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private static CartInfo cart = new CartInfo { Products = new List<CartProduct>() };

        [HttpGet]
        public CartInfo Get() => cart;

        [HttpPost]
        public CartInfo Post([FromBody] AddProductRequest req)
        {
            if (req == null)
            {
                return cart;
            }

            var products = new ShopController().Get();
            var selectedProduct = products.FirstOrDefault(it => it.Id == req.ProductId);
            return new CartFacade().AddProductToCart(cart, selectedProduct, req.Amount);
        }
    }
}
