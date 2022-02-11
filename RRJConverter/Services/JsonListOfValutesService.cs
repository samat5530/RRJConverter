using System;
using System.Text.Json;
using System.Net;
using RRJConverter.Models;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace RRJConverter.Services
{
    public class JsonListOfValutesService
    {
        /// <summary>
        /// Переменная хранящая логгер приходящий из конструктора
        /// </summary>
        private ILogger<JsonListOfValutesService> _logger;

        //private HttpWebResponse _response;

        /// <summary>
        /// Переменная хранящая httpClient приходящий из конструктора
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// URL адрес стороннего API
        /// </summary>
        private readonly string _address = "https://www.cbr-xml-daily.ru/daily_json.js";
  

        public JsonListOfValutesService(ILogger<JsonListOfValutesService> logger, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Предоставляет сериализованный объект класса ListOfValutes из сторонней API
        /// </summary>
        public async Task<ListOfValutes> GetListOfValutesAsync()
        {
            try
            {
                var responseString = await _httpClient.GetStringAsync(_address);
                _logger.LogInformation($"Response data: {responseString}");
                return JsonSerializer.Deserialize<ListOfValutes>(responseString,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"The following Exception was raised : {ex.Message}");
                //Console.WriteLine("\nThe following Exception was raised : {0}", ex.Message);
                throw;
            }
        }

        /*public ListOfValutes GetListOfValutes()
        {
            if (TryGetData())
            {
                using (var stream = _response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        try
                        {
                            string responseString = streamReader.ReadToEnd();
                            _logger.LogInformation($"Response data: {responseString}");
                            return JsonSerializer.Deserialize<ListOfValutes>(responseString,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("\nThe following Exception was raised : {0}", e.Message);
                            throw;
                            //return null;
                        }
                    }
                }
            }
            throw new Exception("Data not received");
            //return null
        }*/

        /*
        public bool TryGetData()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(_address);
                request.Method = "Get";
                _response = (HttpWebResponse)request.GetResponse();
                _logger.LogInformation($"\r\nResponse Status Code is OK and StatusDescription is {_response.StatusDescription}");
                return true;
            }
            catch (WebException e)
            {
                _logger.LogError($"\r\nWebException Raised. The following error occurred : {e.Status}");
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError($"\nThe following Exception was raised : {e.Message}");
                return false;
            }
        }*/
    }
}
