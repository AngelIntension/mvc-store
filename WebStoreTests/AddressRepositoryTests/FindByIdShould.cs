using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;
using Xunit;

namespace WebStoreTests.AddressRepositoryTests
{
    public class FindByIdShould
    {
        [Fact]
        void AttemptToFindMatchingEntity()
        {
            // arrange
            var queryableData = new List<Address>().AsQueryable();
            var mockSet = new Mock<DbSet<Address>>();
            mockSet.As<IQueryable<Address>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<Address>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<Address>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<Address>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(m => m.Addresses).Returns(mockSet.Object);

            // act
            var uut = new AddressRepository(mockContext.Object);
            int id = 42;
            uut.FindById(id);

            // assert
            mockSet.Verify(m => m.Find(id), Times.Once());
        }
    }
}
