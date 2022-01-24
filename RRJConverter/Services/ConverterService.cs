using RRJConverter.Models;
using System;

namespace RRJConverter.Services
{
    public class ConverterService
    {

        public decimal Convert(decimal firstValue, decimal firstNominal, decimal targetValue, decimal targetNominal, decimal count)
        {


            var temp = count / firstNominal;
            temp *= firstValue; //from x to RUB0

            var result = targetNominal / targetValue;
            result *= temp;   //from RUB0 to y         

            return result;
        }

        public decimal Convert(ListOfValutes data, string valute, decimal count, string toValute)
        {

            if (toValute == "RUB") // если toValute == RUB
            {
                var item = data.Valute[valute]; 
                var result = count / item.Nominal;
                result *= item.Value;
                return result;
            }
            else if (valute == "RUB") //еслиг valute == RUB
            {
                var item = data.Valute[toValute];
                var result = item.Nominal / item.Value;
                result *= count;
                return result;
            }
            else // something -> somethin2 
            {
                var item = data.Valute[toValute];

                var temp = count / data.Valute[valute].Nominal;
                temp *= data.Valute[valute].Value;  //from x to RUB0

                var result = item.Nominal / item.Value;
                result *= temp;   //from RUB0 to y         

                return result;
            }
        }



    }
}
