using System;
using FileService.Model;
using Microsoft.AspNetCore.Mvc;

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
                Console.WriteLine("--> Inbound POST # File Service");
                return infoAboutFile;
            }
            [HttpGet]
            public ActionResult<InfoAboutFile> GetOnlyFile()
            {
                return new InfoAboutFile{
                    nameFile = "AngularTest",
                    typeFile = ".txt",
                    sizeFile = "0 bytes",
                    dateCreatedFile = "11/4/2021 9:56"
                };
            }
    }
}