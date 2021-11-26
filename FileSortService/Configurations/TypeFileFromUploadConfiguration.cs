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
    public class TypeFileFromUploadConfiguration : IEntityTypeConfiguration<TypeFileFromUpload>
    {
        public void Configure(EntityTypeBuilder<TypeFileFromUpload> builder)
        {
            //AutofillTypeFile getData = new AutofillTypeFile();
            builder.HasKey(exId => exId.Id);
            //builder.HasOne(extV => extV.extensionCategory).WithMany(extC => extC.typeFileFromUploads).HasForeignKey("typeCategoryId");
           // builder.HasData(getData.GetDate());
        }
    }
}
