using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;
using Xunit;

namespace WebStoreTests.AddressRepositoryTests
{
    public class FindByShould
    {
        [Fact]
        void ReturnEmpty_GivenNoMatch()
        {
            // arrange
            var data = new List<Address>()
            {
                new Address {Id = 1, Line1 = "1234 Somestreet", Line2 = "Apt 7", City = "Boston", State = "MA", ZipCode = "02108"},
                new Address {Id = 2, Line1 = "567 Someotherstreet", Line2 = "Apt 21", City = "Rochester Hills", State = "MI", ZipCode = "48306"},
                new Address {Id = 3, Line1 = "890 Yetanotherstreet", Line2 = "Apt 42", City = "Quantico", State = "VA", ZipCode = "22134"}
            };
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<Address>>();
            mockSet.As<IQueryable<Address>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<Address>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<Address>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<Address>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(m => m.Addresses).Returns(mockSet.Object);

            // act
            var uut = new AddressRepository(mockContext.Object);
            var result = uut.FindBy(a => a.ZipCode == "31415");

            // assert
            Assert.Empty(result);
        }

        [Fact]
        void ReturnCorrectProducts_GivenMatches()
        {
            // arrange
            var data = new List<Address>()
            {
                new Address {Id = 1, Line1 = "1234 Somestreet", Line2 = "Apt 7", City = "Boston", State = "MA", ZipCode = "02108"},
                new Address {Id = 2, Line1 = "567 Someotherstreet", Line2 = "Apt 21", City = "Rochester Hills", State = "MI", ZipCode = "48306"},
                new Address {Id = 3, Line1 = "890 Yetanotherstreet", Line2 = "Apt 42", City = "Quantico", State = "VA", ZipCode = "22134"}
            };
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<Address>>();
            mockSet.As<IQueryable<Address>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<Address>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<Address>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<Address>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(m => m.Addresses).Returns(mockSet.Object);

            // act
            var uut = new AddressRepository(mockContext.Object);
            Func<Address, bool> predicate = a => a.City == "Rochester Hills" || a.City == "Boston";
            var expected = data.Where(predicate).ToList();
            var result = uut.FindBy(predicate);

            // assert
            Assert.Equal(expected, result);
        }
    }
}
