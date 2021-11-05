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
    public class FileController : ControllerBase
    {
        private readonly IFileSortRepository _iFileSortRepository;
        private readonly IMapper _mapper;
        private readonly IFileDataClient _fileDataClient;
        private readonly IWebHostEnvironment _env;
        public FileController(IFileSortRepository iFileSortRepository , IMapper mapper,IFileDataClient  fileDataClient, IWebHostEnvironment env)
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
            var fileItem = _iFileSortRepository.GetAllFile("Test",null);
            //return Ok(fileItem);
            return Ok(_mapper.Map<FileReadDto>(fileItem));
        }
        [HttpGet("{namefile}*{typeName}",Name = "InfoAboutFile")]
        public ActionResult<InfoAboutFile> InfoAboutFile(string nameFile,string typeFile)
        {
            Console.WriteLine($"--> Getting about {nameFile}....");
            
            var fileItem = _iFileSortRepository.InfoAboutFile(nameFile,typeFile);
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

        [HttpDelete]
        public JsonResult DeleteFile(ParameterRequest parameter)
        {
            try
            {
                var tryDeleteFile = _iFileSortRepository.DeleteFile(parameter.nameFile,parameter.typeFile);
                if(tryDeleteFile!=null)
                {
                    return new JsonResult(tryDeleteFile);
                }
                else
                {
                    return new JsonResult(NotFound() + parameter.nameFile + parameter.typeFile);
                }
            }
            catch(Exception)
            {
                return new JsonResult(NotFound());
            }
            
        }
        [Route("RenameFile")]
        [HttpPut]
        public JsonResult RenameFile(ParameterRequest parameter)
        {
            try
            {
                var tryRename = _iFileSortRepository.RenameFile(parameter.nameFile,parameter.typeFile,parameter.newNameFile);
                if(tryRename!=null)
                {
                    return new JsonResult("Old name :" + parameter.nameFile + "\n"
                    +"New name" + parameter.newNameFile);
                }
                else
                {
                    return new JsonResult(NotFound() + parameter.nameFile + parameter.typeFile);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        } 
        [HttpGet("{pathfolder}/{typeFile}")]
        public InfoAboutFiles GetOnlyFile(string pathFolder,string typeFile)
        {
            var getOnlyFile = _iFileSortRepository.GetAllFile(pathFolder,typeFile);
            return getOnlyFile;
        }
    }
}