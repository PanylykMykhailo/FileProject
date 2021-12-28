using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FileSortService.Model.Migrations
{
    public class Architecture
    {
        [Key]
        public Guid Id{get;set;}
        public string nameFile{get;set;}
        public string typeFile{get;set;}
        public ExtenCategory typeCategory{get;set;}
        public string linkToOpen {get;set;}
        public string sizeFile{get;set;}
        public string dateCreatedFile{get;set;} 
        public bool isFolder{get;set;}
        public int fileInFolder{get;set;}
        public string pathfolder{get;set;}
    }
}