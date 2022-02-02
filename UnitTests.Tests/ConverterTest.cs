// тест не совсем автоматизированый 
// руками вводим значения для конвертации
// в сообщении об ошибке смотрим значение actual
// при условии что валюты конкертируются корректно
// оно будет показывать очень 
// близкое значение с конвертером из гугла




using RRJConverter.Services;
using Xunit;

namespace UnitTests.Tests
{
    public class ConverterTest
    {
        [Fact]
        public void CorrectCountigTest()
        {
            //Arrange
            var json = new JsonListOfValutesService();
            var result = json.GetListOfValutes();
            var converter = new ConverterService();


            //Act
            decimal target = converter.Convert(result, "RUB", (decimal)77.8174, "USD"); //актуально на 30.01.2022



            //Assert
            Assert.Equal((decimal)1, target);
        }
    }
}
