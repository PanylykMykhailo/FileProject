using System.Collections.Generic;
using System.Threading.Tasks;
using FileService.Model;
using System.Net;
namespace FileService.SyncDataServices.Http
{
    public interface IFileSortDataClient
    {
        Task<InfoAboutFiles> GetOnlyFile(string pathFolder,string extension);
        Task<HttpStatusCode> DeleteFile(ParameterRequest parameter);
        string RenameFile(ParameterRequest parameter);
        Task<string> EditFile(WorkWithFile parameter);
        HttpStatusCode CreateFile(InfoAboutFile infoAboutFile);//Task<>
        Task<string> SaveFile2(List<ParameterRequest> parameter);
    }
}