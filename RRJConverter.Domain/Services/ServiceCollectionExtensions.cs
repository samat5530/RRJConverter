using Microsoft.Extensions.DependencyInjection;
using RRJConverter.Domain;

namespace RRJConverter.Domain.Services
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Расширяет сервисы Web-приложения доменными сервисами
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainServices(
            this IServiceCollection services)
        {
            services.AddTransient<ICurrencyConverter, CurrencyConverterService>();
            return services;
        }
    }
}