using System;
using System.ComponentModel.DataAnnotations;

namespace FileSortService.Model.DatabaseModel
{
    public class ExtensionValue
    {
        [Key]
        public Guid Id{get;set;}
        public ExtensionCategory extensionCategory{get;set;}
        public string extensionValue{get;set;}
    }
}