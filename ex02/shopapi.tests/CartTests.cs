using FluentAssertions;
using shopapi.Facades;
using shopapi.Models;
using System.Collections.Generic;
using Xunit;

namespace shopapi.tests
{
    public class CartTests
    {
        [Theory(DisplayName = "Add product to cart.")]
        [ClassData(typeof(AddProductsToCartAllDataAcorrect))]
        public void SystemCanAddAProductToCartCorrectly(CartInfo currentCart, ProductInfo product, int amount, CartInfo expected, double rawPrice, double discount, double total)
            => validateAddAProductToCart(currentCart, product, amount, expected, rawPrice, discount, total);

        [Theory(DisplayName = "Add free product to cart.")]
        [ClassData(typeof(AddFreeProductToCart))]
        public void SystemCanTrackFreeProductCartCorrectly(CartInfo currentCart, ProductInfo product, int amount, CartInfo expected, double rawPrice, double discount, double total)
            => validateAddAProductToCart(currentCart, product, amount, expected, rawPrice, discount, total);

        private void validateAddAProductToCart(CartInfo currentCart, ProductInfo product, int amount, CartInfo expected, double rawPrice, double discount, double totalPrice)
        {
            var sut = new CartFacade();
            var actual = sut.AddProductToCart(currentCart, product, amount);
            expected.Should().BeEquivalentTo(actual);
            rawPrice.Should().Be(actual.RawPrice);
            discount.Should().Be(actual.Discount);
            totalPrice.Should().Be(actual.Price);
        }
    }

    public class AddProductsToCartAllDataAcorrect : TheoryData<CartInfo, ProductInfo, int, CartInfo, double, double, double>
    {
        public AddProductsToCartAllDataAcorrect()
        {
            Add(new CartInfo(), new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 1,
                new CartInfo
                {
                    Products = new[] { new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 1) },
                }, 1000, 0, 1000);

            Add(new CartInfo(), new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 3,
                new CartInfo
                {
                    Products = new[] { new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 3) },
                }, 3000, 0, 3000);

            Add(new CartInfo
            {
                Products = new List<CartProduct>
                {
                    new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 1),
                    new CartProduct(new ProductInfo { Id = 2, Name = "bPhone", Price = 1000 }, 1),
                }
            }, new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 1,
            new CartInfo
            {
                Products = new[]
                {
                    new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 1),
                    new CartProduct(new ProductInfo { Id = 2, Name = "bPhone", Price = 1000 }, 1),
                    new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 1),
                },
            }, 3000, 0, 3000);
        }
    }

    public class AddFreeProductToCart : TheoryData<CartInfo, ProductInfo, int, CartInfo, double, double, double>
    {
        public AddFreeProductToCart()
        {
            Add(new CartInfo
            {
                Products = new List<CartProduct>
                 {
                     new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 1),
                     new CartProduct(new ProductInfo { Id = 2, Name = "bPhone", Price = 1000 }, 1),
                     new CartProduct(new ProductInfo { Id = 3, Name = "cPhone", Price = 1000 }, 1),
                 }
            }, new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 1,
            new CartInfo
            {
                Products = new[]
                {
                     new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 1),
                     new CartProduct(new ProductInfo { Id = 2, Name = "bPhone", Price = 1000 }, 1),
                     new CartProduct(new ProductInfo { Id = 3, Name = "cPhone", Price = 1000 }, 1),
                     new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 1, 1000),
                },
            }, 4000, 1000, 3000);

            Add(new CartInfo
            {
                Products = new List<CartProduct>
                 {
                     new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 3),
                 }
            }, new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 1,
            new CartInfo
            {
                Products = new[]
                {
                     new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 3),
                     new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 1, 1000),
                },
            }, 4000, 1000, 3000);

            Add(new CartInfo
            {
                Products = new List<CartProduct>
                  {
                      new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 3),
                  }
            }, new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 4,
            new CartInfo
            {
                Products = new[]
                {
                        new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 3),
                        new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 4, 1000),
                },
            }, 7000, 1000, 6000);

            Add(new CartInfo
            {
                Products = new List<CartProduct>
                  {
                      new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 3),
                  }
            }, new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 5,
            new CartInfo
            {
                Products = new[]
                {
                        new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 3),
                        new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 5, 2000),
                },
            }, 8000, 2000, 6000);

            Add(new CartInfo
            {
                Products = new List<CartProduct>
                {
                    new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 4, 1000),
                }
            }, new ProductInfo { Id = 1, Name = "aPhone", Price = 100 }, 4,
            new CartInfo
            {
                Products = new[]
                {
                    new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 4, 1000),
                    new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 100 }, 4, 100),
                },
            }, 4400, 1100, 3300);

            Add(new CartInfo
            {
                Products = new List<CartProduct>
                {
                    new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 4, 1000),
                    new CartProduct(new ProductInfo { Id = 2, Name = "bPhone", Price = 1000 }, 1),
                }
            }, new ProductInfo { Id = 1, Name = "aPhone", Price = 100 }, 7,
            new CartInfo
            {
                Products = new[]
                {
                    new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 4, 1000),
                    new CartProduct(new ProductInfo { Id = 2, Name = "bPhone", Price = 1000 }, 1),
                    new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 100 }, 7, 200),
                },
            }, 5700, 1200, 4500);
        }
    }
}
