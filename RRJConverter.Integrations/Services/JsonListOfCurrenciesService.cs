using Microsoft.Extensions.Logging;
using RRJConverter.Domain;
using RRJConverter.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RRJConverter.Integrations.Services
{
    class JsonListOfCurrenciesService : IJsonApiCurrenciesService
    {
        /// <summary>
        /// Переменная хранящая логгер приходящий из конструктора
        /// </summary>
        private ILogger<JsonListOfCurrenciesService> _logger;

        //private HttpWebResponse _response;

        /// <summary>
        /// Переменная хранящая httpClient приходящий из конструктора
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// URL адрес стороннего API
        /// </summary>
        private readonly string _address = "https://www.cbr-xml-daily.ru/daily_json.js";

        public JsonListOfCurrenciesService(ILogger<JsonListOfCurrenciesService> logger, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Предоставляет сериализованный объект модели домена DomainCurrencyModel, преобразовывая объект модели интеграции, полученной из сторонней API
        /// </summary>
        public async Task<IEnumerable<DomainCurrencyModel>> GetListOfCurrenciesAsync()
        {
            try
            {
                //Десериализация в модель интеграции
                var responseString = await _httpClient.GetStringAsync(_address);
                _logger.LogInformation($"Response data: {responseString}");
                var currenciesFromApi = JsonSerializer.Deserialize<Currencies>(responseString,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                //Приведение объектов модели интеграции к модели домена
                var result = new List<DomainCurrencyModel>();
                foreach (var currency in currenciesFromApi.Valute)
                {
                    result.Add(new DomainCurrencyModel
                    {
                        FirstCurrency = "RUB",
                        SecondCurrency = currency.Value.CharCode,
                        Rate = currency.Value.Value / currency.Value.Nominal
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"The following Exception was raised : {ex.Message}");
                throw;
            }
        }
    }
}
