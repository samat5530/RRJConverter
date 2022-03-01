using Microsoft.Extensions.Logging;
using RRJConverter.Domain;
using RRJConverter.Domain.Models;
using RRJConverter.Integrations.Models;
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

        private readonly ILogger<JsonListOfCurrenciesService> _logger;

        private readonly HttpClient _httpClient;

        /// <summary>
        /// URL адрес стороннего API - Центробанк России. Даёт валюты в виде отношения их к рублю. 
        /// </summary>
        private readonly string _address = "https://www.cbr-xml-daily.ru/daily_json.js";

        public JsonListOfCurrenciesService(ILogger<JsonListOfCurrenciesService> logger, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<DomainCurrenciesPairModel>> GetListOfCurrenciesAsync()
        {
            try
            {
                //Десериализация в модель интеграции
                var responseString = await _httpClient.GetStringAsync(_address);
                _logger.LogInformation($"Response data: {responseString}");
                var currenciesFromApi = JsonSerializer.Deserialize<BankDataModel>(responseString,
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

        /// <summary>
        /// Добавляет в коллекцию пару RUB->X
        /// </summary>
        /// <param name="result"></param>
        /// <param name="currency"></param>
        private static void AddRubToCurrencyPair(List<DomainCurrenciesPairModel> result, KeyValuePair<string, CurrencyIntegrationModel> currency)
        {
            result.Add(new DomainCurrenciesPairModel
            {
                FirstCurrency = currency.Value.CharCode,
                SecondCurrency = "RUB",
                Rate = currency.Value.Value / currency.Value.Nominal
            });
        }

        /// <summary>
        /// Добавляет в коллекцию пару X->RUB
        /// </summary>
        /// <param name="result"></param>
        /// <param name="currency"></param>
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
