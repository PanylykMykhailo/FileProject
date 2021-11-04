using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FileService.Model;
using Microsoft.Extensions.Configuration;

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
        public async Task<List<InfoAboutFile>> GetOnlyFile(string extension)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(extension),
                Encoding.UTF8,
                "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["FileService"]}",httpContent);
            if(response.IsSuccessStatusCode)
            {
                //Console.WriteLine(response);
                return new List<InfoAboutFile>(){
                    new InfoAboutFile
                    {
                        nameFile = "hello",
                        typeFile = ".txt",
                        sizeFile = "",
                        dateCreatedFile = ""
                    }
                };
            }
            else
            {
                return null;
            }
            
        }
       /* public async Task SendFileSortToFileS(InfoAboutFileDto infoAboutFileDto)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize<InfoAboutFileDto>(infoAboutFileDto),
                Encoding.UTF8,
                "application/json");
            
            var response = await _httpClient.PostAsync($"{_configuration["FileService"]}",httpContent);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ToString());
                Console.WriteLine("--> Sync POST to FileService was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to FileService was NOT OK!");
            }
        }*/
    }
}