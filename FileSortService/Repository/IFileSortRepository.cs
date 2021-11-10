using System.Collections.Generic;
using System.Net;
using FileSortService.Model;

namespace FileSortService.Repository
{
    public interface IFileSortRepository
    {
        InfoAboutFiles GetAllFile(string pathFolder,string typeFile);
        InfoAboutFile InfoAboutFile(string nameFile,string typeName);
        HttpStatusCode DeleteFile(ParameterRequest parameter);
        string RenameFile(string nameFile,string typeName,string newNameFile,string currentDirectory);
        bool OpenAndEdit(string nameFile,string typeName,string infoAdd);
        HttpStatusCode CreateFile(InfoAboutFile infoAboutFile);
    }
}