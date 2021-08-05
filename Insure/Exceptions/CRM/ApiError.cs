using Insure.Exceptions.Helpers;
using Newtonsoft.Json;

namespace Insure.Exceptions.CRM
{
    public class ApiError
    {
        public int StatusCode { get; private set; }

        public string StatusDescription { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }

        public ApiError(int statusCode, string statusDescription)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;

            ConsoleError.Print(statusCode, statusDescription);
        }

        public ApiError(int statusCode, string statusDescription, string message)
            : this(statusCode, statusDescription)
        {
            this.Message = message;
            
            ConsoleError.Print(message);
        }
    }
}