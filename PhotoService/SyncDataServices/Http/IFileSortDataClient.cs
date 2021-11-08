using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoService.Model;

namespace PhotoService.SyncDataServices.Http
{
    public interface IFileSortDataClient
    {
        Task<InfoAboutPhotos> GetOnlyPhoto(string pathFolder,string extension);
    }
}