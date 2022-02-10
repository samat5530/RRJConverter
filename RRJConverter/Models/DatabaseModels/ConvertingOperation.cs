using System;

namespace RRJConverter.Models
{
    /// <summary>
    /// Представляет из себя объектную модель результата операции конвертации.
    /// </summary>
    public class ConvertingOperation
    {
        /// <summary>
        /// Уникальный идентификатор. Требуется для уникализации данных объектов в базе данных
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Представляет валюту из которой проводилась конвертация
        /// </summary>
        public string FromCurrency { get; set; }
        /// <summary>
        /// Представляет количественное значение конвертируемой валюты
        /// </summary>
        public decimal FromCurrencyValue { get; set; }
        /// <summary>
        /// Представляет валюту в которую проводилась конвертация
        /// </summary>
        public string ToCurrency { get; set; }
        /// <summary>
        /// Представляет требуемое количественное значение валюты в которую проводилась конвертация
        /// </summary>
        public decimal ToCurrencyValue { get; set; }
        /// <summary>
        /// Представляет дату заведения операции в базу данных
        /// </summary>
        public DateTime CreatedOn { get; set; }

    }
}
