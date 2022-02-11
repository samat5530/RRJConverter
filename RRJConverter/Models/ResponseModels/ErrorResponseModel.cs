using System.Text.Json;

namespace RRJConverter.Models
{
    public class ErrorResponseModel
    {
        public string Error { get; set; }

        public string GetErrorResponse(string message)
        {
            this.Error = message;

            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
