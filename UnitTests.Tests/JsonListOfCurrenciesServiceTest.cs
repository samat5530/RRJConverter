using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using RRJConverter.Integrations.Services;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace UnitTests.Tests
{
    public class JsonListOfCurrenciesServiceTest
    {

        [Fact]
        public void TryGetDataFromURLTest()
        {
            //Arrange

            var mockLogger = new Mock<ILogger<JsonListOfCurrenciesService>>();
            var logger = mockLogger.Object;

            var mockHandler = new Mock<HttpMessageHandler>();
            var newResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(GetTestSampleData())
            };
            mockHandler.Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(newResponse);
            var httpClient = new HttpClient(mockHandler.Object);

            var json = new JsonListOfCurrenciesService(logger, httpClient);

            //Act
            var result = json.GetListOfCurrenciesAsync().Result;
            var pairList = result.ToList();

            //Assert
            // ?? ???? ????? ?????? ????????? ???? (3+1)^2 ???. +1 - ??? ?????.
            Assert.True(pairList.Count == Math.Pow(3 + 1, 2));

        }

        private string GetTestSampleData()
        {

            // JSON-?????? ? 3 ????????? ???????? - UNIT, TEST, INIT

            string myString = "{\"Date\":\"2022-03-04T11:30:00+03:00\",\"PreviousDate\":\"2022-03-03T11:30:00+03:00\",\"PreviousURL\":\"\",\"Timestamp\":\"2022-03-03T23:00:00+03:00\",\"Valute\":{\"UNIT\":{\"ID\":\"1\",\"NumCode\":\"000\",\"CharCode\":\"UNIT\",\"Nominal\":1,\"Name\":\"????\",\"Value\":81.6828,\"Previous\":74.8863},\"TEST\":{\"ID\":\"2\",\"NumCode\":\"00\",\"CharCode\":\"TEST\",\"Nominal\":100,\"Name\":\"????\",\"Value\":65.7778,\"Previous\":60.7703},\"INIT\":{\"ID\":\"3\",\"NumCode\":\"000\",\"CharCode\":\"INIT\",\"Nominal\":12,\"Name\":\"?????????????\",\"Value\":149.7424,\"Previous\":137.2898}}}";
            return myString;
        }    

    }
}
