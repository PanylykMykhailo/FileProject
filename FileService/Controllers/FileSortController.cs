using System;
using System.Collections.Generic;
using FileService.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

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
            
            public ActionResult<InfoAboutFile> TestInboundConnection(InfoAboutFile infoAboutFile)
            {
                //var response = Request.CreateResponse<InfoAboutFile>(HttpStatusCode.OK, db.Books);
                //Console.WriteLine("--> Inbound POST # File Service");
                //return new HtmlContentResult(infoAboutFile);
                var infoAboutFileCop = new InfoAboutFile()
                { 
                    nameFile = infoAboutFile.nameFile,
                    typeFile = infoAboutFile.typeFile,
                    sizeFile = infoAboutFile.sizeFile,
                    dateCreatedFile = infoAboutFile.dateCreatedFile
                
                };
                /*var httpContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize<InfoAboutFile>(infoAboutFileCop),
                Encoding.UTF8,
                "application/json");*/
                return infoAboutFileCop;
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