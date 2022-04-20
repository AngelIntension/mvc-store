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
    }
}
