using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;
using Xunit;

namespace WebStoreTests.CustomerRepositoryTests
{
    public class FindByIdShould
    {
        [Fact]
        void AttemptToFindMatchingEntity()
        {
            // arrange
            var queryableData = new List<Customer>().AsQueryable();
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);

            // act
            var uut = new CustomerRepository(mockContext.Object);
            int id = 42;
            uut.FindById(id);

            // assert
            mockSet.Verify(m => m.Find(id), Times.Once());
        }
    }
}
