//Требуется прикрутить сюда логгер
//вместо ведения лога исключений
//на время прикрутил печать в консоль


using System;
using System.Text.Json;
using System.Net;
using RRJConverter.Models;

using System.IO;

namespace RRJConverter.Services
{
    public class JsonListOfValutesService
    {
        HttpWebRequest _request;
        HttpWebResponse _response;
        string _address = "https://www.cbr-xml-daily.ru/daily_json.js";


        public ListOfValutes GetListOfValutes()
        {
            if (TryGetData())
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

        public bool TryGetData()
        {
            try
            {
                _request = (HttpWebRequest)WebRequest.Create(_address);
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
