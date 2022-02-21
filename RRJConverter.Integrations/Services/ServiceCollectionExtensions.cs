using Microsoft.Extensions.DependencyInjection;
using RRJConverter.Domain;

namespace RRJConverter.Integrations.Services
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Расширяет сервисы Web-приложения интеграционными сервисами
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIntegrationServices(
            this IServiceCollection services)
        {
            services.AddTransient<IJsonApiCurrenciesService, JsonListOfCurrenciesService>();
            services.AddHttpClient<JsonListOfCurrenciesService>();
            return services;
        }
    }
}