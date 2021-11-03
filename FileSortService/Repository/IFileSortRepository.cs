using System.Collections.Generic;
using FileSortService.Model;

namespace FileSortService.Repository
{
    public interface IFileSortRepository
    {
        List<InfoAboutFile> GetAllFile();
        InfoAboutFile InfoAboutFile(string nameFile,string typeName);
        string DeleteFile(string nameFile,string typeName);
        string RenameFile(string nameFile,string typeName,string newNameFile);
        bool OpenAndEdit(string nameFile,string typeName,string infoAdd);
        bool CreateFile(InfoAboutFile infoAboutFile);
    }
}