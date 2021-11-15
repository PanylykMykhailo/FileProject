using System.Collections.Generic;
using System.Threading.Tasks;
using FileService.Model;
using System.Net;
namespace FileService.SyncDataServices.Http
{
    public interface IFileSortDataClient
    {
        Task<InfoAboutFiles> GetOnlyFile(string pathFolder,string extension);
        //HttpStatusCode DeleteFile(ParameterRequest parameter);
        //string RenameFile(string nameFile,string typeName,string newNameFile,string currentDirectory);
        //string EditFile(WorkWithFile parameter);
        //HttpStatusCode CreateFile(InfoAboutFile infoAboutFile);
        //Task<List<string>> SaveFile2(List<ParameterRequest> parameter);
    }
}