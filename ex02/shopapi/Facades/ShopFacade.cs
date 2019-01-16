using shopapi.Models;
using System.Collections.Generic;

namespace shopapi.Facades
{
    public class ShopFacade
    {
        public IList<ProductInfo> AddNewProduct(IList<ProductInfo> products, ProductInfo product)
        {
            const int MinimumProductPrice = 0;
            var isProductValid = product != null
                && !string.IsNullOrEmpty(product.Name)
                && product.Price > MinimumProductPrice;
            if (!isProductValid)
            {
                return products;
            }

            product.Id = products.Count + 1;
            products.Add(product);
            return products;
        }
    }
}
