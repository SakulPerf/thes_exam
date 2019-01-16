using shopapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace shopapi.Facades
{
    public class CartFacade
    {
        public CartInfo AddProductToCart(CartInfo currentCart, ProductInfo product, int amount)
        {
            const int MinimumProductAmount = 1;
            var areArgumentsValid = currentCart != null
                && product != null
                && amount >= MinimumProductAmount;
            if (!areArgumentsValid)
            {
                return new CartInfo();
            }

            currentCart.Products = currentCart.Products ?? new List<CartProduct>();

            const int FreeProductFactor = 4;
            var totalProductsInCart = currentCart.Products.Sum(it => it.Amount);
            var discountedAmount = totalProductsInCart / FreeProductFactor;
            var totalProducts = totalProductsInCart + amount;
            var totalDiscountAmount = totalProducts / FreeProductFactor;
            var currentDiscountAmount = Math.Abs(totalDiscountAmount - discountedAmount);
            var discountPrice = product.Price * currentDiscountAmount;
            currentCart.Products.Add(new CartProduct(product, amount, discountPrice));
            return currentCart;
        }
    }
}
