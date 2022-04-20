using Microsoft.EntityFrameworkCore;
using Moq;
using WebStore.Models;
using Xunit;

namespace WebStoreTests.ProductRepositoryTests
{
    public class AddShould
    {
        [Fact]
        void AttemptToAddNewProduct()
        {
            // arrange
            var mockSet = new Mock<DbSet<Product>>();

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            // act
            var uut = new ProductRepository(mockContext.Object);
            var product = new Product();
            uut.Add(product);

            // assert
            mockSet.Verify(m => m.Add(product), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
