using FileSortService.Model.DatabaseModel;
using LumenWorks.Framework.IO.Csv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSortService.Data
{
    public class AutofillTypeFile
    {
        private readonly string root = @"C:\Users\mpanylyk\source\repos\File\FileProject\FileSortService\";
        private readonly string rootTest = @"C:\Users\mpanylyk\Desktop\Test\Test.txt";
        StringBuilder script = new StringBuilder().Append("Insert Into [Architecture] (Id,nameFile,typeFile,typeCategoryId,linkToOpen,sizeFile,dateCreatedFile,isFolder,fileInFolder,pathfolder)").Append("Values ");
        private readonly AppDbContext _context;
        public AutofillTypeFile(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ArchitectureFolder> GetArchitecture() 
        {
            List<string> dateFolder = new List<string>() { root + "Test" };
            List<ArchitectureFolder> include = new List<ArchitectureFolder>();
            var result = ReturnAllIncludeFolder(dateFolder, include);
            File.AppendAllText(rootTest, script.ToString());
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
                fileInfo.Add(new ArchitectureFolder
                {
                    Id = id,
                    nameFile = Path.GetFileNameWithoutExtension(item),
                    typeFile = infoFile.Extension,
                    typeCategory = _context.ExtenValue.Select(x => new ExtensionValue
                    {
                        Id = x.Id,
                        extensionCategory = x.extensionCategory,
                        extensionValue = x.extensionValue
                    })
                    .Where(u => u.extensionValue == infoFile.Extension).FirstOrDefault().extensionCategory,
                    linkToOpen = $"/{checkFolder.Replace("*", "/")}/{Path.GetFileName(item)}",
                    sizeFile = infoFile.Length.ToString() + " bytes",
                    dateCreatedFile = infoFile.CreationTime.ToShortDateString() + " " + infoFile.CreationTime.ToShortTimeString(),
                    isFolder = false,
                    pathfolder = checkFolder
                });
                script.Append($"('{id}','{Path.GetFileNameWithoutExtension(item)}','{infoFile.Extension}',null,'/{checkFolder.Replace("*", "/")}/{Path.GetFileName(item)}','{infoFile.Length.ToString()} bytes','{infoFile.CreationTime.ToShortDateString()} {infoFile.CreationTime.ToShortTimeString()}',0,0,'{checkFolder}'),\n");
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
                    typeFile = aboutFolder.Extension,
                    sizeFile = aboutFolder.GetFiles().Length + " bytes",
                    dateCreatedFile = aboutFolder.CreationTime.ToShortDateString() + " " + aboutFolder.CreationTime.ToShortTimeString(),
                    isFolder = true,
                    fileInFolder = Directory.GetFiles(item, $"*.*", SearchOption.AllDirectories).Length,
                    pathfolder = checkFolder
                });
                script.Append($"('{id}','{aboutFolder.Name}','{aboutFolder.Extension}',null,null,'{aboutFolder.GetFiles().Length} bytes','{aboutFolder.CreationTime.ToShortDateString()} {aboutFolder.CreationTime.ToShortTimeString()}',1,{Directory.GetFiles(item, $"*.*", SearchOption.AllDirectories).Length},'{checkFolder}')");
            }
            return folderInfo;
        }
        public IEnumerable<TypeFileFromUpload> GetDate()
        {
            List<TypeFileFromUpload> simpleTest2 = new();
            simpleTest2.AddRange(ReturnCollaritive());
            return simpleTest2;
        }
        public List<TypeFileFromUpload> ReturnCollaritive()
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
        public List<TypeFileFromUpload> Replace(string hex, string exten)
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
                        infoExtensions.Add(new TypeFileFromUpload { Id = Guid.NewGuid(), hexSignature = massHex[i].Replace(" ", ""), typeFile = massExten[j]});//, extensionCategory = Guid.Parse("10E0F285-67FF-410A-9922-064CBF97B30D")
                    }
                }
            }
            return infoExtensions;
        }
        //public StringBuilder Script() 
        //{
        //    StringBuilder script = new StringBuilder();
        //    script.Append("INSERT INTO[UploadCheck] (Id, typeCategoryId, hexSignature, typeFile) \n")
        //        .Append("Values ('82b05e21-527a-4402-84f7-dd2ecaf58299', null, 'a1b2c3d4', 'pcap'), \n")
        //        .Append("       ('364c9d93-ec27-4afd-893e-fec1bc3c8c56', null, '455202000000', 'toast')");
        //    return script;
        //}
    }
    public class SearchParameters
    {
        public string HexCode { get; set; }
        public string ISO { get; set; }
        public string Way { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
    }
}
