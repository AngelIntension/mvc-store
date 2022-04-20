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
        void ReturnNull_GivenNoMatch()
        {
            // arrange
            var data = new List<Customer>()
            {
                new Customer { LastName = "Smith", FirstName = "Joe", Id = 1},
                new Customer { LastName = "Musk", FirstName = "Elon", Id = 2},
                new Customer { LastName = "Doe", FirstName = "Jane", Id = 3}
            };
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);

            // act
            var uut = new CustomerRepository(mockContext.Object);
            var result = uut.FindById(4);

            // assert
            Assert.Null(result);
        }

        [Fact]
        void ReturnCorrectCustomer_GivenMatch()
        {
            // arrange
            var data = new List<Customer>()
            {
                new Customer { LastName = "Smith", FirstName = "Joe", Id = 1},
                new Customer { LastName = "Musk", FirstName = "Elon", Id = 2},
                new Customer { LastName = "Doe", FirstName = "Jane", Id = 3}
            };
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);

            // act
            var uut = new CustomerRepository(mockContext.Object);
            int id = 2;
            var expected = data.Find(c => c.Id == id);
            var result = uut.FindById(id);

            // assert
            Assert.Equal(expected, result);
        }
    }
}
