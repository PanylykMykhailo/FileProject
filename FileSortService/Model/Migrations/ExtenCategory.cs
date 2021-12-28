using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FileSortService.Model.Migrations
{
    public class ExtenCategory
    {
        [Key]
        public Guid Id { get; set; }
        public string nameCategory { get; set; }
    }
}