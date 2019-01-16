using shopapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapi.Facades
{
    public class ShopFacade
    {
        private IList<ProductInfo> products;

        public ShopFacade()
        {
            products = new List<ProductInfo>();
        }

        public ShopFacade(IEnumerable<ProductInfo> products)
        {
            this.products = products.ToList();
        }

        public IEnumerable<ProductInfo> GetAllProducts() => products;

        public void AddNewProduct(ProductInfo product)
        {
            const int MinimumProductPrice = 0;
            var isProductValid = product != null
                && !string.IsNullOrEmpty(product.Name)
                && product.Price > MinimumProductPrice;
            if (!isProductValid)
            {
                return;
            }

            products.Add(product);
        }
    }
}
