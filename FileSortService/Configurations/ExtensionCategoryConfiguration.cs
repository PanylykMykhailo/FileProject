using FileSortService.Model.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSortService.Configurations
{
    public class ExtensionCategoryConfiguration : IEntityTypeConfiguration<ExtensionCategory>
    {
        public void Configure(EntityTypeBuilder<ExtensionCategory> builder)
        {
            builder.HasKey(exId => exId.Id);
            builder.Property(ext => ext.nameCategory).HasColumnType("varchar(20)");
        }
    }
}
