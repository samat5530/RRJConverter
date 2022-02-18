using System;
using System.Collections.Generic;
using System.Text;

namespace RRJConverter.Domain
{
    interface ICurrencyConverter
    {
        public decimal Convert(decimal count, decimal rate);
    }
}
