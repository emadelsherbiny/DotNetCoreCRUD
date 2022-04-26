using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace INNOVATEQ.API.Helper
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                _logger.LogInformation("Performing file logging in Middleware operation");

                // Perform some Database action into Middleware 


                _logger.LogWarning("Performing Middleware Save operation");

                //Save Data


                _logger.LogInformation("Save Comepleted");

               
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
             

            var errorResponse = new ErrorResponse()
            ;
            switch (exception)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid token"))
                    {
                        errorResponse.StatusCode =  HttpStatusCode.Forbidden;
                        errorResponse.Message = ex.Message;
                        break;
                    }
                    errorResponse.StatusCode =  HttpStatusCode.BadRequest;
                    errorResponse.Message = ex.Message;
                    break;
                case KeyNotFoundException ex:
                    errorResponse.StatusCode =  HttpStatusCode.NotFound;
                    errorResponse.Message = ex.Message;
                    break;
                default:
                    errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Internal Server errors. Check Logs!";
                    break;
            }
            _logger.LogError(exception.Message);
            errorResponse.InnerException = exception.InnerException;
             
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }
    }
    //public class ErrorHandlerMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly IWebHostEnvironment _Environment;
    //    public ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment iWebHostEnvironment)
    //    {
    //        _next = next;
    //        _Environment = iWebHostEnvironment;
    //    }

    //    public async Task Invoke(HttpContext context)
    //    {
    //        try
    //        {
    //            await _next(context);
    //        }
    //        catch (Exception error)
    //        {

    //            var response = context.Response;
    //            response.ContentType = "application/json";

    //            switch (error)
    //            {

    //                case KeyNotFoundException e:
    //                    // not found error
    //                    response.StatusCode = (int)HttpStatusCode.NotFound;
    //                    break;
    //                default:
    //                    // unhandled error
    //                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //                    break;
    //            }

    //            var result = JsonSerializer.Serialize(new { message = error?.Message });
    //            await response.WriteAsync(result);

    //        }
    //    }
    //}
}
