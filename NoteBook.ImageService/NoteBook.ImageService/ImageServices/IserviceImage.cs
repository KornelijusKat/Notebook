using NoteBook.ImageService.Validations;


namespace NoteBook.ImageService.NewFolder
{
   public interface IServiceImage
    {
        byte[] ConvertImageToBytes(ValidatingExtension imageRequest);
    }
}
