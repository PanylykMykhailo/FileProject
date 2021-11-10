using System;
using System.ComponentModel.DataAnnotations;

namespace FileSortService.Model.DatabaseModel
{
    public class ExtensionCategory
    {
        [Key]
        public Guid Id{get;set;}
        public string nameCategory{get;set;}
    }
}