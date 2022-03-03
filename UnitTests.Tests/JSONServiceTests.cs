using Xunit;

namespace UnitTests.Tests
{
    public class JSONServiceTests
    {
   
        [Fact]
        public void TryGetDataFromURLTest()
        {
            //Arrange
            var json = new JsonListOfValutesService();

            //Act
            bool result = json.TryGetData();

            //Assert
            Assert.True(result);

        }

        [Fact]
        public void JsonParsingCompleteTest()
        {
            //Arrange
            var json = new JsonListOfValutesService();

            //Act
            object result = json.GetListOfValutes();

            //Assert
            Assert.NotNull(result);
        }

        
    }
}