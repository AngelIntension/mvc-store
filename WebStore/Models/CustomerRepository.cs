using System;
using System.Collections.Generic;
using System.Linq;

namespace WebStore.Models
{
    public class CustomerRepository : IRepository<Customer>
    {
        private StoreDbContext context;

        public CustomerRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public void Add(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public IEnumerable<Customer> FindAll()
        {
            return context.Customers.ToList();
        }

        public IEnumerable<Customer> FindBy(Func<Customer, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Customer FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
