using RRJConverter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RRJConverter.Domain
{

    /// <summary>
    /// Сервис, выполяющий конвертацию валют
    /// </summary>
    public interface ICurrencyConverter
    {
        /// <summary>
        /// Выполняет конвертацию заданного количества валюты "A" в валюту "B"
        /// </summary>
        /// <param name="firstCurrency">Валюта "A"</param>
        /// <param name="count">Количество валюты "A"</param>
        /// <param name="secondCurrency">Валюта "B"</param>
        /// <returns>Результат конвертации в виде численного количества валюты "B"</returns>
        public Task<decimal> ConvertAsync(string firstCurrency, decimal count, string secondCurrency);

        /// <summary>
        /// Проверка наличия валюты в коллекции
        /// </summary>
        /// <param name="collection">Коллекция валют, в которой ведется поиск</param>
        /// <param name="currency">Искомая валюта</param>
        /// <returns>true, если валюта найдена, иначе false</returns>
        public bool IsCurrencyExistInList(IEnumerable<DomainCurrenciesPairModel> collection, string currency);
    }
}
