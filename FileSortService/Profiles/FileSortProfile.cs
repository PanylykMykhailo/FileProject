using System.Collections.Generic;
using AutoMapper;
using FileSortService.Dtos;
using FileSortService.Model;

namespace FileSortService.Profiles
{
    public class FileSortProfile: Profile
    {
        public FileSortProfile()
        {   
            
            //Source to Target
            CreateMap<InfoAboutFiles,FileReadDto>();
            CreateMap<InfoAboutFile,InfoAboutFileDto>();
            CreateMap<FileCreateDto,InfoAboutFiles>();
            CreateMap<InfoAboutFileDtos,InfoAboutFile>();
        }
    }
}