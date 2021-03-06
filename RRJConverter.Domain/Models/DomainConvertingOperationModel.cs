using System;
using System.Collections.Generic;
using System.Text;

namespace RRJConverter.Domain.Models
{

    /// <summary>
    /// Модель операции конвертации
    /// </summary>
    public class DomainConvertingOperationModel
    {
        /// <summary>
        /// Уникальный идентификатор. Требуется для уникализации данных объектов в базе данных
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Валюта из которой проводилась конвертация
        /// </summary>
        public string FromCurrency { get; set; }

        /// <summary>
        /// Количественное значение конвертируемой валюты
        /// </summary>
        public decimal FromCurrencyValue { get; set; }

        /// <summary>
        /// Валюта в которую проводилась конвертация
        /// </summary>
        public string ToCurrency { get; set; }

        /// <summary>
        /// Требуемое количественное значение валюты в которую проводилась конвертация
        /// </summary>
        public decimal ToCurrencyValue { get; set; }

        /// <summary>
        /// Дата заведения операции в базу данных
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
