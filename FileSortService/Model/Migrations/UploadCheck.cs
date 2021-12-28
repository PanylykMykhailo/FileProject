using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileSortService.Model.Migrations
{
    public class UploadCheck
    {
        [Key]
        public Guid Id {get;set;}
        public ExtenCategory typeCategory { get;set;}
        public string typeFile {get;set;}
        public string hexSignature {get;set;}
    }
}