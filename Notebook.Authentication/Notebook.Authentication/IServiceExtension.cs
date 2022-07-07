using Microsoft.Extensions.DependencyInjection;
using Notebook.Authentication.NewFolder;


namespace Notebook.Authentication
{
   public static class IServiceExtension
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            return services;
        }
    }
}
