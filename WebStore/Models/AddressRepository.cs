using System;
using System.Collections.Generic;
using System.Linq;

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
            return context.Addresses.ToList();
        }

        public IEnumerable<Address> FindBy(Func<Address, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Address FindById(int id)
        {
            return context.Addresses.Find(id);
        }

        public void Remove(Address entity)
        {
            throw new NotImplementedException();
        }
    }
}
