using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RRJConverter.Domain
{
    public interface ICurrencyConverter
    {
        public Task<decimal> ConvertAsync(string firstCurrency, decimal count, string secondCurrency);
    }
}
