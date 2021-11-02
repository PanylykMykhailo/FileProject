using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using FileSortService.Dtos;
using FileSortService.Model;
using FileSortService.Repository;
using FileSortService.SyncDataServices.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FileSortService
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileSortController : ControllerBase
    {
        private readonly IFileSortRepository _iFileSortRepository;
        private readonly IMapper _mapper;
        private readonly IFileDataClient _fileDataClient;
        private readonly IWebHostEnvironment _env;
        public FileSortController(IFileSortRepository iFileSortRepository , IMapper mapper,IFileDataClient  fileDataClient, IWebHostEnvironment env)
        {
            _iFileSortRepository = iFileSortRepository;
            _mapper = mapper;
            _fileDataClient = fileDataClient;
            _env = env;
        }
        [HttpGet]
        public ActionResult<InfoAboutFiles> GetAllFiles()
        {
            Console.WriteLine("--> Getting all File....");
            var fileItem = _iFileSortRepository.GetAllFile();
            //return Ok(fileItem);
            return Ok(_mapper.Map<List<InfoAboutFileDto>>(fileItem));
        }
        [HttpGet("{namefile}/{typeName}",Name = "InfoAboutFile")]
        public ActionResult<InfoAboutFile> InfoAboutFile(string nameFile,string typeName)
        {
            Console.WriteLine($"--> Getting about {nameFile}....");
            
            var fileItem = _iFileSortRepository.InfoAboutFile(nameFile,typeName);
            if(fileItem!=null)
            {
                return Ok(_mapper.Map<InfoAboutFileDto>(fileItem));
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<InfoAboutFile>> CreateFile(InfoAboutFileDtos infoAboutFile)
        {
            var infoAboutFileModel = _mapper.Map<InfoAboutFile>(infoAboutFile);
            _iFileSortRepository.CreateFile(infoAboutFileModel);
            
            var fileReadDto = _mapper.Map<InfoAboutFileDto>(infoAboutFileModel);

            try
            {
                await _fileDataClient.SendFileSortToFileS(fileReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not send synchrinously : {ex.Message}");
            }
            return CreatedAtRoute(nameof(InfoAboutFile),new {nameFile = fileReadDto.NameFile,typeName = fileReadDto.TypeFile},fileReadDto);
        }
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Test/" + fileName;

                using (var stream = new FileStream(physicalPath,FileMode.Create))
                {
                     postedFile.CopyTo(stream);
                }
                return new JsonResult(fileName);
            }
            catch(Exception)
            {
                return new JsonResult("anon.png");
            }
        }
    }
}