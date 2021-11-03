using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FileSortService.Model;

namespace FileSortService.Repository
{
    public class FileSortRepository : IFileSortRepository
    {
        //private string rootPath = "Test";
        private string rootPath = Environment.CurrentDirectory + @"\Test";
        public List<InfoAboutFile> GetAllFile()
        {
            var files = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories);
            //InfoAboutFiles infoAboutFile = new InfoAboutFiles();
            //infoAboutFile.CountFile = files.Length;
            List<InfoAboutFile> infoaboutFile = new();
            foreach (var item in files)
            {
                var infoFile = new System.IO.FileInfo(item);
                infoaboutFile.Add(new InfoAboutFile()
                {
                    NameFile = Path.GetFileNameWithoutExtension(item),
                    TypeFile = infoFile.Extension,
                    SizeFile = infoFile.Length.ToString() + " bytes",
                    DateCreatedFile = infoFile.CreationTime.ToShortDateString() +" " + infoFile.CreationTime.ToShortTimeString()
                });
            }
            return infoaboutFile;

        }
        public InfoAboutFile InfoAboutFile(string nameFile,string typeName)
        {
            if(File.Exists(rootPath + @"\" + nameFile + typeName))
            {
                var searchFile = Directory.GetFiles(rootPath,$"*{nameFile}{typeName}*",SearchOption.AllDirectories);
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
        public string DeleteFile(string nameFile,string typeName)
        {
            var filePath = rootPath + @"\" + nameFile + typeName;
            if (System.IO.File.Exists(filePath)) 
            { 
                System.IO.File.Delete(filePath);
                 return nameFile + typeName;
            }
           return null;
        }
        public string RenameFile(string nameFile,string typeName,string newNameFile)
        {
            FileInfo fi = new FileInfo(rootPath + @"\" + nameFile + typeName);
            if (fi.Exists)
            {
                System.IO.File.Move(rootPath + @"\" + nameFile + typeName, rootPath + @"\" + newNameFile + typeName);
                return nameFile + typeName;
            }
            return null;
        }
        public bool OpenAndEdit(string nameFile,string typeName,string infoAdd)
        {
            var filePath = rootPath + @"\" + nameFile + typeName;
            if (System.IO.File.Exists(filePath)) 
            { 
                File.AppendAllText(filePath, infoAdd);
                return true;
            }
            return false;
        }
        public bool CreateFile(InfoAboutFile infoAboutFile)
        {
            FileInfo fi = new FileInfo(rootPath + @"\" + infoAboutFile.NameFile + infoAboutFile.TypeFile);
            DirectoryInfo di = new DirectoryInfo(rootPath);
            if (!di.Exists)
            {
                di.Create();
            }
            if (!fi.Exists)
            {
                fi.Create().Dispose();
            }
            return true;
        }
    }
}