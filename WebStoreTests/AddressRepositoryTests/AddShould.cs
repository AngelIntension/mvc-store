﻿using Microsoft.EntityFrameworkCore;
using Moq;
using WebStore.Models;
using Xunit;

namespace WebStoreTests.AddressRepositoryTests
{
    public class AddShould
    {
        [Fact]
        void AttemptToAddNewAddress()
        {
            // arrange
            var mockSet = new Mock<DbSet<Address>>();
            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(m => m.Addresses).Returns(mockSet.Object);

            // act
            var uut = new AddressRepository(mockContext.Object);
            var address = new Address();
            uut.Add(address);

            // assert
            mockSet.Verify(m => m.Add(address), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
