using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Ttf.BusinessLayer;

namespace Ttf.WebApi.Filters
{
    /// <summary>
    /// An exception filter for globally controlling the error messages that are allowed to be
    /// returned to the client.
    /// </summary>
    public class WebApiExceptionFilter : ExceptionFilterAttribute
    {
        private static readonly IDictionary<Type, ErrorResponse> errorMapping = new Dictionary<Type, ErrorResponse>
        {
            { typeof(UnauthorizedAccessException), new ErrorResponse("Access to the API is not authorized.", HttpStatusCode.Unauthorized) },
            { typeof(DivideByZeroException), new ErrorResponse("Internal Server Error.", HttpStatusCode.InternalServerError) },
            { typeof(InvalidOptionException), new ErrorResponse("Option not supported.", HttpStatusCode.BadRequest) },
            { typeof(ArgumentException), new ErrorResponse("{0}", HttpStatusCode.BadRequest) },
            { typeof(ArgumentNullException), new ErrorResponse("{0}", HttpStatusCode.BadRequest) },
        };

        private static readonly ErrorResponse defaultResponse = new ErrorResponse("Not found.", HttpStatusCode.NotFound);


        public override bool AllowMultiple
        {
            get { return false; }
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            ErrorResponse errorResponse;
            if (!errorMapping.TryGetValue(actionExecutedContext.Exception.GetType(), out errorResponse))
            {
                // If there is no mapping found, use the default error response.
                errorResponse = defaultResponse;
            }

            actionExecutedContext.Response = new HttpResponseMessage
            {
                Content = new StringContent(
                    string.Format(errorResponse.Message, actionExecutedContext.Exception.Message), 
                    System.Text.Encoding.UTF8, 
                    "text/plain"),
                StatusCode = errorResponse.StatusCode
            };
        }

        #region Nested type: ErrorResponse

        private class ErrorResponse
        {
            public ErrorResponse(string message, HttpStatusCode statusCode)
            {
                if (string.IsNullOrEmpty(message))
                    throw new ArgumentNullException("message");

                this.Message = message;
                this.StatusCode = statusCode;
            }

            public string Message { get; private set; }
            public HttpStatusCode StatusCode { get; private set; }
        }

        #endregion Nested type: ErrorResponse
    }
}