
using FileSortService.Configurations;
using FileSortService.Model.DatabaseModel;
using Microsoft.EntityFrameworkCore;

namespace FileSortService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(){}
        public AppDbContext(DbContextOptions options):base(options) { }
        public DbSet<ExtensionCategory> ExtenCategory { get; set; }
        public DbSet<ExtensionValue> ExtenValue { get; set; }
        public DbSet<ArchitectureFolder> Architecture { get; set; }
        public DbSet<TypeFileFromUpload> UploadCheck { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof (ExtensionCategoryConfiguration).Assembly);
            
            //modelBuilder.ApplyConfiguration(new ExtensionCategoryConfiguration());
            //modelBuilder.ApplyConfiguration(new ExtensionValueConfiguration());
            //modelBuilder.ApplyConfiguration(new ArchitectureFolderConfiguration());
            //modelBuilder.ApplyConfiguration(new TypeFileFromUploadConfiguration());
        }
    }
}