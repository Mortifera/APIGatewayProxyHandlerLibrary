using System;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;

namespace APIGatewayHandlerLibrary.Responses
{
    public class APIGatewayErrorResponse : APIGatewayProxyResponse
    {
        public APIGatewayErrorResponse(Exception error,
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
            : this(error.Message, $"{error.Message} {error.StackTrace}", httpStatusCode)
        {
        }

        public APIGatewayErrorResponse(string errorMessage, string detailedMessage,
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        {
            Body = errorMessage;
            StatusCode = (int)httpStatusCode;
#if _DEBUG
            Headers = new Dictionary<string, string>
            {
                {"Debug", $"{errorMessage} {detailedMessage}"}
            };
#endif
        }

        public APIGatewayErrorResponse(string errorMessage,
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        {
            Body = errorMessage;
            StatusCode = (int)httpStatusCode;
        }
    }
}
