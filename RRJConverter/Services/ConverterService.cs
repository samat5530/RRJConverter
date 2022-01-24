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


            var temp = count / firstNominal;
            temp *= firstValue; //from x to RUB0

            var result = targetNominal / targetValue;
            result *= temp;   //from RUB0 to y         

            return result;
        }

    }
}
