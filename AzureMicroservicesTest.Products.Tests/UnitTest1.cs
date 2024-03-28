using AutoMapper;
using AzureMicroservicesTest.Api.Products.Db;
using AzureMicroservicesTest.Api.Products.Providers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;

namespace AzureMicroservicesTest.Products.Tests
{
    public class UnitTest1
    {
        ProductsProvider SetupProvider()
        {
            var mockedDb = new Mock<ProductsDbContext>();
            var entities = new List<Product>() { new Product() { Id = 1, Inventory = 2, Name = "Test", Price = 200m } };
            var mappedProduct = new List<Api.Products.Models.Product>() { new Api.Products.Models.Product { Id = 1, Inventory = 2, Name = "Test", Price = 200m } };
            mockedDb.Setup(x => x.Products).ReturnsDbSet(entities);
            
            var mockedLogger = new Mock<ILogger<ProductsProvider>>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<IEnumerable<Product>, IEnumerable<Api.Products.Models.Product>>((IEnumerable<Product>)It.IsAny<object>())).Returns(mappedProduct);

            return new ProductsProvider(mockedDb.Object, mockedLogger.Object, mockedMapper.Object);
        }

        [Fact]
        public async Task GetProductsShouldGetOneProduct()
        {
            // Arrange
            var provider = SetupProvider();

            // Act

            var result = await provider.GetProductsAsync();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Products.Should().NotBeNull();
            result.Products.Should().HaveCount(1);
            result.ErrorMsg.Should().BeNull();
        }
    }
}