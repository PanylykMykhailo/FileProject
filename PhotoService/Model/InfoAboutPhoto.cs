using System.Collections.Generic;

namespace PhotoService.Model
{
    public class InfoAboutPhotos
    {
        public int CountFile{get;set;}
        public List<string> folderPath{get;set;}
        public List<InfoAboutPhoto> infoaboutFile{get;set;}
        
    }
    public class InfoAboutPhoto
    {
        public string nameFile{get;set;}
        public string typeFile{get;set;}
        public string sizeFile{get;set;}
        public string dateCreatedFile{get;set;}
        public bool isFolder{get;set;}
        public int fileInFolder{get;set;}
    }
}