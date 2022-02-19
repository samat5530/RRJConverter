using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RRJConverter.Domain
{
    public interface IJsonApiCurrenciesService
    {
        public Task<IEnumerable<DomainCurrencyModel>> GetListOfCurrenciesAsync();
    }
}
