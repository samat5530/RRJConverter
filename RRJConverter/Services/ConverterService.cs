using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RRJConverter.Models;

namespace RRJConverter.Services
{
    public class ConverterService
    {

        public decimal Convert(decimal firstValue, decimal firstNominal, decimal targetValue, decimal targetNominal, decimal count)
        {
            ////var value = firstValue / firstNominal;
            ////value *= targetNominal;
            //value /= targetValue;
            //return value * count;

            var temp = count / firstNominal;
            temp *= firstValue; //from x to RUB0

            var result = targetNominal / targetValue;
            result *= temp;   //from RUB0 to y         

            return result;
        }



        //The first realizing of this service.

        //public (string, decimal, string) takenValueOfValute { get; set; }
        ////public Dictionary<string,decimal> givenValueOfValute { get; private set; }
        //public JsonListOfValutesService ListOfValutesService { get; set; }
        ////public ConvertValutesModel operation { get; set; }

        //public ConverterService(JsonListOfValutesService jsonListOfValutesService)
        //{
        //    ListOfValutesService = jsonListOfValutesService;
        //}

        //public (string, decimal, string) GetValueOfValute()
        //{
        //    if (takenValueOfValute.Item1 != String.Empty)
        //    {
        //        var list = ListOfValutesService.GetListOfValutes();

        //    }


        //    else return (String.Empty, 0);
        //}

    }
}
