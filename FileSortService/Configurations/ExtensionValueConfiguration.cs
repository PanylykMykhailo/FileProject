using FileSortService.Model.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSortService.Configurations
{
    public class ExtensionValueConfiguration : IEntityTypeConfiguration<ExtensionValue>
    {
        public void Configure(EntityTypeBuilder<ExtensionValue> builder)
        {
            builder.HasKey(exId => exId.Id);
            builder.Property(ext => ext.extensionValue).HasColumnType("varchar(20)");
            //builder.HasOne(extV => extV.extensionCategory).WithMany(extC => extC.extensionValue);
        }
    }
}
