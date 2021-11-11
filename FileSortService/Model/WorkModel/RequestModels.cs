using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileSortService.Model.WorkModel
{
    public class ParameterRequest
    {
        public string nameFile{get;set;}
        public string typeFile{get;set;}
        public string newNameFile{get;set;}
        public string currentDirectory{get;set;}
        public bool isFolder{get;set;}
    }
    public class WorkWithFile
    {
        public string nameFile{get;set;}
        public string typeFile{get;set;}
        public string currentDirectory{get;set;}
        public int workbranch{get;set;}
        public string content{get;set;}
    }
    public class ParameterResponse
    {
        public string response{get;set;}
    }
}

