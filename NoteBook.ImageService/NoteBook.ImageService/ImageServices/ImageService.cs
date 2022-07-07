using NoteBook.ImageService.NewFolder;
using NoteBook.ImageService.Validations;
using System.IO;


namespace NoteBook.ImageService
{
    public class ImageService : IServiceImage
    {
        public byte[] ConvertImageToBytes(ValidatingExtension imageRequest)
        {

            if (imageRequest.Image != null)
            {
                using var memoryStream = new MemoryStream();
                //var fileNameOfOriginal = imageRequest.Image.FileName;
                //var fileExtension = Path.GetExtension(fileNameOfOriginal);
                imageRequest.Image.CopyTo(memoryStream);
                var imageBytes = memoryStream.ToArray();
                return imageBytes;
            }
            return null;
        }
    }
}
