using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileSortService.Model
{
    public class InfoAboutFiles
    {
        public int CountFile{get;set;}
        public List<InfoAboutFile> infoaboutFile{get;set;}
        
    }
    public class InfoAboutFile
    {
        public string NameFile{get;set;}
        public string TypeFile{get;set;}
        public string SizeFile{get;set;}
        public string DateCreatedFile{get;set;} 
        public bool isFolder{get;set;}
        public int fileInFolder{get;set;}
    }
}

