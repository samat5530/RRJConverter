using Microsoft.Extensions.Logging;
using RRJConverter.Domain;
using RRJConverter.Domain.Models;
using RRJConverter.Integrations.Models;
using RRJConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// URL адрес стороннего API - Центробанк России. Даёт валюты в виде отношения их к рублю. 
        /// 
        /// </summary>
        private readonly string _address = "https://www.cbr-xml-daily.ru/daily_json.js";

        public JsonListOfCurrenciesService(ILogger<JsonListOfCurrenciesService> logger, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Предоставляет сериализованный объект модели домена DomainCurrenciesPairModel, преобразовывая объект модели интеграции, полученной из сторонней API
        /// </summary>
        public async Task<IEnumerable<DomainCurrenciesPairModel>> GetListOfCurrenciesAsync()
        {
            try
            {
                //Десериализация в модель интеграции
                var responseString = await _httpClient.GetStringAsync(_address);
                _logger.LogInformation($"Response data: {responseString}");
                var currenciesFromApi = JsonSerializer.Deserialize<Currencies>(responseString,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                //Приведение объектов модели интеграции к модели домена, приведение всех пар в виде модели домена
                var result = new List<DomainCurrenciesPairModel>();
                foreach (var currency in currenciesFromApi.Valute)
                {   
                    // X-X пара              
                    foreach (var otherCurrency in currenciesFromApi.Valute)
                    {
                        if (currency.Value.CharCode == otherCurrency.Value.CharCode)
                        {
                            result.Add(new DomainCurrenciesPairModel
                            {
                                FirstCurrency = currency.Value.CharCode,
                                SecondCurrency = otherCurrency.Value.CharCode,
                                Rate = 1,
                            });
                        }
                        else
                        {
                            result.Add(new DomainCurrenciesPairModel
                            {
                                FirstCurrency = currency.Value.CharCode,
                                SecondCurrency = otherCurrency.Value.CharCode,
                                Rate = (currency.Value.Value / currency.Value.Nominal) * (otherCurrency.Value.Nominal / otherCurrency.Value.Value)
                            });
                        }
                    }

                    //X-RUB пара
                    AddCurrencyToRubPair(result, currency);
                    //RUB-X пара
                    AddRubToCurrencyPair(result, currency);
                }

                //RUB-RUB пара
                result.Add(new DomainCurrenciesPairModel
                {
                    FirstCurrency = "RUB",
                    SecondCurrency = "RUB",
                    Rate = 1
                });

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"The following Exception was raised : {ex.Message}");
                throw;
            }
        }

        private static void AddRubToCurrencyPair(List<DomainCurrenciesPairModel> result, KeyValuePair<string, CurrencyIntegrationModel> currency)
        {
            result.Add(new DomainCurrenciesPairModel
            {
                FirstCurrency = currency.Value.CharCode,
                SecondCurrency = "RUB",
                Rate = currency.Value.Value / currency.Value.Nominal
            });
        }

        private static void AddCurrencyToRubPair(List<DomainCurrenciesPairModel> result, KeyValuePair<string, CurrencyIntegrationModel> currency)
        {
            result.Add(new DomainCurrenciesPairModel
            {
                FirstCurrency = "RUB",
                SecondCurrency = currency.Value.CharCode,
                Rate = currency.Value.Nominal / currency.Value.Value
            });
        }

    }
}
