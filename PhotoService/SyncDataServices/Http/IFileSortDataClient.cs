using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoService.Model;

namespace PhotoService.SyncDataServices.Http
{
    public interface IFileSortDataClient
    {
        Task<InfoAboutPhotos> GetOnlyPhoto(string pathFolder,string extension);
        //HttpStatusCode DeleteFile(ParameterRequest parameter);
        //string RenameFile(string nameFile,string typeName,string newNameFile,string currentDirectory);
        //HttpStatusCode CreateFile(InfoAboutFile infoAboutFile);
        //Task<List<string>> SaveFile2(List<ParameterRequest> parameter);
    }
}