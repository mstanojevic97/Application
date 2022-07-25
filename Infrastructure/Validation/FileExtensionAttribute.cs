using System.ComponentModel.DataAnnotations;

namespace Application.Infrastructure.Validation
{
    public class FileExtensionAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                string[] extensions = { "jpg", "png" };
                bool resault = extensions.Any(x => extension.EndsWith(x));
                if (!resault)
                {
                    return new ValidationResult("Allowed extensions are jpg and png!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
