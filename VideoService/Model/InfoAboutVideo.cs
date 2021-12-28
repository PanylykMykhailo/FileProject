using System.Collections.Generic;

namespace VideoService.Model
{
    public class InfoAboutVideos
    {
        public int CountFile{get;set;}
        public List<string> folderPath{get;set;}
        public List<InfoAboutVideo> infoaboutFile{get;set;}
        
    }
    public class InfoAboutVideo
    {
        public string nameFile{get;set;}
        public string typeFile{get;set;}
        public string sizeFile{get;set;}
        public string dateCreatedFile{get;set;}
        public bool isFolder{get;set;}
        public int fileInFolder{get;set;}
    }
}