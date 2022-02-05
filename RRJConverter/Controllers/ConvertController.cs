using Microsoft.AspNetCore.Mvc;
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
        public JsonListOfValutesService ValutesService { get; set; }

        public ConverterService Converter { get; set; }
        public ApplicationContext applicationContext { get; set; }

        public ConvertController(JsonListOfValutesService jsonListOfValutesService, ConverterService converter, ApplicationContext context)
        {
            ValutesService = jsonListOfValutesService;
            Converter = converter;
            applicationContext = context;
        }

     
        public async Task<string> GetAsync(string valute, decimal count, string toValute)
        {
            var valuteList = ValutesService.GetListOfValutes(); 

            if(valuteList != null)
            {
                if (valute == null || toValute == null || count < 0)
                {
                    return new ErrorResponseModel().GetErrorResponse("One or two of the arguments in the request are bad.");
                }

                else
                {
                    if ((!valuteList.Valute.ContainsKey(valute) && valute != "RUB") || (!valuteList.Valute.ContainsKey(toValute) && toValute != "RUB")) // проверка корректности валют
                    {
                        return new ErrorResponseModel().GetErrorResponse("The central bank does not provide data on your currencies. Check if the data is correct");
                    }
                    else
                    {
                        if (valute == toValute) //когда валюты равны (для экономии вычисления).
                        {
                            await AddConvertationToDbAsync(valute, count, toValute, count);
                            return new ResponseModel().SendResponse(valute, toValute, count, count, DateTime.Now);
                        }
                        else
                        {

                            var result = Converter.Convert(valuteList, valute, count, toValute);
                            await AddConvertationToDbAsync(valute, count, toValute, result);
                            return new ResponseModel().SendResponse(valute, toValute, count, result, DateTime.Now);
                        }
                    }
                }
            }
            else
            {
                return new ErrorResponseModel().GetErrorResponse("Internal Error. Try later");
            }

        }

        private async Task AddConvertationToDbAsync(string fromCurrency, decimal value, string toCurrency, decimal toValue)
        {
            var dbUnit = new ConvertingOperation
            {
                FromCurrency = fromCurrency,
                FromCurrencyValue = value,
                ToCurrency = toCurrency,
                ToCurrencyValue = toValue,
                DateOfConvertation = DateTime.Now
            };
            applicationContext.Add(dbUnit);
            await applicationContext.SaveChangesAsync();
        }
    }
}
