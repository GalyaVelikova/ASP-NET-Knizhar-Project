namespace Knizhar.Attributes
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    public class AllowedImageExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] allowedExtensions;
        public AllowedImageExtensionsAttribute(string[] extensions)
        {
            this.allowedExtensions = extensions;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!allowedExtensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"This photo extension is not allowed!";
        }
    }
}
