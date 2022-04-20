using System;
using System.Collections.Generic;
using System.Linq;

namespace WebStore.Models
{
    public class ProductRepository
    {
        private StoreDbContext context;

        public ProductRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> FindAll()
        {
            return context.Products.ToList();
        }

        public Product FindById(int id)
        {
            return context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public void Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Remove(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public IEnumerable<Product> FindBy(Func<Product, bool> predicate)
        {
            return context.Products.Where(predicate);
        }
    }
}
