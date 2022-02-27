using RRJConverter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRJConverter.Domain
{
    public class CurrencyConverterService : ICurrencyConverter
    {
        private readonly IJsonApiCurrenciesService currencies;

        public CurrencyConverterService(IJsonApiCurrenciesService currenciesService)
        {
            currencies = currenciesService;
        }

        public async Task<decimal> ConvertAsync(string firstCurrency, decimal count, string secondCurrency)
        {
            var currencyCollection = (await currencies.GetListOfCurrenciesAsync()).ToList();

            if (currencyCollection == null)
            {
                throw new Exception("The Central bank's JSON-Api returns no data");
            }

            if (firstCurrency == null || secondCurrency == null || count < 0)
            {
                throw new Exception("One or two of the arguments are bad");
            }

            if (IsCurrencyExistInList(currencyCollection,firstCurrency) && IsCurrencyExistInList(currencyCollection,secondCurrency))
            {
                if (firstCurrency == secondCurrency)
                {
                    return count;
                }

                var pairItem = currencyCollection.Find(x => x.FirstCurrency == firstCurrency && x.SecondCurrency == secondCurrency);
                var result = Math.Round((count * pairItem.Rate),4);
                return result;
            }
            else
            {
                throw new Exception("The central bank does not provide data on your currencies. Check if the data is correct");
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
            bool checkingOperation = collectionOfPairs.Exists(pair => pair.SecondCurrency == currency);
            return checkingOperation;
        }
    }
}
