using System.Collections.Generic;
using System.Threading.Tasks;
using FileService.Model;

namespace FileService.SyncDataServices.Http
{
    public interface IFileSortDataClient
    {
        Task<InfoAboutFiles> GetOnlyFile(string pathFolder,string extension);
    }
}