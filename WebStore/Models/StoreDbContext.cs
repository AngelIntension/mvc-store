using Microsoft.EntityFrameworkCore;

namespace WebStore.Models
{
    public class StoreDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
    }
}
