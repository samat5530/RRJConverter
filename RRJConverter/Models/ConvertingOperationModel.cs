using System;

namespace RRJConverter.Models
{
    public class ConvertingOperationModel
    {
        public int Id { get; set; }
        public string fromCurrency { get; set; }
        public int fromCurrencyValue { get; set; }
        public string toCurrency { get; set; }
        public int toCurrencyValue { get; set; }
        public DateTime dateOfConvertation { get; set; }

    }
}
