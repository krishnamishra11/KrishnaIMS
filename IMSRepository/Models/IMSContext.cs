using Microsoft.EntityFrameworkCore;

namespace IMSRepository.Models
{
    public class IMSContext:DbContext
    {
        public IMSContext(DbContextOptions options):base(options)
        { }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Person> Person { get; set; }
    }
}
