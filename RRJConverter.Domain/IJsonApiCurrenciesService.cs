using RRJConverter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RRJConverter.Domain
{
    public interface IJsonApiCurrenciesService
    {
        public Task<IEnumerable<DomainCurrenciesPairModel>> GetListOfCurrenciesAsync();
        public bool IsCurrencyExistInList(IEnumerable<DomainCurrenciesPairModel> collection, string currency);
    }
}
