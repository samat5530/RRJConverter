using RRJConverter.Models;
using System;

namespace RRJConverter.Services
{
    public class ConverterService
    {
        /// <summary>
        /// Переменная, хранящая объект модели валют, предоставленная сторонним API
        /// </summary>
        private readonly JsonListOfValutesService _jsonListOfValutesService;

        public ConverterService(JsonListOfValutesService jsonListOfValutesService)
        {
            _jsonListOfValutesService = jsonListOfValutesService;
        }

        //ListOfValutes data 
        public decimal Convert(string valute, decimal count, string toValute)
        {      
            var valutes = _jsonListOfValutesService.GetListOfValutesAsync().Result;
            
            if (valutes == null) 
            {
                throw new Exception("The Central bank JSON-Api returns no data");
                //return new ErrorResponseModel().GetErrorResponse("Internal Error. Try later");
            }

            if (valute == null || toValute == null || count < 0)
            {
                throw new Exception("One or two of the arguments are bad");
                //return new ErrorResponseModel().GetErrorResponse("One or two of the arguments in the request are bad.");
            }


            // проверка корректности валют
            if ((!valutes.Valute.ContainsKey(valute) && valute != "RUB")
                || (!valutes.Valute.ContainsKey(toValute) && toValute != "RUB")) 
            {
                throw new Exception("The central bank does not provide data on your currencies. Check if the data is correct");
                //return new ErrorResponseModel()
                //   .GetErrorResponse("The central bank does not provide data on your currencies. Check if the data is correct");
            }

            //когда валюты равны 
            if (valute == toValute) 
            {
                return count;
            }

            if (toValute == "RUB") // если toValute == RUB
            {
                return toValuteIsRUB(valutes, valute, count);
            }
            if (valute == "RUB") //еслиг valute == RUB
            {
                return valuteIsRUB(valutes, count, toValute);
            }
            // something -> somethin2 

            var item = valutes.Valute[toValute];
            var temp = count / valutes.Valute[valute].Nominal;
            temp *= valutes.Valute[valute].Value;  //from x to RUB
            var result = item.Nominal / item.Value;
            result *= temp;   //from RUB to y         
            return Math.Round(result, 4);
        }

        /// <summary>
        /// Конвертация из рубля в другую валюту
        /// </summary>
        /// <param name="data"></param>
        /// <param name="count"></param>
        /// <param name="toValute"></param>
        /// <returns></returns>
        private decimal valuteIsRUB(ListOfValutes data, decimal count, string toValute)
        {
            var item = data.Valute[toValute];
            var result = item.Nominal / item.Value;
            result *= count;
            return Math.Round(result, 4);
        }
        /// <summary>
        /// Конвертация из другой валюты в рубль
        /// </summary>
        /// <param name="data"></param>
        /// <param name="valute"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        
        private decimal toValuteIsRUB(ListOfValutes data, string valute, decimal count)
        {
            var item = data.Valute[valute];
            var result = count / item.Nominal;
            result *= item.Value;
            return Math.Round(result, 4);
        }
    }
}
