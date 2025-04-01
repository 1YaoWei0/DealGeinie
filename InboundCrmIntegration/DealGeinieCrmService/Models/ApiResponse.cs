using System.Net;

namespace DealGeinieCrmService.Models
{
    /// <summary>
    /// ApiResponse used to return the response of the API
    /// Willie Yao - 03/27/2025
    /// </summary>
    /// <typeparam name="T">
    /// T is the type of the response
    /// </typeparam>
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        /// <summary>
        /// Constructor of ApiResponse
        /// Willie Yao - 03/27/2025
        /// </summary>
        /// <param name="statusCode">Response status code</param>
        /// <param name="message">Message</param>
        /// <param name="data">Data</param>
        public ApiResponse(
            HttpStatusCode statusCode, 
            string message = null, 
            T data = default)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(statusCode);
            Data = data;
        }

        /// <summary>
        /// Get the default message based on the status code
        /// Willie Yao - 03/27/2025
        /// </summary>
        /// <param name="statusCode">Response status code</param>
        /// <returns>Response message</returns>
        private static string GetDefaultMessage(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    return "Succeed";
                case HttpStatusCode.BadRequest:
                    return "Bad request: incorrect parameters";
                case HttpStatusCode.Unauthorized:
                    return "Unauthorized";
                case HttpStatusCode.NotFound:
                    return "Resource is not found";
                case HttpStatusCode.InternalServerError:
                    return "Internal server error";
                default:
                    return "Unknown error";
            }
        }
    }
}