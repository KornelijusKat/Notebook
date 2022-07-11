using Microsoft.AspNetCore.Http;


namespace NoteBook.ImageService.Validations
{
    public class ValidatingExtension
    {
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".png", ".jpg" })]
        public IFormFile Image { get; set; }
    }
}
