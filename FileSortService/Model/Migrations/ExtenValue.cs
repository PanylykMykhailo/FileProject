using System;
using System.ComponentModel.DataAnnotations;

namespace FileSortService.Model.Migrations
{
    public class ExtenValue
    {
        [Key]
        public Guid Id{get;set;}
        public ExtenCategory extensionCategory { get;set;}
        public string extensionValue{get;set;}
    }
}