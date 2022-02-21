using RRJConverter.Models;
using System;
using RRJConverter.Domain;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using RRJConverter.Domain.Models;

namespace RRJConverter.Services
{
    public class ConverterService : ICurrencyConverter
    {
        /// <summary>
        /// Переменная, хранящая коллекцию IEnumerable объектов доменной модели валют, предоставленная сторонним API
        /// </summary>
        private readonly IJsonApiCurrenciesService _currencies;

        public ConverterService(IJsonApiCurrenciesService jsonApiCurrenciesService)
        {
            _currencies = jsonApiCurrenciesService;
        }

        //ListOfValutes data 
        public async Task<decimal> ConvertAsync(string fromCurrency, decimal count, string toCurrency)
        {
            var currencyCollection = (await _currencies.GetListOfCurrenciesAsync()).ToList();
            
            if (currencyCollection == null) 
            {
                throw new Exception("The Central bank's JSON-Api returns no data");
            }

            if (fromCurrency == null || toCurrency == null || count < 0)
            {
                throw new Exception("One or two of the arguments are bad");
            }

            // проверка корректности валют

            if (!_currencies.IsCurrencyExistInList(currencyCollection, fromCurrency)
                || !_currencies.IsCurrencyExistInList(currencyCollection, fromCurrency))
            {
                throw new Exception("The central bank does not provide data on your currencies. Check if the data is correct"); 
            }

            //когда валюты равны 
            if (fromCurrency == toCurrency) 
            {
                return count;
            }

            if (fromCurrency == "RUB") //конвертация из рубля
            {
                return convertingFromRUB(toCurrency, count, currencyCollection);
            }

            if (toCurrency == "RUB") // если toValute == RUB
            {
                return convertingToRUB(fromCurrency, count, currencyCollection);
            }

            // something -> somethin2 

            var fromCurrencyToRUBValue = convertingToRUB(fromCurrency, count, currencyCollection);
            var result = convertingFromRUB(toCurrency, fromCurrencyToRUBValue, currencyCollection);
       
            return Math.Round(result, 4);
        }

        private static decimal convertingToRUB(string fromCurrency, decimal count, List<DomainCurrenciesPairModel> currencyCollection)
        {
            
            var pair = currencyCollection.Find(x => x.SecondCurrency == fromCurrency);
            return Math.Round(pair.Rate * count, 4);
        }

        private static decimal convertingFromRUB(string toCurrency, decimal count,  List<DomainCurrenciesPairModel> currencyCollection)
        {
            var pair = currencyCollection.Find(x => x.SecondCurrency == toCurrency);
            return Math.Round((1 / pair.Rate) * count, 4);
        }
    }
}
