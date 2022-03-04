using Moq;
using RRJConverter.Domain;
using RRJConverter.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests
{
    public class CurrencyConverterServiceTest
    {
        [Fact]
        public void ServiceCorrectCountingTest()
        {

            //Arrange
            var jsonMock = new Mock<IJsonApiCurrenciesService>();
            jsonMock.Setup(jsonData => jsonData.GetListOfCurrenciesAsync()).Returns(GetTestRates());
            IJsonApiCurrenciesService currenciesService = jsonMock.Object;
            var converter = new CurrencyConverterService(currenciesService);

            //Act
            decimal target = converter.ConvertAsync("A", 10, "B").Result;

            //Assert
            Assert.Equal(160, target);

        }

        /// <summary>
        ///  Testing samples 
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<DomainCurrenciesPairModel>> GetTestRates()
        {
            
            var list = new List<DomainCurrenciesPairModel>
            {
                new DomainCurrenciesPairModel { FirstCurrency="A",SecondCurrency="B",Rate=16},
                new DomainCurrenciesPairModel { FirstCurrency="X",SecondCurrency="Y",Rate=4},
                new DomainCurrenciesPairModel { FirstCurrency="Q",SecondCurrency="W",Rate=8},
                new DomainCurrenciesPairModel { FirstCurrency="USD",SecondCurrency="RUB",Rate=0.1M},
            };
            return list;
        }
    }
}
