using RRJConverter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RRJConverter.Models
{
    public class ConvertValutesModel
    {
        public int Id { get; set; }
        public (string, decimal) fromCurrency { get; set; }
        public (string, decimal) toCurrency { get; set; }

        public DateTime dateOfConvertation { get; set; }



        //public JsonListOfValutesService ListOfValutesService { get; private set; }
        //public ConvertValutesModel(JsonListOfValutesService listOfValutesService)
        //{
        //    ListOfValutesService = listOfValutesService;
        //}

        

    }
}
