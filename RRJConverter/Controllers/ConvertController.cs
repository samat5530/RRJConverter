using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRJConverter.Models;
using RRJConverter.Models.DatabaseModels;
using RRJConverter.Services;
using System;
using System.Threading.Tasks;

namespace RRJConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        
        /// <summary>
        /// Хранит методы конвертации валют, определённые в сервисе ConverterService
        /// </summary>
        private readonly ConverterService _valuteConverter;

        /// <summary>
        ///  Хранит контекст базы данных, предоставленной EF в сервисе ApplicationContext
        /// </summary>
        public ApplicationContext applicationContext { get; set; }

        public ConvertController(ConverterService converter, ApplicationContext context)
        {
            _valuteConverter = converter;
            applicationContext = context;
        }

        /// <summary>
        /// Возвращает запрос по адресу /api/Convert/?{query} <br />
        /// {query} должен состоять из трёх параметров данного метода
        /// </summary>
        /// <param name="valute">Представляет валюту, из которой требуется конвертация</param>
        /// <param name="count">Представляет количественное значение валюты, из которой требуется выполнить конвертацию</param>
        /// <param name="toValute">Представляет валюту, в которую требуется выполнить конвертирование</param>
        /// <returns>Возвращает JSON-объект клиенту с данными о конвертации</returns>
        public async Task<string> Get(string valute, decimal count, string toValute)
        {
            var result = _valuteConverter.Convert(valute, count, toValute);
            await AddConvertationToDbAsync(valute, count, toValute, result);
            return new ResponseModel().GetResponse(valute, toValute, count, result, DateTime.Now);
        }

        /// <summary>
        /// Непубличный метод, выполняет сохранение операции конвертации в базу данных
        /// </summary>
        /// <param name="fromCurrency">Представляет валюту, из которой требуется конвертация</param>
        /// <param name="value">Представляет количественное значение валюты, из которой требуется выполнить конвертацию</param>
        /// <param name="toCurrency">Представляет валюту, в которую требуется выполнить конвертирование</param>
        /// <param name="toValue">Представляет итоговое найденное количественное значение требуемой валюты</param>
        /// <returns>Выполняет сохранение изменений в базу данных</returns>
        private async Task AddConvertationToDbAsync(string fromCurrency, decimal value, string toCurrency, decimal toValue)
        {
            var convertingOperation = new ConvertingOperation
            {
                FromCurrency = fromCurrency,
                FromCurrencyValue = value,
                ToCurrency = toCurrency,
                ToCurrencyValue = toValue,
                CreatedOn = DateTime.UtcNow
            };
            await applicationContext.AddAsync(convertingOperation);
            await applicationContext.SaveChangesAsync();
        }
    }
}
