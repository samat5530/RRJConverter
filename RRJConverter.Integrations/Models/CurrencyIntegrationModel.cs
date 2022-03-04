using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RRJConverter.Integrations.Models
{
    /// <summary>
    /// Валютная модель интеграции
    /// </summary>
    public class CurrencyIntegrationModel
    {
        /// <summary>
        /// ID валюты. (сторонний API)
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Номерной код валюты (сторонний API)
        /// </summary>
        public string NumCode { get; set; }

        /// <summary>
        /// Трехзначный символьный код валюты, используемый банками. (сторонний API)
        /// </summary>
        public string CharCode { get; set; }

        /// <summary>
        /// Номинал валюты по отношению к рублю(сторонний API)
        /// </summary>
        public decimal Nominal { get; set; }

        /// <summary>
        /// Полное имя валюты на русском языке(сторонний API)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Значение валюты по отношению к рублю(сторонний API)
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Предыдущее значение (не используется)(сторонний API)
        /// </summary>
        public decimal Previous { get; set; }

    }
}
