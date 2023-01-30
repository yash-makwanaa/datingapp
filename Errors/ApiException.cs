namespace API.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string message, string detials)
        {
            StatusCode = statusCode;
            Message = message;
            Detials = detials;
        }
 
        public int StatusCode { get; set; }
        public string Message { get; set; }   
        public string Detials { get; set; }   
    }
}