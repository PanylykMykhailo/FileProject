
using FileSortService.Model.DatabaseModel;
using Microsoft.EntityFrameworkCore;

namespace FileSortService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options):base(options) { }
        public DbSet<ExtensionCategory> ExtenCategory { get; set; }
        public DbSet<ExtensionValue> ExtenValue { get; set; }
    }
}