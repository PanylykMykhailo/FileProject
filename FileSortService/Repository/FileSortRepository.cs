using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Diagnostics;
using System.Text;
using FileSortService.Data;
using FileSortService.Model.WorkModel;
using FileSortService.Model.DatabaseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
namespace FileSortService.Repository
{
    public class FileSortRepository : IFileSortRepository
    {
        //private string rootPath = "Test";
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public FileSortRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        private string rootPath = Environment.CurrentDirectory;
        public InfoAboutFiles GetAllFile(string pathFolder, string typeFile)
        {
            var updatepath = pathFolder.Split('*').ToList().Count != 0 ? rootPath + @"\" + pathFolder.Replace("*", @"\").ToString() : rootPath + @"\" + pathFolder;
            var files = Directory.GetFiles(updatepath, $"*.{typeFile}*", SearchOption.TopDirectoryOnly);
            var checkFolder = CheckFolder(updatepath, typeFile);
            InfoAboutFiles infoAboutFiles = new();
            infoAboutFiles.folderPath = pathFolder.Split('*').ToList();
            infoAboutFiles.infoaboutFile = new();
            if (checkFolder != null)
            {
                infoAboutFiles.infoaboutFile.AddRange(checkFolder);
            }
            foreach (var item in files)
            {
                var infoFile = new System.IO.FileInfo(item);
                infoAboutFiles.infoaboutFile.Add(new InfoAboutFile()
                {
                    NameFile = Path.GetFileNameWithoutExtension(item),
                    TypeFile = infoFile.Extension,
                    typeCategory = _context.ExtenValue.Select(x => new ExtensionValue
                    {
                        Id = x.Id,
                        extensionCategory = x.extensionCategory,
                        extensionValue = x.extensionValue
                    })
                    .Where(u => u.extensionValue == infoFile.Extension).FirstOrDefault().extensionCategory.nameCategory,
                    linkToOpen = $"{_configuration["WorkLink"]}/{pathFolder.Replace("*", "/")}/{Path.GetFileName(item)}",
                    SizeFile = infoFile.Length.ToString() + " bytes",
                    DateCreatedFile = infoFile.CreationTime.ToShortDateString() + " " + infoFile.CreationTime.ToShortTimeString()
                });
            }
            return infoAboutFiles;

        }
        public InfoAboutFile InfoAboutFile(string nameFile, string typeName)
        {
            if (File.Exists(rootPath + @"\" + nameFile + typeName))
            {
                var searchFile = Directory.GetFiles(rootPath, $"*{nameFile}{typeName}*", SearchOption.AllDirectories);
                InfoAboutFile infoAboutFile = new InfoAboutFile();
                infoAboutFile.NameFile = Path.GetFileNameWithoutExtension(searchFile[0]);
                infoAboutFile.TypeFile = new System.IO.FileInfo(searchFile[0]).Extension;
                infoAboutFile.SizeFile = new System.IO.FileInfo(searchFile[0]).Length.ToString() + " bytes";
                infoAboutFile.DateCreatedFile = new System.IO.FileInfo(searchFile[0]).CreationTime.ToShortDateString() + " " + new System.IO.FileInfo(searchFile[0]).CreationTime.ToShortTimeString();
                return infoAboutFile;
            }
            else
            {
                return null;
            }
        }
        public HttpStatusCode DeleteFile(ParameterRequest parameter)
        {
            var updatepath = parameter.currentDirectory.Split('*').ToList().Count != 0 ? rootPath + @"\" + parameter.currentDirectory.Replace("*", @"\").ToString() : rootPath + @"\" + parameter.currentDirectory;
            var filePath = updatepath + @"\" + parameter.nameFile + parameter.typeFile;
            if (parameter.isFolder)
            {
                DirectoryInfo di = new DirectoryInfo(filePath);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                Directory.Delete(filePath);
                return HttpStatusCode.OK;
            }
            else
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return HttpStatusCode.OK;
                }
                return HttpStatusCode.Conflict;
            }

        }
        public string RenameFile(string nameFile, string typeName, string newNameFile, string currentDirectory)
        {
            var updatepath = currentDirectory.Split('*').ToList().Count != 0 ? rootPath + @"\" + currentDirectory.Replace("*", @"\").ToString() : rootPath + @"\" + currentDirectory;
            if (typeName == "folder")
            {
                Directory.Move(updatepath + @"\" + nameFile, updatepath + @"\" + newNameFile);
                return nameFile;
            }
            else
            {
                FileInfo fi = new FileInfo(updatepath + @"\" + nameFile + typeName);
                if (fi.Exists)
                {
                    System.IO.File.Move(updatepath + @"\" + nameFile + typeName, updatepath + @"\" + newNameFile + typeName);
                    return nameFile + typeName;
                }
                return null;
            }

        }
        public string EditFile(WorkWithFile parameter)
        {
            var updatepath = parameter.currentDirectory.Split('*').ToList().Count != 0 ? rootPath + @"\" + parameter.currentDirectory.Replace("*", @"\").ToString() : rootPath + @"\" + parameter.currentDirectory;
            updatepath = updatepath + @"\" + parameter.nameFile + parameter.typeFile;
            switch (parameter.workbranch)
            {
                case 1:
                    string text = System.IO.File.ReadAllText(updatepath);
                    return text;
                case 2:
                    File.Create(updatepath).Close();
                    File.AppendAllText(updatepath, parameter.content);
                    return "WorkWithFile";
                default:
                    break;
            }
            return "work";
        }
        public HttpStatusCode CreateFile(InfoAboutFile infoAboutFile)
        {
            var updatepath = infoAboutFile.currentDirectory.Split('*').ToList().Count != 0 ? rootPath + @"\" + infoAboutFile.currentDirectory.Replace("*", @"\").ToString() : rootPath + @"\" + infoAboutFile.currentDirectory;
            if (infoAboutFile.isFolder)
            {
                string[] searchFile = Directory.GetDirectories(updatepath, $"*{infoAboutFile.NameFile}*", SearchOption.TopDirectoryOnly);
                if (searchFile.Length == 0)
                {
                    _context.Architecture.Add( new ArchitectureFolder {
                        Id = Guid.NewGuid(),
                        nameFile = infoAboutFile.NameFile,
                        typeFile = "folder",
                        isFolder = infoAboutFile.isFolder,
                        dateCreatedFile = DateTime.Now.ToShortDateString(),
                        pathfolder = infoAboutFile.currentDirectory,
                        sizeFile = "0 bytes"
                    });
                    _context.SaveChanges();
                    Directory.CreateDirectory(updatepath + @"\" + infoAboutFile.NameFile);
                    return HttpStatusCode.Created;
                }
                return HttpStatusCode.Conflict;
            }
            else
            {
                FileInfo fi = new FileInfo(updatepath + @"\" + infoAboutFile.NameFile + infoAboutFile.TypeFile);
                DirectoryInfo di = new DirectoryInfo(updatepath);
                if (!di.Exists)
                {
                    di.Create();
                    return HttpStatusCode.Created;
                }
                if (!fi.Exists)
                {
                    _context.Architecture.Add( new ArchitectureFolder {
                        Id = Guid.NewGuid(),
                        nameFile = infoAboutFile.NameFile,
                        typeFile = infoAboutFile.TypeFile,
                        isFolder = infoAboutFile.isFolder,
                        dateCreatedFile = DateTime.Now.ToShortDateString(),
                        linkToOpen =  $"{_configuration["WorkLink"]}/{infoAboutFile.currentDirectory.Replace("*", "/")}/{infoAboutFile.NameFile}.{infoAboutFile.TypeFile}",
                        pathfolder = infoAboutFile.currentDirectory,
                        sizeFile = "0 bytes"
                    });
                    _context.SaveChanges();
                    fi.Create().Dispose();
                    return HttpStatusCode.Created;
                }
                return HttpStatusCode.Forbidden;
            }
        }
        public List<InfoAboutFile> CheckFolder(string rootPath, string typeFile)
        {
            var checking = Directory.GetDirectories(rootPath).Select(r => r.Replace(rootPath + @"\", "")).ToList();
            if (checking.Count != 0)
            {
                List<InfoAboutFile> infoaboutFileinFolder = new List<InfoAboutFile>();
                foreach (var item in checking)
                {
                    var aboutFolder = new System.IO.DirectoryInfo(rootPath + @"\" + item);
                    infoaboutFileinFolder.Add(new InfoAboutFile()
                    {
                        NameFile = item,
                        TypeFile = aboutFolder.Extension,
                        SizeFile = aboutFolder.GetFiles().Length + " bytes",
                        DateCreatedFile = aboutFolder.CreationTime.ToShortDateString() + " " + aboutFolder.CreationTime.ToShortTimeString(),
                        isFolder = true,
                        fileInFolder = Directory.GetFiles(rootPath + @"\" + item, $"*.{typeFile}*", SearchOption.AllDirectories).Length
                    });
                }
                return infoaboutFileinFolder;
            }
            return null;
        }
        public async Task<List<string>> SaveFile2(List<ParameterRequest> parameter) 
        {
            //WriteAllBytes
            List<string> timeSpend = new();
            int iteration = 0;
            foreach (var item in parameter)
            {
                var timer = new Stopwatch();
                timer.Start();
                if(!String.IsNullOrEmpty(item.dateFile))
                {
                    System.Console.WriteLine("-->Start to {0}",iteration);
                    Byte[] byteses = Convert.FromBase64String(item.dateFile);
                    await File.WriteAllBytesAsync(rootPath + @"\Test\" + item.nameFile, byteses);
                    System.Console.WriteLine("-->Finish to {0}",iteration);
                    iteration++;
                }
                else
                {
                    FileInfo fi = new FileInfo(rootPath + @"\Test\" + item.nameFile);
                    if (!fi.Exists)
                    {
                        fi.Create().Dispose();
                     }
                }
  //B: Run stuff you want timed
                timer.Stop();
                timeSpend.Add(timer.Elapsed.ToString());
                //TimeSpan timeTaken = timer.Elapsed;
                //string foo = "Time taken: " + timeTaken.ToString(@"m\:ss\.fff");     
            }
            return timeSpend;
            //System.Console.WriteLine(parameter.nameFile);
        }
    }
}