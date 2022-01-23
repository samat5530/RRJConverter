using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRJConverter.Models;
using RRJConverter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RRJConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        public JsonListOfValutesService ValutesService { get; set; }

        public ConverterService Converter { get; set; }

        public ListOfValutes valuteList { get; private set; }

        public ConvertController(JsonListOfValutesService jsonListOfValutesService, ConverterService converter)
        {
            ValutesService = jsonListOfValutesService;
            Converter = converter;
        }

     

        //[HttpGet("{toValute}/{count}/{valute}")]
        public string Get([FromQuery] string valute, [FromQuery] decimal count, [FromQuery] string toValute)
        {
            var errorResponse = new ErrorResponseModel();
            var myResponse = new ResponseModel();
            var valuteList = ValutesService.GetListOfValutes();

            if(valuteList != null)
            {
                if (valute == null || toValute == null || count < 0)
                {
                    errorResponse.Error = "One or two of the arguments in the request are bad.";
                    return errorResponse.GetErrorRespose();
                }

                else
                {
                    if ((!valuteList.Valute.ContainsKey(valute) && valute != "RUB") || (!valuteList.Valute.ContainsKey(toValute) && toValute != "RUB")) // проверка корректности валют
                    {
                        errorResponse.Error = $"The central bank does not provide data on your currencies. Check if the data is correct";
                        return errorResponse.GetErrorRespose();
                    }
                    else
                    {
                        if (valute == toValute) //когда валюты равны
                        {
                            return myResponse.SendResponse(valute, toValute, count, count, DateTime.Now);
                        }
                        else
                        {
                            if (toValute == "RUB") // если toValute == RUB
                            {
                                var item = valuteList.Valute[valute];
                                var result = count / item.Nominal;
                                result *= item.Value;
                                return myResponse.SendResponse(valute, toValute, count, result, DateTime.Now);
                            }
                            else if (valute == "RUB") //еслиг valute == RUB
                            {
                                var item = valuteList.Valute[toValute];
                                var result = item.Nominal / item.Value;
                                result *= count;
                                return myResponse.SendResponse(valute, toValute, count, result, DateTime.Now);
                            }
                            else // something -> somethin2 
                            {
                                var item = valuteList.Valute[toValute];
                                var result = Converter.Convert(valuteList.Valute[valute].Value, valuteList.Valute[valute].Nominal, item.Value, item.Nominal, count);
                                return myResponse.SendResponse(valute, toValute, count, result, DateTime.Now);
                            }
                        }
                    }
                }
            }
            else
            {
                errorResponse.Error = $"Internal Error. Try later";
                return errorResponse.GetErrorRespose();
            }

        }
    }
}
