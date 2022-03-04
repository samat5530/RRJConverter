using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRJConverter.Domain;
using RRJConverter.Domain.Models;
using RRJConverter.Models;
using System;
using System.Threading.Tasks;

namespace RRJConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {

        private readonly ICurrencyConverter _valuteConverter;
        private readonly IRepository _repository;

        public ConvertController(ICurrencyConverter converter, IRepository repository)
        {
            _valuteConverter = converter;
            _repository = repository;
        }

        /// <summary>
        /// Возвращает ответ в виде JSON-объекта на запрос по адресу /api/Convert/?{query} <br />
        /// {query} должен состоять из трёх параметров данного метода
        /// </summary>
        /// <param name="valute">Представляет валюту, из которой требуется конвертация</param>
        /// <param name="count">Представляет количественное значение валюты, из которой требуется выполнить конвертацию</param>
        /// <param name="toValute">Представляет валюту, в которую требуется выполнить конвертирование</param>
        /// <returns>Возвращает JSON-объект клиенту с данными о конвертации</returns>
        public async Task<ResponseModel> Get(string valute, decimal count, string toValute)
        {
            var resultOfConverting = await _valuteConverter.ConvertAsync(valute, count, toValute);
            AddConvertationToDb(valute, count, toValute, resultOfConverting);
            var response = new ResponseModel
            {
                FromValute = valute,
                ToValute = toValute,
                Count = count,
                Result = resultOfConverting,
                ConvertationTime = DateTime.UtcNow
            };
            return response;
        }

        /// <summary>
        /// Непубличный метод, выполняет сохранение операции конвертации в базу данных
        /// </summary>
        /// <param name="fromCurrency">Представляет валюту, из которой требуется конвертация</param>
        /// <param name="value">Представляет количественное значение валюты, из которой требуется выполнить конвертацию</param>
        /// <param name="toCurrency">Представляет валюту, в которую требуется выполнить конвертирование</param>
        /// <param name="toValue">Представляет итоговое найденное количественное значение требуемой валюты</param>
        private void AddConvertationToDb(string fromCurrency, decimal value, string toCurrency, decimal toValue)
        {
            var operation = new DomainConvertingOperationModel
            {
                FromCurrency = fromCurrency,
                FromCurrencyValue = value,
                ToCurrency = toCurrency,
                ToCurrencyValue = toValue,
                CreatedOn = DateTime.UtcNow
            };

            _repository.Create(operation);
            _repository.Save();
        }
    }
}
