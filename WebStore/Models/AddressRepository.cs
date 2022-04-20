using System;
using System.Collections.Generic;

namespace WebStore.Models
{
    public class AddressRepository : IRepository<Address>
    {
        private StoreDbContext context;

        public AddressRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public void Add(Address address)
        {
            context.Addresses.Add(address);
            context.SaveChanges();
        }

        public IEnumerable<Address> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Address> FindBy(Func<Address, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Address FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Address entity)
        {
            throw new NotImplementedException();
        }
    }
}
