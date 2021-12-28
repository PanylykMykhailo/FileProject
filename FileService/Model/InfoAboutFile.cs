using System.Collections.Generic;

namespace FileService.Model
{
    public class InfoAboutFiles
    {
        public int CountFile{get;set;}
        public List<string> folderPath{get;set;}
        public List<InfoAboutFile> infoaboutFile{get;set;}
        
    }
    public class InfoAboutFile
    {
        public string nameFile{get;set;}
        public string typeFile{get;set;}
        public string sizeFile{get;set;}
        public string dateCreatedFile{get;set;}
        public bool isFolder{get;set;}
        public int fileInFolder{get;set;}
    }
}