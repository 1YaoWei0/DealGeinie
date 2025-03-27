using DealGeinieCrmService.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System;
using System.Web.Http.Filters;

namespace DealGeinieCrmService.Filters
{
    /// <summary>
    /// Global exception filter.
    /// Willie Yao - 03/27/2025
    /// </summary>
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Override the OnException method to handle the exception
        /// Willie Yao - 03/27/2025
        /// </summary>
        /// <param name="context">Http action executed context</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            // 1. Log the exception
            LogException(context.Exception);

            // 2. Construct the response object
            var statusCode = GetStatusCode(context.Exception);
            var response = new ApiResponse<object>(statusCode, "The server has strayed, please try again later", null);

            // 3. Set the http response message with the response object and status code
            context.Response = context.Request.CreateResponse(statusCode, response);
        }

        /// <summary>
        /// Get the status code based on the exception
        /// Willie Yao - 03/27/2025
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>Http status code</returns>
        private HttpStatusCode GetStatusCode(Exception ex)
        {
            if (ex is ArgumentException)
                return HttpStatusCode.BadRequest;
            if (ex is UnauthorizedAccessException)
                return HttpStatusCode.Unauthorized;
            if (ex is System.Web.Http.HttpResponseException httpEx)
                return (HttpStatusCode)httpEx.Response.StatusCode;
            return HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// Log the exception
        /// Willie Yao - 03/27/2025
        /// </summary>
        /// <param name="ex">Exception</param>
        private void LogException(Exception ex)
        {
            // TODO: 实际项目中可替换为 ILogger、Serilog 等
            Trace.TraceError($"[{DateTime.UtcNow:u}] Error message: {ex.Message}");
            Trace.TraceError($"Stack trace: {ex.StackTrace}");
            if (ex.InnerException != null)
            {
                Trace.TraceError($"Inner exception: {ex.InnerException.Message}");
            }
        }
    }
}