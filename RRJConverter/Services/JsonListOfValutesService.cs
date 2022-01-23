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
        HttpWebResponse _response;
        string _address = "https://www.cbr-xml-daily.ru/daily_json.js";


        //public string Response {private get; set; }

        public ListOfValutes GetListOfValutes()
        {
            if (TryGetDataFromURL(_address))
            {
                using (var stream = _response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        try
                        {
                            return JsonSerializer.Deserialize<ListOfValutes>(streamReader.ReadToEnd(),
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("\nThe following Exception was raised : {0}", e.Message);
                            return null;
                        }
                    }
                }
            }
            else
            {
                return null;
            }

        }

        public bool TryGetDataFromURL(string url)
        {
            try
            {
                _request = (HttpWebRequest)WebRequest.Create(url);
                _request.Method = "Get";
                _response = (HttpWebResponse)_request.GetResponse();
                if (_response.StatusCode == HttpStatusCode.OK)
                    //имеет сюда прикрутить логгирование
                    Console.WriteLine("\r\nResponse Status Code is OK and StatusDescription is: {0}",
                                         _response.StatusDescription);             
                return true;
            }
            catch (WebException e)
            {
                Console.WriteLine("\r\nWebException Raised. The following error occurred : {0}", e.Status);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nThe following Exception was raised : {0}", e.Message);
                return false;
            }
        }

    }
}
