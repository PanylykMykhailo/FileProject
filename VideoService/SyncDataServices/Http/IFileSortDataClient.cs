using System.Collections.Generic;
using System.Threading.Tasks;
using VideoService.Model;

namespace VideoService.SyncDataServices.Http
{
    public interface IFileSortDataClient
    {
        Task<InfoAboutVideos> GetOnlyVideo(string pathFolder,string extension);
        //HttpStatusCode DeleteFile(ParameterRequest parameter);
        //string RenameFile(string nameFile,string typeName,string newNameFile,string currentDirectory);
        //HttpStatusCode CreateFile(InfoAboutFile infoAboutFile);
        //Task<List<string>> SaveFile2(List<ParameterRequest> parameter);
    }
}