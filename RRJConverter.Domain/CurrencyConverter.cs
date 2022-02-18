using System;
using System.Collections.Generic;
using System.Text;

namespace RRJConverter.Domain
{

    class CurrencyConverter : ICurrencyConverter
    {
        public decimal Convert(decimal count, decimal rate)
        {
            var result = count * rate;
            return result;
        }
    }
}
