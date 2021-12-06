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
        public string FromValute { get; set; }

        public decimal Count { get; set; }
       
        public string  ToValute { get; set; }

        public decimal Result { get; set; }

        public DateTime ConvertationTime { get; set; }



        public string GetResponce(string fromValute, string toValute, decimal count, decimal result, DateTime time)
        {
            var obj = new ResponseModel {
                FromValute = fromValute,
                ToValute = toValute,
                Count = count,
                Result = result,
                ConvertationTime = time,                          
            };

            return JsonSerializer.Serialize<ResponseModel>(obj, new JsonSerializerOptions() { 
                PropertyNameCaseInsensitive = true, });
        }

    }
}
