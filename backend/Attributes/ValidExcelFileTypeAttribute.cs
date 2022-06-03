using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using backend.Models.RequestModels;
using backend.References;

namespace backend.Attributes
{
    public class ValidExcelFileTypeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (CreateUserFromFileRequestModel) validationContext.ObjectInstance;
            var file = model.UserFile;

            if (string.IsNullOrEmpty(model.FileType) || !FileTypes.FileTypeToContentTypeMap.ContainsKey(model.FileType))
            {
                return ValidationResult.Success;
            }
            var validContentType = FileTypes.FileTypeToContentTypeMap[model.FileType];

            if (file.ContentType != validContentType) return new ValidationResult("Invalid file");
            
            return ValidationResult.Success;
        }
    }
}