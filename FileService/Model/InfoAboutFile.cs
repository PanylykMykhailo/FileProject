namespace FileService.Model
{
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