using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;
using Xunit;

namespace WebStoreTests.ProductRepositoryTests
{
    public class FindByShould
    {
        [Fact]
        void ReturnEmpty_GivenNoMatch()
        {
            // arrange
            var data = new List<Product>()
            {
                new Product {Id = 1, Name = "product 1", Description = "some cool new product", Price = 1.00M},
                new Product {Id = 2, Name = "product 2", Description = "some cool new product", Price = 2.00M},
                new Product {Id = 3, Name = "product 3", Description = "some cool new product", Price = 3.00M}
            };

            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            // act
            Func<Product, bool> predicate = p => p.Name == "product 2" & p.Price == 1.00M;
            var uut = new ProductRepository(mockContext.Object);
            var result = uut.FindBy(predicate);

            // assert
            Assert.Empty(result);
        }

        [Fact]
        void ReturnCorrectProducts_GivenMatches()
        {
            // arrange
            var data = new List<Product>()
            {
                new Product {Id = 1, Name = "product 1", Description = "some cool new product", Price = 1.00M},
                new Product {Id = 2, Name = "product 2", Description = "some cool new product", Price = 2.00M},
                new Product {Id = 3, Name = "product 3", Description = "some cool new product", Price = 3.00M},
                new Product {Id = 4, Name = "product 4", Description = "some cool new product", Price = 2.00M}
            };

            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            // act
            Func<Product, bool> predicate = p => p.Price == 2.00M;
            var expected = data.Where(predicate).ToList();

            var uut = new ProductRepository(mockContext.Object);
            var result = uut.FindBy(predicate);

            // assert
            Assert.Equal(expected, result);
        }
    }
}
