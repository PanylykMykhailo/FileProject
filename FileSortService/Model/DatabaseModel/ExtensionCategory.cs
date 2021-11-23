using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FileSortService.Model.DatabaseModel
{
    public class ExtensionCategory
    {
        [Key]
        public Guid Id { get; set; }
        public string nameCategory { get; set; }
        [JsonIgnore]
        public ICollection<ArchitectureFolder> architectureFolder { get; set;}
        [JsonIgnore]
        public ICollection<ExtensionValue> extensionValue { get; set; }
        [JsonIgnore]
        public ICollection<TypeFileFromUpload> typeFileFromUploads { get; set; }
    }
}