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
using System.Data;
using LumenWorks.Framework.IO.Csv;
using Newtonsoft.Json;

namespace FileSortService.Repository
{
    public class FileSortRepository : IFileSortRepository
    {
        //private string rootPath = "Test";
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string root = @"C:\Users\mpanylyk\source\repos\File\FileProject\FileSortService\";
        private readonly string rootTest = @"C:\Users\mpanylyk\Desktop\Test\Test.txt";
        StringBuilder script = new StringBuilder().Append("Insert Into [Architecture] (Id,nameFile,typeFile,typeCategoryId,linkToOpen,sizeFile,dateCreatedFile,isFolder,fileInFolder,pathfolder)\n").Append("Values ");
        StringBuilder scriptUpload = new StringBuilder().Append("Insert Into [UploadCheck] (Id,typeCategoryId,typeFile,hexSignature)\n").Append("Values ");
        StringBuilder scriptExtenCategory = new StringBuilder().Append("Insert Into [ExtenCategory] (Id,nameCategory)\n").Append("Values ");
        StringBuilder scriptExtenVal = new StringBuilder().Append("Insert Into [ExtenValue] (Id,extensionCategoryId,extensionValue)\n").Append("Values ");
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
                    _context.Architecture.Add(new ArchitectureFolder
                    {
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
                    _context.Architecture.Add(new ArchitectureFolder
                    {
                        Id = Guid.NewGuid(),
                        nameFile = infoAboutFile.NameFile,
                        typeFile = infoAboutFile.TypeFile,
                        isFolder = infoAboutFile.isFolder,
                        dateCreatedFile = DateTime.Now.ToShortDateString(),
                        linkToOpen = $"{_configuration["WorkLink"]}/{infoAboutFile.currentDirectory.Replace("*", "/")}/{infoAboutFile.NameFile}.{infoAboutFile.TypeFile}",
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
            List<string> timeSpend = new();
            int iteration = 0;
            foreach (var item in parameter)
            {
                var timer = new Stopwatch();
                timer.Start();
                if (!String.IsNullOrEmpty(item.dateFile))
                {
                    System.Console.WriteLine("-->Start to {0}", iteration);
                    Byte[] byteses = Convert.FromBase64String(item.dateFile);
                    await File.WriteAllBytesAsync(rootPath + @"\Test\" + item.nameFile, byteses);
                    System.Console.WriteLine("-->Finish to {0}", iteration);
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
                timer.Stop();
                timeSpend.Add(timer.Elapsed.ToString());     
            }
            return timeSpend;
        }
        public InfoAboutFiles GetAllFileV2(string pathFolder, string typeFile)
        {
            var checkFolder = CheckFolderV2(pathFolder, typeFile);
            InfoAboutFiles infoAboutFiles = new();
            infoAboutFiles.folderPath = pathFolder.Split('*').ToList();
            infoAboutFiles.infoaboutFile = new();
            if (checkFolder != null)
            {
                infoAboutFiles.infoaboutFile.AddRange(checkFolder);
            }
            var files = _context.Architecture.Select(x => x).Where(y => y.pathfolder == rootPath && !y.isFolder).ToList();
            foreach (var item in files)
            {
                //var infoFile = new System.IO.FileInfo(item);
                infoAboutFiles.infoaboutFile.Add(new InfoAboutFile()
                {
                    NameFile = item.nameFile,
                    TypeFile = item.typeFile,
                    typeCategory = _context.ExtenValue.Select(x => new ExtensionValue
                    {
                        Id = x.Id,
                        extensionCategory = x.extensionCategory,
                        extensionValue = x.extensionValue
                    })
                    .Where(u => u.Id == item.typeCategory.Id).FirstOrDefault().extensionCategory.nameCategory,
                    linkToOpen = item.linkToOpen,
                    SizeFile = item.sizeFile,
                    DateCreatedFile = item.dateCreatedFile
                });
            }
            return infoAboutFiles;
        }
        public List<InfoAboutFile> CheckFolderV2(string rootPath,string typeFile)
        {
            var folder = _context.Architecture.Select(x => x).Where(y => y.pathfolder == rootPath).ToList();
            if (folder.Count != 0)
            {
                List<InfoAboutFile> infoaboutFileinFolder = new List<InfoAboutFile>();
                foreach (var item in folder)
                {
                    infoaboutFileinFolder.Add(new InfoAboutFile()
                    {
                        NameFile = item.nameFile,
                        TypeFile = item.typeFile,
                        SizeFile = item.sizeFile,
                        DateCreatedFile = item.dateCreatedFile,
                        isFolder = true,
                        fileInFolder = _context.Architecture.Where(x => x.pathfolder == rootPath+"*"+item.nameFile).Count()
                    });
                }
                return infoaboutFileinFolder;
            }
            return null;
        }



        /////////AutofillTypeFile//////////////////////////
        public IEnumerable<ArchitectureFolder> GetArchitecture()
        {
            List<string> dateFolder = new List<string>() { root + "Test" };
            List<ArchitectureFolder> include = new List<ArchitectureFolder>();
            var result = ReturnAllIncludeFolder(dateFolder, include);
            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            File.WriteAllText(@"C:\Users\mpanylyk\source\repos\File\FileProject\FileSortService\Data\Jsons\DateForArchitecture.json", JsonConvert.SerializeObject(result, Formatting.Indented, jsSettings));
            File.AppendAllText(rootTest, script.ToString() + "\n\n");
            return result;
        }
        public List<ArchitectureFolder> ReturnAllIncludeFolder(List<string> folder, List<ArchitectureFolder> include)
        {
            List<string> listDirectory = new List<string>();

            foreach (var item in folder)
            {
                var derictory = Directory.GetDirectories(item).ToList();
                include.AddRange(InfoAboutFile(item));
                if (derictory.Count != 0)
                {
                    include.AddRange(InfoAboutFolder(item));
                    listDirectory.AddRange(derictory);
                }
            }
            if (listDirectory.Count == 0)
            {
                return include;
            }
            folder.Clear();
            folder.AddRange(listDirectory);
            return ReturnAllIncludeFolder(folder, include);
        }
        public List<ArchitectureFolder> InfoAboutFile(string folderActive)
        {
            var checkFolder = folderActive.Replace(root, "").Split('\\').ToList().Count != 0 ? folderActive.Replace(root, "").Replace("\\", "*").ToString() : folderActive.Replace(root, "").ToString();
            var pathTofile = Directory.GetFiles(folderActive, $"*.*", SearchOption.TopDirectoryOnly).ToList();
            List<ArchitectureFolder> fileInfo = new List<ArchitectureFolder>();
            foreach (var item in pathTofile)
            {
                var infoFile = new System.IO.FileInfo(item);
                Guid id = Guid.NewGuid();
                var typeCatr = _context.ExtenValue.Select(x => new ExtensionValue
                {
                    Id = x.Id,
                    extensionCategory = x.extensionCategory,
                    extensionValue = x.extensionValue
                })
                    .Where(u => u.extensionValue == infoFile.Extension).FirstOrDefault().extensionCategory;
                if (typeCatr != null)
                {
                    fileInfo.Add(new ArchitectureFolder
                    {
                        Id = id,
                        nameFile = Path.GetFileNameWithoutExtension(item),
                        typeFile = infoFile.Extension,
                        typeCategory = typeCatr,
                        linkToOpen = $"/{checkFolder.Replace("*", "/")}/{Path.GetFileName(item)}",
                        sizeFile = infoFile.Length.ToString() + " bytes",
                        dateCreatedFile = infoFile.CreationTime.ToShortDateString() + " " + infoFile.CreationTime.ToShortTimeString(),
                        isFolder = false,
                        pathfolder = checkFolder
                    });
                    script.Append($"('{id}','{Path.GetFileNameWithoutExtension(item)}','{infoFile.Extension}','{typeCatr.Id}','/{checkFolder.Replace("*", "/")}/{Path.GetFileName(item)}','{infoFile.Length.ToString()} bytes','{infoFile.CreationTime.ToShortDateString()} {infoFile.CreationTime.ToShortTimeString()}',0,0,'{checkFolder}'),\n");
                }
                else 
                {
                    fileInfo.Add(new ArchitectureFolder
                    {
                        Id = id,
                        nameFile = Path.GetFileNameWithoutExtension(item),
                        typeFile = infoFile.Extension,
                        linkToOpen = $"/{checkFolder.Replace("*", "/")}/{Path.GetFileName(item)}",
                        sizeFile = infoFile.Length.ToString() + " bytes",
                        dateCreatedFile = infoFile.CreationTime.ToShortDateString() + " " + infoFile.CreationTime.ToShortTimeString(),
                        isFolder = false,
                        pathfolder = checkFolder
                    });
                    script.Append($"('{id}','{Path.GetFileNameWithoutExtension(item)}','{infoFile.Extension}',null,'/{checkFolder.Replace("*", "/")}/{Path.GetFileName(item)}','{infoFile.Length.ToString()} bytes','{infoFile.CreationTime.ToShortDateString()} {infoFile.CreationTime.ToShortTimeString()}',0,0,'{checkFolder}'),\n");
                }
                
            }
            return fileInfo;
        }
        public List<ArchitectureFolder> InfoAboutFolder(string folderActive)
        {
            List<ArchitectureFolder> folderInfo = new List<ArchitectureFolder>();
            var pathTofolder = Directory.GetDirectories(folderActive).ToList();
            foreach (var item in pathTofolder)
            {
                var checkFolder = folderActive.Replace(root, "").Split('\\').ToList().Count != 0 ? folderActive.Replace(root, "").Replace("\\", "*").ToString() : folderActive.Replace(root, "").ToString();
                var aboutFolder = new System.IO.DirectoryInfo(item);
                Guid id = Guid.NewGuid();
                folderInfo.Add(new ArchitectureFolder()
                {
                    Id = id,
                    nameFile = aboutFolder.Name,
                    typeFile = "",
                    sizeFile = aboutFolder.GetFiles().Length + " bytes",
                    dateCreatedFile = aboutFolder.CreationTime.ToShortDateString() + " " + aboutFolder.CreationTime.ToShortTimeString(),
                    isFolder = true,
                    fileInFolder = Directory.GetFiles(item, $"*.*", SearchOption.AllDirectories).Length,
                    pathfolder = checkFolder
                });
                script.Append($"('{id}','{aboutFolder.Name}','',null,null,'{aboutFolder.GetFiles().Length} bytes','{aboutFolder.CreationTime.ToShortDateString()} {aboutFolder.CreationTime.ToShortTimeString()}',1,{Directory.GetFiles(item, $"*.*", SearchOption.AllDirectories).Length},'{checkFolder}'),\n");
            }
            return folderInfo;
        }
        public IEnumerable<TypeFileFromUpload> GetDate()
        {
            List<TypeFileFromUpload> simpleTest2 = new();
            simpleTest2.AddRange(ReturnCollaritive());
            File.WriteAllText(@"C:\Users\mpanylyk\source\repos\File\FileProject\FileSortService\Data\Jsons\DateForUploadCheck.json", JsonConvert.SerializeObject(simpleTest2, Formatting.Indented));
            File.AppendAllText(rootTest, scriptUpload.ToString() + "\n\n");
            return simpleTest2;
        }
        public List<TypeFileFromUpload> ReturnCollaritive()//TypeFileFromUpload
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(@"C:\Users\mpanylyk\Desktop\Test\CSVFile.csv")), true))
            {
                csvTable.Load(csvReader);
            }
            List<SearchParameters> searchParameters = new List<SearchParameters>();
            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                searchParameters.Add(new SearchParameters
                {
                    HexCode = csvTable.Rows[i][0].ToString(),
                    ISO = csvTable.Rows[i][1].ToString(),
                    Way = csvTable.Rows[i][2].ToString(),
                    Extension = csvTable.Rows[i][3].ToString(),
                    Description = csvTable.Rows[i][4].ToString()
                }
                );
            }
            List<TypeFileFromUpload> infoExtensions = new List<TypeFileFromUpload>();
            for (int i = 0; i < searchParameters.Count; i++)
            {
                switch (i)
                {
                    case 55:
                        infoExtensions.AddRange(Replace(searchParameters[i].HexCode.Replace("\n", ";"), searchParameters[i].Extension.Replace("\n", ";")));
                        break;
                    case 73:
                        infoExtensions.AddRange(Replace(searchParameters[i].HexCode.Replace("\n", ";"), searchParameters[i].Extension.Replace("\n", ";")));
                        break;
                    case 74:
                        infoExtensions.AddRange(Replace(searchParameters[i].HexCode.Replace("\n", ";"), searchParameters[i].Extension.Replace("\n", ";")));
                        break;
                    case 101:
                        infoExtensions.AddRange(Replace(searchParameters[i].HexCode.Replace("\n", ";"), searchParameters[i].Extension.Replace("\n", ";")));
                        break;
                    default:
                        infoExtensions.AddRange(Replace(searchParameters[i].HexCode.Replace("\n\n", ";").Replace("\r", ""), searchParameters[i].Extension.Replace("\n", ";")));
                        break;
                }
            }
            return infoExtensions;
        }
        public List<TypeFileFromUpload> Replace(string hex, string exten)//TypeFileFromUpload
        {
            List<TypeFileFromUpload> infoExtensions = new List<TypeFileFromUpload>();
            var massHex = hex.Contains("\n") ? hex.Replace("\n", "").Split(';').ToList() : hex.Split(';').ToList();
            var massExten = exten.Split(';').ToList();
            for (int i = 0; i < massHex.Count; i++)
            {
                if (!massHex[i].StartsWith("("))
                {
                    for (int j = 0; j < massExten.Count; j++)
                    {
                        Guid id = Guid.NewGuid();
                        ExtensionCategory group = _context.ExtenValue.Select(x => new ExtensionValue
                        {
                            Id = x.Id,
                            extensionCategory = x.extensionCategory,
                            extensionValue = x.extensionValue
                        }).Where(u => u.extensionValue == "." + massExten[j]).FirstOrDefault()?.extensionCategory ?? null;
                        if (group == null)
                        {
                            infoExtensions.Add(new TypeFileFromUpload { Id = id, hexSignature = massHex[i].Replace(" ", ""), typeFile = massExten[j] });
                            scriptUpload.Append($"('{id}',null,'{massExten[j]}','{massHex[i].Replace(" ", "")}'),\n");
                        }
                        else 
                        {
                            infoExtensions.Add(new TypeFileFromUpload { Id = id, hexSignature = massHex[i].Replace(" ", ""), typeFile = massExten[j], extensionCategory = group });
                            scriptUpload.Append($"('{id}','{group.Id}','{massExten[j]}','{massHex[i].Replace(" ", "")}'),\n");
                        }
                    }
                }
            }
            return infoExtensions;
        }




