using Microsoft.EntityFrameworkCore;
using Moq;
using WebStore.Models;
using Xunit;

namespace WebStoreTests.ProductRepositoryTests
{
    public class RemoveShould
    {
        [Fact]
        void AttemptToRemovePassedProduct()
        {
            // arrange
            var mockSet = new Mock<DbSet<Product>>();

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            // act
            var uut = new ProductRepository(mockContext.Object);
            var product = new Product();
            uut.Remove(product);

            // assert
            mockSet.Verify(m => m.Remove(product), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
