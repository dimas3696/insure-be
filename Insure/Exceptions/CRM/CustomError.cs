namespace Insure.Exceptions.CRM
{
    public class CustomError : ApiError
    {
        public CustomError(string statusDescription) : base(600, statusDescription)
        {
        }

        public CustomError(string statusDescription, string message) : base(600, statusDescription, message)
        {
        }
    }
}