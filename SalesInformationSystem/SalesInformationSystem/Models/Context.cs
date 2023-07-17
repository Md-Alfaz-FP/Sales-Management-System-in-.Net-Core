using Microsoft.EntityFrameworkCore;

namespace SalesInformationSystem.Models
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<SaleMaster> SaleMasters { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
    }
}
