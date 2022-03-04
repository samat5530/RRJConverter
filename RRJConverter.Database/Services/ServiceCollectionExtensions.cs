using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using RRJConverter.Domain;

namespace RRJConverter.Database.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("LocalDBConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection));
            services.AddScoped<IRepository, ConvertingOperationsRepository>();
            return services;
        }
    }
}
