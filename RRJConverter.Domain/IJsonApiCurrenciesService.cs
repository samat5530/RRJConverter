using RRJConverter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RRJConverter.Domain
{
    /// <summary>
    /// Сервис для получения валют из стороннего API
    /// </summary>
    public interface IJsonApiCurrenciesService
    {
        /// <summary>
        /// Предоставляет коллекцию пар валют
        /// </summary>
        public Task<IEnumerable<DomainCurrenciesPairModel>> GetListOfCurrenciesAsync();
    }
}
