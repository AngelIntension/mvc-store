﻿using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;
using Xunit;

namespace WebStoreTests.CustomerRepositoryTests
{
    public class FindByShould
    {
        [Fact]
        void ReturnEmpty_GivenNoMatch()
        {
            // arrange
            var data = new List<Customer>()
            {
                new Customer {LastName = "Smith", FirstName = "Joe"},
                new Customer {LastName = "Musk", FirstName = "Elon"},
                new Customer {LastName = "Doe", FirstName = "Jane"},
                new Customer {LastName = "Doe", FirstName = "John"}
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
            Func<Customer, bool> predicate = c => c.LastName == "Doe";
            var expected = data.Where(predicate);
            var result = uut.FindBy(predicate);

            // assert
            Assert.Equal(expected, result);
        }
    }
}