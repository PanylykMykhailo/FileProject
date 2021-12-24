using AutoMapper;
using FileSortService.Data;
using FileSortService.Dtos;
using FileSortService.Model.DatabaseModel;
using FileSortService.Model.WorkModel;
using FileSortService.Repository;
using FileSortService.SyncDataServices.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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
        private readonly AppDbContext _appDbContext;

        public FileController(IFileSortRepository iFileSortRepository, IMapper mapper, IFileDataClient fileDataClient, IWebHostEnvironment env, AppDbContext appDbContext)
        {
            _iFileSortRepository = iFileSortRepository;
            _mapper = mapper;
            _fileDataClient = fileDataClient;
            _env = env;
            _appDbContext = appDbContext;
        }
        [Route("Autofill")]
        [HttpGet]
        public IEnumerable<ArchitectureFolder> Geting() 
        {
            var result = _iFileSortRepository.GetArchitecture();
            return result;
        }
        [Route("UploadCheck")]
        [HttpGet]
        public IEnumerable<TypeFileFromUpload> GetDate()//
        {
            var result = _iFileSortRepository.GetDate();
            return result;
        }
        [Route("InsertExtension")]
        [HttpPost]
        public void InsertExtension() 
        {
            _iFileSortRepository.InsertExt();
        }
        [HttpGet("{pathfolder}/{typeFile}")]
        public InfoAboutFiles GetAllFiles(string pathfolder,string typeFile)//<ActionResult>
        {
            Console.WriteLine("--> Getting all File....");
            //typeFile = typeFile == "*" ? null : typeFile;  
            //var fileItem = _iFileSortRepository.GetAllFile(pathfolder,typeFile);
            //return Ok(_mapper.Map<FileReadDto>(fileItem));
            typeFile = typeFile == "*" ? null : typeFile;
            var fileItem = _iFileSortRepository.GetAllFileV2(pathfolder,typeFile);
            return fileItem;
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
        public HttpStatusCode CreateFile(InfoAboutFileDtos infoAboutFile)//async Task<>
        {
            var infoAboutFileModel = _mapper.Map<InfoAboutFile>(infoAboutFile);
            
            //var infoAboutFileModel = _mapper.Map<InfoAboutFile>(infoAboutFile);
            var statusCode =  _iFileSortRepository.CreateFile(infoAboutFileModel);
            return statusCode;
            //var fileReadDto = _mapper.Map<InfoAboutFileDto>(infoAboutFileModel);
            // try
            // {
            //     await _fileDataClient.SendFileSortToFileS(fileReadDto);
            // }
            // catch(Exception ex)
            // {
            //     Console.WriteLine($"--> Could not send synchrinously : {ex.Message}");
            // }
            
            //return CreatedAtRoute(nameof(InfoAboutFile),new {nameFile = fileReadDto.NameFile,typeName = fileReadDto.TypeFile},fileReadDto);
        }
        [Route("SaveFile/{currentDirectory}")]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveFile(string currentDirectory)
        {
            Console.WriteLine("--> Upload File");
            var updatepath = currentDirectory.Split('*').ToList().Count!=0 ? _env.ContentRootPath + @"\" + currentDirectory.Replace("*",@"\").ToString() : _env.ContentRootPath + @"\" + currentDirectory;
            System.Console.WriteLine(updatepath);
            try
            {
                var httpRequest = Request.Form;
                System.Console.WriteLine(httpRequest);
                foreach (var postedFile in httpRequest.Files)
                {
                    string fileName = postedFile.FileName;
                    var physicalPath = updatepath +@"\" + fileName;

                    using (var stream = new FileStream(physicalPath,FileMode.Create))
                    {
                        await postedFile.CopyToAsync(stream);
                        Console.WriteLine(DateTime.UtcNow + fileName);
                    }
                }
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch(Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
        [Route("SaveFile2")]
        [HttpPost]
        public async Task<HttpStatusCode> SaveFile2(List<ParameterRequest> parameter) //[FromQuery] string currentDirectory,
        {
            System.Console.WriteLine("-->Start Work");
            var listTime = await _iFileSortRepository.SaveFile2(parameter);
            if(listTime.Count!=0)
                return HttpStatusCode.Created; 
            return HttpStatusCode.BadRequest;
        }
        [HttpDelete]
        public HttpStatusCode DeleteFile(ParameterRequest parameter)
        {
            Console.WriteLine("--> Delete File");
            try
            {
                var tryDeleteFile = _iFileSortRepository.DeleteFile(parameter);
                if(tryDeleteFile == HttpStatusCode.OK)
                {
                    return tryDeleteFile;
                }
                else
                {
                    return tryDeleteFile;
                }
            }
            catch(Exception)
            {
                return HttpStatusCode.NotFound;
            }
            
        }
        [Route("RenameFile")]
        [HttpPut]
        public JsonResult RenameFile(ParameterRequest parameter)
        {
            Console.WriteLine("--> Rename File");
            System.Console.WriteLine(parameter.currentDirectory);
            try
            {
                var tryRename = _iFileSortRepository.RenameFile(parameter.nameFile,parameter.typeFile,parameter.newNameFile,parameter.currentDirectory);
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
        [Route("EditFile")]
        [HttpPut]
        public JsonResult EditFile(WorkWithFile parameter)
        {
            var getStatus = _iFileSortRepository.EditFile(parameter);
            return new JsonResult(getStatus);
        }
        [Route("CheckJson")]
        [HttpGet]
        public string CheckJson()
        {
            WriteScript writeScript = new WriteScript();
            return writeScript.WriteScriptAll<Model.Migrations.ArchitectureFolder>("DateForArchitecture.json").ToString();
        }
    }
}