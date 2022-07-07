using Microsoft.Extensions.DependencyInjection;
using Notebook.Database.DatabaseService;


namespace Notebook.Database.Extensions
{
   public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {

            services.AddScoped<IDbService, DbService>();
            services.AddScoped<IAccount, AccountService>();
            services.AddDbContext<NoteContext>();
            return services;
        }
    }
}
