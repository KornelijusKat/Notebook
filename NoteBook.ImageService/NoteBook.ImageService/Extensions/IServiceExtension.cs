using Microsoft.Extensions.DependencyInjection;
using NoteBook.ImageService.NewFolder;


namespace NoteBook.ImageService.Extensions
{
    public static class IServiceExtension
    {
        public static IServiceCollection AddImageService(this IServiceCollection services)
        {
            services.AddScoped<IServiceImage, ImageService>();
            return services;
        }
    }
}
