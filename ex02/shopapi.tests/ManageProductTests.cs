using FluentAssertions;
using shopapi.Facades;
using shopapi.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace shopapi.tests
{
    public class ManageProductTests
    {
        [Theory(DisplayName = "Add new product.")]
        [ClassData(typeof(AddAProductAllDataCorrect))]
        public void SystemCanAddNewProduct(IEnumerable<ProductInfo> allProducts, ProductInfo newProduct, IEnumerable<ProductInfo> expected)
            => validateAddNewProduct(allProducts, newProduct, expected);

        [Theory(DisplayName = "Add new product (some data are invalid).")]
        [ClassData(typeof(AddAProductSomeDataIncorrect))]
        public void SystemDoNothingWhenAddAnIncorrectProduct(IEnumerable<ProductInfo> allProducts, ProductInfo newProduct, IEnumerable<ProductInfo> expected)
            => validateAddNewProduct(allProducts, newProduct, expected);

        [Theory(DisplayName = "Add new product (existing products).")]
        [ClassData(typeof(AddAProductWithExistingProducts))]
        public void SystemCanGenerateProductIdCorrectly(IEnumerable<ProductInfo> allProducts, ProductInfo newProduct, IEnumerable<ProductInfo> expected)
            => validateAddNewProduct(allProducts, newProduct, expected);

        private void validateAddNewProduct(IEnumerable<ProductInfo> allProducts, ProductInfo newProduct, IEnumerable<ProductInfo> expected)
        {
            var actual = allProducts.ToList();
            var sut = new ShopFacade();
            sut.AddNewProduct(actual, newProduct);
            expected.Should().BeEquivalentTo(actual);
        }
    }

    public class AddAProductAllDataCorrect : TheoryData<IEnumerable<ProductInfo>, ProductInfo, IEnumerable<ProductInfo>>
    {
        public AddAProductAllDataCorrect()
        {
            Add(Enumerable.Empty<ProductInfo>(), new ProductInfo { Name = "iPhone", Price = 1000 }, new[] { new ProductInfo { Id = 1, Name = "iPhone", Price = 1000 } });
            Add(Enumerable.Empty<ProductInfo>(), new ProductInfo { Name = "iPhone", Price = 1 }, new[] { new ProductInfo { Id = 1, Name = "iPhone", Price = 1 } });
        }
    }

    public class AddAProductWithExistingProducts : TheoryData<IEnumerable<ProductInfo>, ProductInfo, IEnumerable<ProductInfo>>
    {
        public AddAProductWithExistingProducts()
        {
            Add(new[]
            {
                new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 },
                new ProductInfo { Id = 2, Name = "bPhone", Price = 1000 },
                new ProductInfo { Id = 3, Name = "cPhone", Price = 1000 },
            },
            new ProductInfo { Name = "iPhone", Price = 1000 },
            new[]
            {
                new ProductInfo { Id = 1, Name = "aPhone", Price = 1000 },
                new ProductInfo { Id = 2, Name = "bPhone", Price = 1000 },
                new ProductInfo { Id = 3, Name = "cPhone", Price = 1000 },
                new ProductInfo { Id = 4, Name = "iPhone", Price = 1000 }
            });
        }
    }

    public class AddAProductSomeDataIncorrect : TheoryData<IEnumerable<ProductInfo>, ProductInfo, IEnumerable<ProductInfo>>
    {
        public AddAProductSomeDataIncorrect()
        {
            Add(Enumerable.Empty<ProductInfo>(), new ProductInfo { Name = null, Price = 1000 }, new ProductInfo[0]);
            Add(Enumerable.Empty<ProductInfo>(), new ProductInfo { Name = "", Price = 1000 }, new ProductInfo[0]);
            Add(Enumerable.Empty<ProductInfo>(), new ProductInfo { Name = "iPhone", Price = 0 }, new ProductInfo[0]);
        }
    }
}
