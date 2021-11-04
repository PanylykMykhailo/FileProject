using System;
using System.Collections.Generic;
using FileService.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace FileService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class FileSortController:ControllerBase
    {
            public FileSortController()
            {
                
            }
            
            [HttpPost]
            
            public ActionResult TestInboundConnection(InfoAboutFile infoAboutFile)
            {
                //var response = Request.CreateResponse<InfoAboutFile>(HttpStatusCode.OK, db.Books);
                //Console.WriteLine("--> Inbound POST # File Service");
                //return new HtmlContentResult(infoAboutFile);
                return CreatedAtRoute(nameof(InfoAboutFile), new { 
                    nameFile = infoAboutFile.nameFile,
                    typeFile = infoAboutFile.typeFile,
                    sizeFile = infoAboutFile.sizeFile,
                    dateCreatedFile = infoAboutFile.dateCreatedFile
                }, infoAboutFile);
            }
            
            [Route("GetOnlyFile")]
            [HttpGet]
            public ActionResult<List<InfoAboutFile>> GetOnlyFile()
            {
                List<InfoAboutFile> fileList = new();
                fileList.Add(new InfoAboutFile
                {
                    nameFile = "AngularTest",
                    typeFile = ".txt",
                    sizeFile = "0 bytes",
                    dateCreatedFile = "11/4/2021 9:56"
                });
                return fileList;
            }
    }
}