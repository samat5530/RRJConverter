using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RRJConverter.Models
{
    public class ListOfValutes
    {
        
        public DateTime Date { get; set; }

        public DateTime PreviousDate {get; set;}

        public string PreviousURL { get; set; }

        public DateTime Timestamp { get; set; }

        public Dictionary<string, ValuteObj> Valute { set; get; }

        public override string ToString() => JsonSerializer.Serialize(this);
        
    }
}
