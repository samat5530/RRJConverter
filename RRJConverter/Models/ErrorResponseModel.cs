using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RRJConverter.Models
{
    public class ErrorResponseModel
    {
        public string Error { get; set; }

        public string GetErrorRespose()
        {
            //var obj = new ErrorResponseModel();

            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
