using FileSortService.Model.DatabaseModel;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Collections.Specialized;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Text.Json;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;

namespace FileSortService.Data
{
    public class DeserealizeJsonDate
    {
        private readonly Assembly assembly = Assembly.GetExecutingAssembly();
        public StringBuilder ReturnDateForArchitecture(string action)
        {
            StringBuilder scriptArchitecture = new StringBuilder().Append("Insert Into [Architecture] (Id,nameFile,typeFile,typeCategoryId,linkToOpen,sizeFile,dateCreatedFile,isFolder,fileInFolder,pathfolder)\n").Append("Values ");
            StringBuilder scriptArchitectureRemove = new StringBuilder().Append("Delete from Architecture \n ").Append("Where ");
            List<ArchitectureFolder> jsonArchitecture = new();
            try
            {
                var resourcePath = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("DateForArchitecture.json"));
                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonArchitecture = JsonConvert.DeserializeObject<List<ArchitectureFolder>>(reader.ReadToEnd());
                }
                foreach (var item in jsonArchitecture)
                {
                    switch (action)
                    {
                        case "Up":
                            var folder = item.isFolder == true ? 1 : 0;
                            scriptArchitecture.Append($"('{item?.Id}','{item?.nameFile}','{item.typeFile}',{(item?.typeCategory?.Id == null ? "null" : $"'{item.typeCategory.Id}'")},'{item?.linkToOpen}','{item?.sizeFile}','{item?.dateCreatedFile}',{folder},{item?.fileInFolder},'{item?.pathfolder}'),\n");
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
            catch (Exception ex)
            {
                StringBuilder exception = new StringBuilder();
                if (ex.Message == "Sequence contains no matching element")
                {
                    exception = new StringBuilder("File in this name not found in project");
                }
                return exception;
            }

        }
        public StringBuilder ReturnDateForExtenCategory(string action)
        {
            StringBuilder scriptExtenCategory = new StringBuilder().Append("Insert Into [ExtenCategory] (Id,nameCategory)\n").Append("Values ");
            StringBuilder scriptExtenCategoryRemove = new StringBuilder().Append("Delete from ExtenCategory \n ").Append("Where ");
            List<ExtensionCategory> jsonExtenCategory = new();
            try
            {
                var resourcePath = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("DateForExtenCategory.json"));
                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonExtenCategory = JsonConvert.DeserializeObject<List<ExtensionCategory>>(reader.ReadToEnd());
                }

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
            catch (Exception ex)
            {
                StringBuilder exception = new StringBuilder();
                if (ex.Message == "Sequence contains no matching element")
                {
                    exception = new StringBuilder("File in this name not found in project");
                }
                return exception;
            }
        }
        public StringBuilder ReturnDateForExtenValue(string action)
        {
            StringBuilder scriptExtenVal = new StringBuilder().Append("Insert Into [ExtenValue] (Id,extensionCategoryId,extensionValue)\n").Append("Values ");
            StringBuilder scriptExtenValRemove = new StringBuilder().Append("Delete from ExtenValue \n ").Append("Where ");
            List<ExtensionValue> jsonExtenValue = new();
            try
            {
                var resourcePath = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("DateForExtenValue.json"));
                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonExtenValue = JsonConvert.DeserializeObject<List<ExtensionValue>>(reader.ReadToEnd());
                }
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
            catch (Exception ex)
            {
                StringBuilder exception = new StringBuilder();
                if (ex.Message == "Sequence contains no matching element")
                {
                    exception = new StringBuilder("File in this name not found in project");
                }
                return exception;
            }
        }
        public StringBuilder ReturnDateForUploadCheck(string action)
        {
            StringBuilder scriptUpload = new StringBuilder().Append("Insert Into [UploadCheck] (Id,typeCategoryId,typeFile,hexSignature)\n").Append("Values ");
            StringBuilder scriptUploadRemove = new StringBuilder().Append("Delete from UploadCheck \n ").Append("Where ");
            List<TypeFileFromUpload> jsonUpload = new();
            try
            {
                var resourcePath = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("DateForUploadCheck.json"));
                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonUpload = JsonConvert.DeserializeObject<List<TypeFileFromUpload>>(reader.ReadToEnd());
                }
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
            catch (Exception ex)
            {
                StringBuilder exception = new StringBuilder();
                if (ex.Message == "Sequence contains no matching element")
                {
                    exception = new StringBuilder("File in this name not found in project");
                }
                return exception;
            }
        }
        public List<TOut> TryGetCollection<TOut>(string filename)
        {
            List<TOut> jsonDesrealize = new();
            var resourcePath = assembly.GetManifestResourceNames()
            .Single(str => str.EndsWith(filename));
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonDesrealize = JsonConvert.DeserializeObject<List<TOut>>(reader.ReadToEnd(), jsonSerializerSettings);
            }
            return jsonDesrealize;
        }
    }
    public class WriteScript 
    {
        public StringBuilder WriteScriptAll<TOut>(string filename)
        {
            DeserealizeJsonDate deserealizeJsonDate = new DeserealizeJsonDate();
            var jsonDesrealize = deserealizeJsonDate.TryGetCollection<TOut>(filename);
            var script = HeaderInsert<TOut>(jsonDesrealize[0]);
            var exp = CreatePredicate<TOut>(script[1].ToString()).Result;
            foreach (var item in jsonDesrealize)
            {
                script[0].Append("(" + exp(item)+ "),\n");
            }
            return script[0].Remove(script[0].Length - 2,1);
        }
        
        public List<StringBuilder> HeaderInsert<TOut>(TOut obj)
        {
            StringBuilder script = new StringBuilder($"Insert Into {obj.GetType().Name} (");
            StringBuilder scriptValues = new StringBuilder("item => {return $\"");
            int countProperty = 0;
            var nameColumn = obj.GetType().GetProperties();
            for (int i = 0; i < nameColumn.Length; i++)
            {
                var jsonIgnoreAttribute = nameColumn[i].CustomAttributes.Count() != 0 ? nameColumn[i].CustomAttributes.ToList().FirstOrDefault().AttributeType.Name : "none";
                if (jsonIgnoreAttribute != "JsonIgnoreAttribute") 
                {
                    if (nameColumn[i].PropertyType.FullName.StartsWith(Assembly.GetCallingAssembly().GetName().Name))
                    {
                        if (i == nameColumn.Length - 1)
                        {
                            script.Append($"{nameColumn[i].Name + "Id"})\n");
                            scriptValues.Append("{((item?." + nameColumn[i].Name + "?.Id.ToString() ?? null) == null ? \"null\" : $\"'{item." + nameColumn[i].Name + ".Id}'\")}");
                            countProperty++;
                            continue;
                        }
                        else
                        {
                            script.Append($"{nameColumn[i].Name + "Id"},");
                            scriptValues.Append("{((item?." + nameColumn[i].Name + "?.Id.ToString() ?? null) == null ? \"null\" : $\"'{item." + nameColumn[i].Name + ".Id}'\")},");
                            countProperty++;
                            continue;
                        }
                    }
                    else
                    {
                        switch (nameColumn[i].PropertyType.FullName)
                        {
                            case "System.Int32":
                                script.Append($"{nameColumn[i].Name},");
                                scriptValues.Append("{item." + nameColumn[i].Name + "},");
                                countProperty++;
                                break;
                            case "System.Boolean":
                                script.Append($"{nameColumn[i].Name},");
                                scriptValues.Append("{(item." + nameColumn[i].Name + " == true ? 1 : 0)},");
                                countProperty++;
                                break;
                            default:
                                script.Append($"{nameColumn[i].Name},");
                                scriptValues.Append("'{item." + nameColumn[i].Name + "}',");
                                countProperty++;
                                break;

                        }
                    }
                }
                
            }
            scriptValues.Remove(scriptValues.Length - 1, 1).Append("\";}");

            script.Remove(script.Length - 1, 1).Append(")\nValues ");

            List<StringBuilder> scripts = new List<StringBuilder>() { script, scriptValues };
            return scripts;
        }
        public async Task<Func<T, string>> CreatePredicate<T>(string command)
        {
            var options = ScriptOptions.Default.AddReferences(typeof(T).Assembly);
            var predicate = await CSharpScript.EvaluateAsync<Func<T, string>>(command, options).ConfigureAwait(true);
            return predicate;
        }
    }
}
