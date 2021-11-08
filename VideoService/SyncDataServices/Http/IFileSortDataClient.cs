using System.Collections.Generic;
using System.Threading.Tasks;
using VideoService.Model;

namespace VideoService.SyncDataServices.Http
{
    public interface IFileSortDataClient
    {
        Task<InfoAboutVideos> GetOnlyVideo(string pathFolder,string extension);
    }
}