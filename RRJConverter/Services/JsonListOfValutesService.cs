using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;
using RRJConverter.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace RRJConverter.Services
{
    public class JsonListOfValutesService
    {
        HttpWebRequest _request;
        string _address = "https://www.cbr-xml-daily.ru/daily_json.js";

        //public string Response {private get; set; }

        public ListOfValutes GetListOfValutes()
        {
            _request = (HttpWebRequest)WebRequest.Create(_address);
            _request.Method = "Get";

            HttpWebResponse response = (HttpWebResponse)_request.GetResponse();

            using (var stream = response.GetResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    return JsonSerializer.Deserialize<ListOfValutes>(streamReader.ReadToEnd(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
        } 
    }
}
