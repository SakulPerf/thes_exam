using FluentAssertions;
using shopapi.Facades;
using shopapi.Models;
using System.Collections.Generic;
using Xunit;

namespace shopapi.tests
{
    public class ManageTests
    {
        [Theory(DisplayName = "Add new product.")]
        [ClassData(typeof(AddAProductAllDataCorrect))]
        public void SystemCanAddNewProduct(ProductInfo newProduct, IEnumerable<ProductInfo> expected)
            => validateAddNewProduct(newProduct, expected);

        [Theory(DisplayName = "Add new product (some data are invalid).")]
        [ClassData(typeof(AddAProductSomeDataIncorrect))]
        public void SystemDoNothingWhenAddAnIncorrectProduct(ProductInfo newProduct, IEnumerable<ProductInfo> expected)
            => validateAddNewProduct(newProduct, expected);

        private void validateAddNewProduct(ProductInfo newProduct, IEnumerable<ProductInfo> expected)
        {
            var sut = new ShopFacade();
            sut.AddNewProduct(newProduct);
            expected.Should().BeEquivalentTo(sut.GetAllProducts());
        }
    }

    public class AddAProductAllDataCorrect : TheoryData<ProductInfo, IEnumerable<ProductInfo>>
    {
        public AddAProductAllDataCorrect()
        {
            Add(new ProductInfo { Name = "iPhone", Price = 1000 }, new[] { new ProductInfo { Name = "iPhone", Price = 1000 } });
            Add(new ProductInfo { Name = "iPhone", Price = 1 }, new[] { new ProductInfo { Name = "iPhone", Price = 1 } });
        }
    }

    public class AddAProductSomeDataIncorrect : TheoryData<ProductInfo, IEnumerable<ProductInfo>>
    {
        public AddAProductSomeDataIncorrect()
        {
            Add(new ProductInfo { Name = null, Price = 1000 }, new ProductInfo[0]);
            Add(new ProductInfo { Name = "", Price = 1000 }, new ProductInfo[0]);
            Add(new ProductInfo { Name = "iPhone", Price = 0 }, new ProductInfo[0]);
        }
    }
}
