using System;
using System.Collections.Generic;
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