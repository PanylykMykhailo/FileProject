using FileSortService.Model.DatabaseModel;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileSortService.Data
{
    public class AutofillTypeFile
    {
        
        public IEnumerable<TypeFileFromUpload> GetDate()
        {
            List<TypeFileFromUpload> simpleTest2 = new();
            simpleTest2.AddRange(ReturnCollaritive());
            return simpleTest2;
        }
        public List<TypeFileFromUpload> ReturnCollaritive()
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(@"C:\Users\mpanylyk\Desktop\Test\CSVFile.csv")), true))
            {
                csvTable.Load(csvReader);
            }
            List<SearchParameters> searchParameters = new List<SearchParameters>();
            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                searchParameters.Add(new SearchParameters
                {
                    HexCode = csvTable.Rows[i][0].ToString(),
                    ISO = csvTable.Rows[i][1].ToString(),
                    Way = csvTable.Rows[i][2].ToString(),
                    Extension = csvTable.Rows[i][3].ToString(),
                    Description = csvTable.Rows[i][4].ToString()
                }
                );
            }
            List<TypeFileFromUpload> infoExtensions = new List<TypeFileFromUpload>();
            for (int i = 0; i < searchParameters.Count; i++)
            {
                switch (i)
                {
                    case 55:
                        infoExtensions.AddRange(Replace(searchParameters[i].HexCode.Replace("\n", ";"), searchParameters[i].Extension.Replace("\n", ";")));
                        break;
                    case 73:
                        infoExtensions.AddRange(Replace(searchParameters[i].HexCode.Replace("\n", ";"), searchParameters[i].Extension.Replace("\n", ";")));
                        break;
                    case 74:
                        infoExtensions.AddRange(Replace(searchParameters[i].HexCode.Replace("\n", ";"), searchParameters[i].Extension.Replace("\n", ";")));
                        break;
                    case 101:
                        infoExtensions.AddRange(Replace(searchParameters[i].HexCode.Replace("\n", ";"), searchParameters[i].Extension.Replace("\n", ";")));
                        break;
                    default:
                        infoExtensions.AddRange(Replace(searchParameters[i].HexCode.Replace("\n\n", ";").Replace("\r", ""), searchParameters[i].Extension.Replace("\n", ";")));
                        break;
                }
            }
            return infoExtensions;
        }
        public List<TypeFileFromUpload> Replace(string hex, string exten)
        {
            List<TypeFileFromUpload> infoExtensions = new List<TypeFileFromUpload>();
            var massHex = hex.Contains("\n") ? hex.Replace("\n", "").Split(';').ToList() : hex.Split(';').ToList();
            var massExten = exten.Split(';').ToList();
            for (int i = 0; i < massHex.Count; i++)
            {
                if (!massHex[i].StartsWith("("))
                {
                    for (int j = 0; j < massExten.Count; j++)
                    {
                        infoExtensions.Add(new TypeFileFromUpload { Id = Guid.NewGuid(), hexSignature = massHex[i].Replace(" ", ""), typeFile = massExten[j], extensionCategoryId = Guid.Parse("10E0F285-67FF-410A-9922-064CBF97B30D") });
                    }
                }
            }
            return infoExtensions;
        }
    }
    public class SearchParameters
    {
        public string HexCode { get; set; }
        public string ISO { get; set; }
        public string Way { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
    }
}
