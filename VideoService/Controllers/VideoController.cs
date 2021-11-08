using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VideoService.Model;
using VideoService.SyncDataServices.Http;

namespace VideoService.Controllers
{
    [Route("api/videoservice/[controller]")]
    [ApiController]
    public class VideoController:ControllerBase
    {
        private readonly IFileSortDataClient _iFileSortDataClient;
        public VideoController(IFileSortDataClient iFileSortDataClient)
        {
            _iFileSortDataClient = iFileSortDataClient;
        }
        [HttpGet("GetOnlyPhoto/{pathFolder}")]
        public async Task<InfoAboutVideos> GetOnlyPhoto(string pathFolder)
        {

            var getOnlyFile =  await _iFileSortDataClient.GetOnlyVideo(pathFolder,"");
            return getOnlyFile;
        }
    }
}