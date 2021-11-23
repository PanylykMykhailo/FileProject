using FileSortService.Model.DatabaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSortService.Data
{
    public class DeserealizeJsonDate
    {
        private readonly string rootArchitecture = @"C:\Users\mpanylyk\source\repos\File\FileProject\FileSortService\Data\DateForArchitecture.json";
        private readonly string rootExtenCategory = @"C:\Users\mpanylyk\source\repos\File\FileProject\FileSortService\Data\DateForExtenCategory.json";
        private readonly string rootExtenValue = @"C:\Users\mpanylyk\source\repos\File\FileProject\FileSortService\Data\DateForExtenValue.json";
        private readonly string rootUploadCheck = @"C:\Users\mpanylyk\source\repos\File\FileProject\FileSortService\Data\DateForUploadCheck.json";

        public StringBuilder ReturnDateForArchitecture(string action) 
        {
            StringBuilder scriptArchitecture = new StringBuilder().Append("Insert Into [Architecture] (Id,nameFile,typeFile,typeCategoryId,linkToOpen,sizeFile,dateCreatedFile,isFolder,fileInFolder,pathfolder)\n").Append("Values ");
            StringBuilder scriptArchitectureRemove = new StringBuilder().Append("Delete from Architecture \n ").Append("Where ");
            var jsonArchitecture = JsonConvert.DeserializeObject<List<ArchitectureFolder>>(File.ReadAllText(rootArchitecture));
            foreach (var item in jsonArchitecture)
            {
                switch (action) 
                {
                    case "Up":
                        var folder = item.isFolder == true ? 1 : 0;
                        var typeCategory = item?.typeCategory?.Id == null ? "null" : $"'{item.typeCategory.Id}'";
                        scriptArchitecture.Append($"('{item?.Id}','{item?.nameFile}','{item.typeFile}',{typeCategory},'{item?.linkToOpen}','{item?.sizeFile}','{item?.dateCreatedFile}',{folder},{item?.fileInFolder},'{item?.pathfolder}'),\n");
                        break;
                    case "Down":
                        scriptArchitectureRemove.Append($"Id = '{item.Id}' or\n");
                        break;
                    default:
                        break;
                }
            }
            if (action == "Up") 
            {
                return scriptArchitecture.Remove(scriptArchitecture.Length - 2, 1);
            }
            return scriptArchitectureRemove.Remove(scriptArchitectureRemove.Length - 3, 2); 
        }
        public StringBuilder ReturnDateForExtenCategory(string action)
        {
            StringBuilder scriptExtenCategory = new StringBuilder().Append("Insert Into [ExtenCategory] (Id,nameCategory)\n").Append("Values ");
            StringBuilder scriptExtenCategoryRemove = new StringBuilder().Append("Delete from ExtenCategory \n ").Append("Where ");
            var jsonExtenCategory = JsonConvert.DeserializeObject<List<ExtensionCategory>>(File.ReadAllText(rootExtenCategory));
            foreach (var item in jsonExtenCategory)
            {
                switch (action) 
                {
                    case "Up":
                        scriptExtenCategory.Append($"('{item.Id}','{item.nameCategory}'),\n");
                        break;
                    case "Down":
                        scriptExtenCategoryRemove.Append($"Id = '{item.Id}' or\n");
                        break;
                    default:
                        break;
                }
                
            }
            if (action == "Up") 
            {
                return scriptExtenCategory.Remove(scriptExtenCategory.Length - 2, 1);
            }
            return scriptExtenCategoryRemove.Remove(scriptExtenCategoryRemove.Length - 3, 2);
        }
        public StringBuilder ReturnDateForExtenValue(string action)
        {
            StringBuilder scriptExtenVal = new StringBuilder().Append("Insert Into [ExtenValue] (Id,extensionCategoryId,extensionValue)\n").Append("Values ");
            StringBuilder scriptExtenValRemove = new StringBuilder().Append("Delete from ExtenValue \n ").Append("Where ");
            var jsonExtenValue = JsonConvert.DeserializeObject<List<ExtensionValue>>(File.ReadAllText(rootExtenValue));
            foreach (var item in jsonExtenValue)
            {
                switch (action)
                {
                    case "Up":
                        var extensionCategory = item?.extensionCategory?.Id == null ? "null" : $"'{item.extensionCategory.Id}'";
                        scriptExtenVal.Append($"('{item.Id}',{extensionCategory},'{item.extensionValue}'),\n");
                        break;
                    case "Down":
                        scriptExtenValRemove.Append($"Id = '{item.Id}' or\n");
                        break;
                    default:
                        break;
                }
            }
            if (action == "Up")
            {
                return scriptExtenVal.Remove(scriptExtenVal.Length - 2, 1);
            }
            return scriptExtenValRemove.Remove(scriptExtenValRemove.Length - 3, 2);
        }
        public StringBuilder ReturnDateForUploadCheck(string action)
        {
            StringBuilder scriptUpload = new StringBuilder().Append("Insert Into [UploadCheck] (Id,typeCategoryId,typeFile,hexSignature)\n").Append("Values ");
            StringBuilder scriptUploadRemove = new StringBuilder().Append("Delete from UploadCheck \n ").Append("Where ");
            var jsonUpload = JsonConvert.DeserializeObject<List<TypeFileFromUpload>>(File.ReadAllText(rootUploadCheck));
            foreach (var item in jsonUpload)
            {
                switch (action)
                {
                    case "Up":
                        var extensionCategory = item?.extensionCategory?.Id == null ? "null" : $"'{item.extensionCategory.Id}'";
                        scriptUpload.Append($"('{item.Id}',{extensionCategory},'{item.typeFile}','{item.hexSignature}'),\n");
                        break;
                    case "Down":
                        scriptUploadRemove.Append($"Id = '{item.Id}' or\n");
                        break;
                    default:
                        break;
                }
               
            }
            if (action == "Up")
            {
                return scriptUpload.Remove(scriptUpload.Length - 2, 1);
            }
            return scriptUploadRemove.Remove(scriptUploadRemove.Length - 3, 2);
        }
    }
}
