
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
            AutofillTypeFile getData = new AutofillTypeFile();
            modelBuilder.Entity<TypeFileFromUpload>(tfu =>
            {  
                tfu.HasData(getData.GetDate());
            });
        }
    }
}