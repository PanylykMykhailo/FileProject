using System;
using System.Collections.Generic;
using FileService.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using FileService.SyncDataServices.Http;

namespace FileService.Controllers
{
    [Route("api/fileservice/[controller]")]
    [ApiController]
    public class FileController:ControllerBase
    {
        private readonly IFileSortDataClient _iFileSortDataClient;
        public FileController(IFileSortDataClient iFileSortDataClient)
        {
                _iFileSortDataClient = iFileSortDataClient;
        }
            
        [HttpPost] 
        public ActionResult<InfoAboutFile> TestInboundConnection(InfoAboutFile infoAboutFile)
        {
            var infoAboutFileCop = new InfoAboutFile()
            { 
                nameFile = infoAboutFile.nameFile,
                typeFile = infoAboutFile.typeFile,
                sizeFile = infoAboutFile.sizeFile,
                dateCreatedFile = infoAboutFile.dateCreatedFile
                
            };
            return infoAboutFileCop;
        }
            
        [Route("GetOnlyFile")]
        [HttpGet]
        public async Task<IEnumerable<InfoAboutFile>> GetOnlyFile()
        {
            var getOnlyFile = await _iFileSortDataClient.GetOnlyFile("txt");
            return getOnlyFile;
        }
    }
}