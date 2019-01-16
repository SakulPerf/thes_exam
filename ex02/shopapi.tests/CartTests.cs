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
        public void SystemCanAddAProductToCartCorrectly(CartInfo currentCart, ProductInfo product, int amount, CartInfo expected)
            => validateAddAProductToCart(currentCart, product, amount, expected);

        [Theory(DisplayName = "Add free product to cart.")]
        [ClassData(typeof(AddFreeProductToCart))]
        public void SystemCanTrackFreeProductCartCorrectly(CartInfo currentCart, ProductInfo product, int amount, CartInfo expected)
            => validateAddAProductToCart(currentCart, product, amount, expected);

        private void validateAddAProductToCart(CartInfo currentCart, ProductInfo product, int amount, CartInfo expected)
        {
            var sut = new CartFacade();
            var actual = sut.AddProductToCart(currentCart, product, amount);
            expected.Should().BeEquivalentTo(actual);
        }
    }

    public class AddProductsToCartAllDataAcorrect : TheoryData<CartInfo, ProductInfo, int, CartInfo>
    {
        public AddProductsToCartAllDataAcorrect()
        {
            Add(new CartInfo(), new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 1,
                new CartInfo
                {
                    Products = new[] { new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 1) },
                });

            Add(new CartInfo(), new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 3,
                new CartInfo
                {
                    Products = new[] { new CartProduct(new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 }, 3) },
                });

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
            });
        }
    }

    public class AddFreeProductToCart : TheoryData<CartInfo, ProductInfo, int, CartInfo>
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
            });

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
            });

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
           });

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
           });

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
            });

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
            });
        }
    }
}
