using System.Collections.Generic;
using FileSortService.Model;

namespace FileSortService.Repository
{
    public interface IFileSortRepository
    {
        InfoAboutFiles GetAllFile(string pathFolder,string typeFile);
        InfoAboutFile InfoAboutFile(string nameFile,string typeName);
        string DeleteFile(string nameFile,string typeName,string currentDirectory);
        string RenameFile(string nameFile,string typeName,string newNameFile,string currentDirectory);
        bool OpenAndEdit(string nameFile,string typeName,string infoAdd);
        bool CreateFile(InfoAboutFile infoAboutFile);
    }
}