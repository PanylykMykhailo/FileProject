using System;
using System.Collections.Generic;

namespace FileSortService.Dtos
{
    public class FileReadDto
    {
        public int CountFile{get;set;}
        public List<string> folderPath{get;set;}
        public List<InfoAboutFileDto> infoaboutFile{get;set;}
    }
    public class InfoAboutFileDto
    {
        public string NameFile{get;set;}
        public string TypeFile{get;set;}
        public string typeCategory{get;set;}
        public string linkToOpen {get;set;}
        public string SizeFile{get;set;}
        public string DateCreatedFile{get;set;}
        public bool isFolder{get;set;}
        public int fileInFolder{get;set;}
    }
}