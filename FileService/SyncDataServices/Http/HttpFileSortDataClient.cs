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
        public async Task<InfoAboutFiles> GetOnlyFile(string pathFolder,string extension)
        {
            var stringReq = $"{_configuration["FileSortService"]}File/{pathFolder}/{extension}";
            var response = await _httpClient.GetAsync(stringReq);
            if(response.IsSuccessStatusCode)
            {
                var getFile = await response.Content.ReadAsStringAsync();
                var body = JsonSerializer.Deserialize<InfoAboutFiles>(getFile);
                return body; 
            }
            else
            {
                return null;
            } 
        }
        

    }
}