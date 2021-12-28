using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PhotoService.Model;

namespace PhotoService.SyncDataServices.Http
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
        public async Task<InfoAboutPhotos> GetOnlyPhoto(string pathFolder,string extension)
        {
            var stringReq = $"{_configuration["FileSortService"]}File/{pathFolder}/{extension}";
            var response = await _httpClient.GetAsync(stringReq);
            if(response.IsSuccessStatusCode)
            {
                var getFile = await response.Content.ReadAsStringAsync();
                var body = JsonSerializer.Deserialize<InfoAboutPhotos>(getFile);
                return body; 
            }
            else
            {
                return null;
            }
            
        }
    }
}