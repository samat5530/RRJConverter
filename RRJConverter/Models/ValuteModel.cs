using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RRJConverter.Models
{
    public class ValuteModel
    {
        public string ID { get; set; }

        public string NumCode { get; set; }

        public string CharCode { get; set; }

        public decimal Nominal { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public decimal Previous { get; set; }

    }
}
