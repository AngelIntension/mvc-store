using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;
using Xunit;

namespace WebStoreTests.CustomerRepositoryTests
{
    public class FindAllShould
    {
        [Fact]
        void ReturnAllCustomers()
        {
            // arrange
            var data = new List<Customer>()
            {
                new Customer {Id = 1, LastName = "Smith", FirstName = "Joe", PhoneNumber = "555-123-4567", Email = "joe.smith@gmail.com"},
                new Customer {Id = 2, LastName = "Musk", FirstName = "Elon", PhoneNumber = "555-890-1234", Email = "elon.musk@spacex.com"},
                new Customer {Id = 3, LastName = "Doe", FirstName = "Jane", PhoneNumber = "555-567-8901", Email = "jane.doe@gmail.com"}
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
            var result = uut.FindAll();

            // assert
            Assert.Equal(data, result);
        }
    }
}
