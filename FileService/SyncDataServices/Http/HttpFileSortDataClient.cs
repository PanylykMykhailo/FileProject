using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using FileService.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FileService.SyncDataServices.Http
{
    public class HttpFileSortDataClient : IFileSortDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public HttpFileSortDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;  
        }
        public async Task<InfoAboutFiles> GetOnlyFile(string pathFolder,string extension)
        {
            var stringReq = $"{_configuration["FileSortService"]}File/{pathFolder}/{extension}";
            var response = await _httpClient.GetAsync(stringReq);
            if(response.IsSuccessStatusCode)
            {
                var getFile = await response.Content.ReadAsStringAsync();
                //var body = JsonSerializer.Deserialize<InfoAboutFiles>(getFile);
                //return body;
                return null; 
            }
            else
            {
                return null;
            } 
        }
        public async Task<HttpStatusCode> DeleteFile(ParameterRequest parameter)
        {
            //var stringReq = $"{_configuration["FileSortService"]}File";
            var request = new HttpRequestMessage {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_configuration["FileSortService"]}File"),
                Content = new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json")
            };
            //var response = await client.SendAsync(request);
            var response = await _httpClient.SendAsync(request);  //GetAsync(stringReq);
            if(response.IsSuccessStatusCode)
            {
                var getFile = await response.Content.ReadAsStringAsync();
                //JsonTextReader reader = new JsonTextReader(new StringReader(getFile));
                //JsonReader exception = new JsonReader();
                //var body = JsonSerializer.Deserialize<InfoAboutFiles>(getFile);
                return HttpStatusCode.OK; 
            }
            else
            {
                return HttpStatusCode.BadRequest;
            } 
        }
        public async Task<string> EditFile(WorkWithFile parameter)
        {
            var content = new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_configuration["FileSortService"]}File/EditFile",content);
            if(response.IsSuccessStatusCode)
            {
                var getFile = await response.Content.ReadAsStringAsync();
                //var body = JsonSerializer.Deserialize<InfoAboutFiles>(getFile);
                return getFile; 
            }
            else
            {
               return null;
            }
            
        }
        public HttpStatusCode CreateFile(InfoAboutFile infoAboutFile)//async Task<>
        {
            return  HttpStatusCode.OK;
            // var updatepath = infoAboutFile.currentDirectory.Split('*').ToList().Count != 0 ? rootPath + @"\" + infoAboutFile.currentDirectory.Replace("*", @"\").ToString() : rootPath + @"\" + infoAboutFile.currentDirectory;
            // if (infoAboutFile.isFolder)
            // {
            //     string[] searchFile = Directory.GetDirectories(updatepath, $"*{infoAboutFile.NameFile}*", SearchOption.TopDirectoryOnly);
            //     if (searchFile.Length == 0)
            //     {
            //         Directory.CreateDirectory(updatepath + @"\" + infoAboutFile.NameFile);
            //         return HttpStatusCode.Created;
            //     }
            //     return HttpStatusCode.Conflict;
            // }
            // else
            // {
            //     FileInfo fi = new FileInfo(updatepath + @"\" + infoAboutFile.NameFile + infoAboutFile.TypeFile);
            //     DirectoryInfo di = new DirectoryInfo(updatepath);
            //     if (!di.Exists)
            //     {
            //         di.Create();
            //         return HttpStatusCode.Created;
            //     }
            //     if (!fi.Exists)
            //     {
            //         fi.Create().Dispose();
            //         return HttpStatusCode.Conflict;
            //     }
            //     return HttpStatusCode.Forbidden;
            // }
        }
        public string RenameFile(ParameterRequest parameter)
        {
            return null;
            // var content = new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json");
            // var response =  _httpClient.PutAsync($"{_configuration["FileSortService"]}File/RenameFile",content);
            // if(response.IsSuccessStatusCode)
            // {
            //     var getFile =  response.Content.ReadAsStringAsync();
            //     //var body = JsonSerializer.Deserialize<InfoAboutFiles>(getFile);
            //     return getFile; 
            // }
            // else
            // {
            //    return null;
            // }
        }
        public async Task<string> SaveFile2(List<ParameterRequest> parameter)
        {
            var content = new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["FileSortService"]}File",content);
            if(response.IsSuccessStatusCode)
            {
                var getFile = await response.Content.ReadAsStringAsync();
                //var body = JsonSerializer.Deserialize<InfoAboutFiles>(getFile);
                return getFile; 
            }
            else
            {
               return null;
            }
        }
    }
}