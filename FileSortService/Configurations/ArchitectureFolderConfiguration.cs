using FileSortService.Data;
using FileSortService.Model.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSortService.Configurations
{
    public class ArchitectureFolderConfiguration : IEntityTypeConfiguration<ArchitectureFolder>
    {
        public void Configure(EntityTypeBuilder<ArchitectureFolder> builder)
        {
            builder.HasKey(exId => exId.Id);
            builder.HasOne(extV => extV.typeCategory).WithMany(extC => extC.architectureFolder).HasForeignKey("typeCategoryId");
        }
    }
}
