using Microsoft.EntityFrameworkCore;
using Moq;
using WebStore.Models;
using Xunit;

namespace WebStoreTests.CustomerRepositoryTests
{
    public class RemoveShould
    {
        [Fact]
        void AttemptToRemovePassedProduct()
        {
            // arrange
            var mockSet = new Mock<DbSet<Customer>>();
            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);

            // act
            var uut = new CustomerRepository(mockContext.Object);
            var customer = new Customer();
            uut.Remove(customer);

            // assert
            mockSet.Verify(m => m.Remove(customer), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
