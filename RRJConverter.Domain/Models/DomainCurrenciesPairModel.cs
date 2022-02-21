using System;
using System.Collections.Generic;
using System.Text;

namespace RRJConverter.Domain.Models
{
    public class DomainCurrenciesPairModel
    {
        /// <summary>
        /// Первая валюта
        /// </summary>
        public string FirstCurrency { get; set; }
        /// <summary>
        /// Вторая валюта
        /// </summary>
        public string SecondCurrency { get; set; }
        /// <summary>
        /// Отношение первой валюты ко второй
        /// </summary>
        public decimal Rate { get; set; }
    }
}
