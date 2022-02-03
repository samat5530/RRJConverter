using System;

namespace RRJConverter.Models
{
    public class ConvertingOperation
    {
        public int Id { get; set; }
        public string FromCurrency { get; set; }
        public decimal FromCurrencyValue { get; set; }
        public string ToCurrency { get; set; }
        public decimal ToCurrencyValue { get; set; }
        public DateTime DateOfConvertation { get; set; }

    }
}
