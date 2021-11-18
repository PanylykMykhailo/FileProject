using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileSortService.Model.DatabaseModel
{
    public class TypeFileFromUpload
    {
        [Key]
        public Guid Id{get;set;}
        public ExtensionCategory extensionCategory{get;set;}
        [ForeignKey("extensionCategoryId")]
        public Guid extensionCategoryId { get; set; }
        public string typeFile {get;set;}
        public string hexSignature {get;set;}
    }
}