namespace Ecom.Api.Helper
{
    public class ResponseAPI
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ResponseAPI(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetStatusMessage(statusCode);
        }
        private string GetStatusMessage(int statusCode)
        {
            return statusCode switch
            {
                200 => "OK",
                201 => "Created",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Unknown Status"
            };
        }
    }
}