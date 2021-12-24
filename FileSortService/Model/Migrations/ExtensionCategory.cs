using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FileSortService.Model.Migrations
{
    public class ExtensionCategory
    {
        [Key]
        public Guid Id { get; set; }
        public string nameCategory { get; set; }
        //[JsonIgnore]
        //public ICollection<ArchitectureFolder> architectureFolder { get; set; }
        //[JsonIgnore]
        //public ICollection<ExtensionValue> extensionValue { get; set; }
        //[JsonIgnore]
        //public ICollection<TypeFileFromUpload> typeFileFromUploads { get; set; }
    }
}