        ////////////////////////Insert////////////////////////
        public void InsertExt() 
        {
            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            if (_context.ExtenCategory.Count() == 0)
            {
                List<ExtensionCategory> extenCategory = new();
                string[] category = new string[3] { "File", "Photo", "Video" };
                foreach (var item in category)
                {
                    Guid id = Guid.NewGuid();
                    extenCategory.Add(new ExtensionCategory { Id = id, nameCategory = item });
                    ///Script scriptExtenCategory
                    scriptExtenCategory.Append($"('{id}','{item}'),\n");
                    
                };
                File.WriteAllText(@"C:\Users\mpanylyk\source\repos\File\FileProject\FileSortService\Data\Jsons\DateForExtenCategory.json", JsonConvert.SerializeObject(extenCategory, Formatting.Indented, jsSettings));
                _context.ExtenCategory.AddRange(extenCategory);
                _context.SaveChanges();
                File.AppendAllText(rootTest, scriptExtenCategory.ToString() + "\n\n");
            }
            if (_context.ExtenValue.Count() == 0)
            {
                List<string> OnlyFile = new List<string> { ".txt", ".doc", ".docx", ".docm", ".rtf", ".odt", ".pdf", ".arj", ".zip", ".rar", ".tar", ".pcap", ".pcapng", ".rpm", ".bin", ".PIF", ".SEA", ".YTR", ".PDB", ".DBA", ".TDA", ".z", ".tar.z", ".bac", ".bz2", ".iff", ".svx", ".faxx", ".fax", ".ftxt", ".lz", ".exe", ".jar", ".ods", ".xlsx", ".apk", ".aar", ".class", ".iso", ".xls", ".msg", ".dex", ".vmdk", ".crx", ".fh8", ".cwk", ".toast", ".xar", ".dat", ".nes", ".tar", ".tox", ".7z", ".gz", ".tar.gz", ".xz", ".tar.xz", ".lz4", ".cab", ".stg", ".der", ".woff", ".woff2", ".XML", ".wasm", ".lep", ".deb", ".tsv", ".tsa", ".zlib", ".lzfse", ".avro", ".rc", ".p25", ".obt", ".pcv", ".pbt", ".pdt", ".pea", ".peb", ".pet", ".pgt", ".pjt", ".pkt", ".pmt", ".luac" };
                List<string> OnlyPhoto = new List<string> { ".svg", ".apng", ".fle", ".wlmp", ".bmp", ".gif", ".jpeg", ".tiff", ".png", ".eps", ".wmf", ".jpg", ".jfif", ".PIC", ".ico", ".tif", ".cr2", ".cin", ".dpx", ".exr", ".bpg", ".ilbm", ".lbm", ".ibm", ".acbm", ".anbm", ".odp", ".pptx", ".ps", ".psd", ".dib", ".fits", ".ppt", ".dmg", ".flif", ".djvu", ".djv", ".dcm", ".webp", ".ez2", ".ez3" };
                List<string> OnlyVideo = new List<string> { ".mp3", ".mp4", ".wav", ".wma", ".midi", ".avi", ".flv", ".swf", ".wmv", ".mov", ".mpeg",".3gp", ".3g2", ".8svx", ".8sv", ".snd", ".anim", ".smus", ".smu", ".mus", ".cmus", ".yuvn", ".yuv", ".aiff", ".aif", ".aifc", ".idx", ".vsdx", ".asf", ".ogg", ".oga", ".ogv", ".flac", ".mid", ".MLV", ".mkv", ".mka", ".mks", ".mk3d", ".webm", ".ts", ".m2p", ".vob", ".mpg", ".mpg", ".orc" };
                
                Dictionary<string, List<string>> lists = new Dictionary<string, List<string>>();
                lists.Add("OnlyFile", OnlyFile);
                lists.Add("OnlyPhoto", OnlyPhoto);
                lists.Add("OnlyVideo", OnlyVideo);
                List<ExtensionValue> extensionValues = new();
                foreach (var exten in lists.Keys)
                {
                    ExtensionCategory whatcategory = new();
                    switch (exten)
                    {
                        case "OnlyFile":
                            whatcategory = _context.ExtenCategory.Select(x => x).Where(u => u.nameCategory == "File").FirstOrDefault();
                            break;
                        case "OnlyPhoto":
                            whatcategory = _context.ExtenCategory.Select(x => x).Where(u => u.nameCategory == "Photo").FirstOrDefault();
                            break;
                        case "OnlyVideo":
                            whatcategory = _context.ExtenCategory.Select(x => x).Where(u => u.nameCategory == "Video").FirstOrDefault();
                            break;
                        default:
                            break;
                    }
                    if (whatcategory != null)
                    {
                        foreach (var item in lists[exten])
                        {
                            Guid id = Guid.NewGuid();
                            extensionValues.Add(new ExtensionValue { Id = id, extensionCategory = whatcategory, extensionValue = item });
                            //Script scriptExtenVal
                            scriptExtenVal.Append($"('{id}','{whatcategory.Id}','{item}'),\n");
                        }
                    }

                }
                File.WriteAllText(@"C:\Users\mpanylyk\source\repos\File\FileProject\FileSortService\Data\Jsons\DateForExtenValue.json", JsonConvert.SerializeObject(extensionValues, Formatting.Indented, jsSettings));
                _context.ExtenValue.AddRange(extensionValues);
                _context.SaveChanges();
                File.AppendAllText(rootTest,scriptExtenVal.ToString() + "\n\n");
            }
        }
    }
}