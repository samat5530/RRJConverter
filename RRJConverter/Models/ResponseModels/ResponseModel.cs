using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RRJConverter.Models
{
    public class ResponseModel
    {
        /// <summary>
        /// Валюта из которой проводилась конвертация
        /// </summary>
        public string FromValute { get; set; }

        /// <summary>
        /// Количественное значение конвертируемой валюты
        /// </summary>
        public decimal Count { get; set; }
        /// <summary>
        /// Валюта в которую проводилась конвертация
        /// </summary>
        public string  ToValute { get; set; }

        /// <summary>
        /// Требуемое количественное значение валюты в которую проводилась конвертация
        /// </summary>
        public decimal Result { get; set; }

        /// <summary>
        /// Дата проведения операции 
        /// </summary>
        public DateTime ConvertationTime { get; set; }

        public string GetResponse(string fromValute, string toValute, decimal count, decimal result, DateTime time)
        {
            var response = new ResponseModel {
                FromValute = fromValute,
                ToValute = toValute,
                Count = count,
                Result = result,
                ConvertationTime = time,                          
            };

            return JsonSerializer.Serialize<ResponseModel>(response, new JsonSerializerOptions() { 
                PropertyNameCaseInsensitive = true});
        }

    }
}
