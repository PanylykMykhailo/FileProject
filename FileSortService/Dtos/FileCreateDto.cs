using System;
using System.Collections.Generic;

namespace FileSortService.Dtos
{
    public class FileCreateDto
    {
        public int CountFile{get;set;}
        public List<InfoAboutFileDtos> infoaboutFile{get;set;}
    }
    public class InfoAboutFileDtos
    {
        public string NameFile{get;set;}
        public string TypeFile{get;set;}
        public string SizeFile{get;set;}
        public string DateCreatedFile{get;set;}
        public bool isFolder{get;set;}
        public int fileInFolder{get;set;}
        public string currentDirectory{get;set;}
    }
}