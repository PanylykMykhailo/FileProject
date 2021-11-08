using PhotoService.SyncDataServices.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PhotoService.Model;

namespace PhotoService.Controllers
{
    [Route("api/photoservice/[controller]")]
    [ApiController]
    public class PhotoController:ControllerBase
    {
        private readonly IFileSortDataClient _iFileSortDataClient;
        public PhotoController(IFileSortDataClient iFileSortDataClient)
        {
            _iFileSortDataClient = iFileSortDataClient;
        }
        [HttpGet("GetOnlyPhoto/{pathFolder}")]
        public async Task<InfoAboutPhotos> GetOnlyPhoto(string pathFolder)
        {

            var getOnlyFile =  await _iFileSortDataClient.GetOnlyPhoto(pathFolder,"txt");
            return getOnlyFile;
        }
    }
}