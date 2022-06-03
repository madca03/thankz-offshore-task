using System.ComponentModel.DataAnnotations;
using backend.Attributes;
using backend.References;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace backend.Models.RequestModels
{
    public class CreateUserFromFileRequestModel
    {
        [Required]
        [ValidExcelFileType]
        public IFormFile UserFile { get; set; }

        [Required]
        [StringRange(FileTypes.CSV, FileTypes.XLSX)]
        public string FileType { get; set; }
    }
}