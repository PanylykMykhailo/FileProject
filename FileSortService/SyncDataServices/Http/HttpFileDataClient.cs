using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FileSortService.Dtos;
using Microsoft.Extensions.Configuration;

namespace FileSortService.SyncDataServices.Http
{
    public class HttpFileSDataClient : IFileDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public HttpFileSDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;  
        }
        public async Task SendFileSortToFileS(InfoAboutFileDto infoAboutFileDto)
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
        }
    }
}