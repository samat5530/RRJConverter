using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RRJConverter.Domain
{
    interface ICurrencyRatesService
    {
        public Task<DomainCurrencyModel> GetListOfValutesAsync();    
    }
}
