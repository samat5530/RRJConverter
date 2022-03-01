using RRJConverter.Integrations.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace RRJConverter.Models
{

    /// <summary>
    /// Валютная модель интеграции 
    /// </summary>
    public class BankDataModel
    {
        /// <summary>
        /// Дата текущего курса (сторонний API)
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Предыдущая дата курса (не требуется)(сторонний API)
        /// </summary>
        public DateTime PreviousDate { get; set; }

        /// <summary>
        /// Ссылка на архивные данные (не требуется)(сторонний API)
        /// </summary>
        public string PreviousURL { get; set; }

        /// <summary>
        /// Метка времени (не требуется)(сторонний API)
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Объект валют. Основное свойство модели. (сторонний API)
        /// </summary>
        public Dictionary<string, CurrencyIntegrationModel> Valute { set; get; }

        /// <summary>
        /// Переопределённая версия ToString. Сериализует объект модели в JSON. 
        /// </summary>
        /// <returns>Возвращает строку в формате JSON.</returns>
        public override string ToString() => JsonSerializer.Serialize(this);

    }
}
