using System;
using System.Collections.Generic;
using FileService.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
            
        // [HttpPost] 
        // public ActionResult<InfoAboutFile> TestInboundConnection(InfoAboutFile infoAboutFile)
        // {
        //     var infoAboutFileCop = new InfoAboutFile()
        //     { 
        //         nameFile = infoAboutFile.nameFile,
        //         typeFile = infoAboutFile.typeFile,
        //         sizeFile = infoAboutFile.sizeFile,
        //         dateCreatedFile = infoAboutFile.dateCreatedFile
                
        //     };
        //     return infoAboutFileCop;
        // }

        [HttpGet("GetOnlyFile/{pathFolder}")]
        public async Task<InfoAboutFiles> GetOnlyFile(string pathFolder)
        {
            var getOnlyFile =  await _iFileSortDataClient.GetOnlyFile(pathFolder,"txt");
            return getOnlyFile;
        }

        // [HttpDelete]
        // public HttpStatusCode DeleteFile(ParameterRequest parameter)
        // {
        //     Console.WriteLine("--> Delete File");
        //     try
        //     {
        //         var tryDeleteFile = _iFileSortDataClient.DeleteFile(parameter);
        //         if(tryDeleteFile == HttpStatusCode.OK)
        //         {
        //             return tryDeleteFile;
        //         }
        //         else
        //         {
        //             return tryDeleteFile;
        //         }
        //     }
        //     catch(Exception)
        //     {
        //         return HttpStatusCode.NotFound;
        //     }
            
        // }
        // [Route("RenameFile")]
        // [HttpPut]
        // public JsonResult RenameFile(ParameterRequest parameter)
        // {
        //     Console.WriteLine("--> Rename File");
        //     System.Console.WriteLine(parameter.currentDirectory);
        //     try
        //     {
        //         var tryRename = _iFileSortDataClient.RenameFile(parameter.nameFile,parameter.typeFile,parameter.newNameFile,parameter.currentDirectory);
        //         if(tryRename!=null)
        //         {
        //             return new JsonResult("Old name :" + parameter.nameFile + "\n"
        //             +"New name" + parameter.newNameFile);
        //         }
        //         else
        //         {
        //             return new JsonResult(NotFound() + parameter.nameFile + parameter.typeFile);
        //         }
        //     }
        //     catch (Exception)
        //     {
        //         throw;
        //     }
        // } 
        // [Route("EditFile")]
        // [HttpPut]
        // public JsonResult EditFile(WorkWithFile parameter)
        // {
        //     var getStatus = _iFileSortDataClient.EditFile(parameter);
        //     return new JsonResult(getStatus);
        // }
        // [HttpPost]
        // public async Task<HttpStatusCode> SaveFile(List<ParameterRequest> parameter) //[FromQuery] string currentDirectory,
        // {
        //     System.Console.WriteLine("-->Start Work");
        //     var listTime = await _iFileSortDataClient.SaveFile2(parameter);
        //     if(listTime.Count!=0)
        //         return HttpStatusCode.Created; 
        //     return HttpStatusCode.BadRequest;
        // }
    }
}