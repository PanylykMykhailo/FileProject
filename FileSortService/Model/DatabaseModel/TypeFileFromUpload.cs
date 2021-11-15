using System;
using System.ComponentModel.DataAnnotations;
namespace FileSortService.Model.DatabaseModel
{
    public class TypeFileFromUpload
    {
        [Key]
        public Guid Id{get;set;}
        public ExtensionCategory extensionCategory{get;set;}
        public string typeFile {get;set;}
    }
}