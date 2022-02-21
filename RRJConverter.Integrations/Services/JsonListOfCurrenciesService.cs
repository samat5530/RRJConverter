using Microsoft.Extensions.Logging;
using RRJConverter.Domain;
using RRJConverter.Domain.Models;
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

                /*Приведение объектов модели интеграции к модели домена,
                Так как центробанк возвращает валюты в виде отношения к рублю
                логика поиска валют, конвертирования будет выстроена вокруг модели в виде пар отношений
                RUB->OTHERCURRENCY
                */
                var result = new List<DomainCurrenciesPairModel>();
                foreach (var currency in currenciesFromApi.Valute)
                {
                    result.Add(new DomainCurrenciesPairModel
                    {
                        FirstCurrency = "RUB",
                        SecondCurrency = currency.Value.CharCode,
                        Rate = currency.Value.Value / currency.Value.Nominal
                    });
                    result.Add(new DomainCurrenciesPairModel
                    {
                        FirstCurrency = "RUB",
                        SecondCurrency = "RUB",
                        Rate = 1
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

        /// <summary>
        /// Проверка наличия валюты в коллекции IEnumerable, состоящей из объектов DomainCurrenciesPairModel
        /// </summary>
        /// <param name="collection">Коллекция IEnumerable, состоящей из объектов DomainCurrenciesPairModel</param>
        /// <param name="currency">Строковый трёхбуквенный код валюты</param>
        /// <returns></returns>
        public bool IsCurrencyExistInList(IEnumerable<DomainCurrenciesPairModel> collection, string currency)
        {
            var collectionOfPairs = collection.ToList();
            bool chekingOperation = collectionOfPairs.Exists(pair => pair.SecondCurrency == currency);
            
            if (chekingOperation)
            {
                return true;
            }         
            return false;
        }

    }
}
