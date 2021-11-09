using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileSortService.Model
{
    public class ParameterRequest
    {
        public string nameFile{get;set;}
        public string typeFile{get;set;}
        public string newNameFile{get;set;}
        public string currentDirectory{get;set;}
    }
    public class ParameterResponse
    {
        public string response{get;set;}
    }
}

