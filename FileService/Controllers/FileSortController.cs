using System;
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
            public ActionResult TestInboundConnection()
            {
                Console.WriteLine("--> Inbound POST # File Service");
                return Ok("Inbound test of from FileSort Controller");
            }
    }
}