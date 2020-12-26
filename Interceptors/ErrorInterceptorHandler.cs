using System;
using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using APIGatewayHandlerLibrary.Responses;
using APIGatewayHandlerLibrary.Routing;

namespace APIGatewayHandlerLibrary.Interceptors
{
    public class ErrorInterceptorHandler : IRouteHandler
    {
        private readonly Dictionary<Type, HttpStatusCode> _exceptionToHttpStatusCode;
        private readonly IRouteHandler _routeHandler;

        public ErrorInterceptorHandler(IRouteHandler routeHandler) : this(routeHandler,
            new Dictionary<Type, HttpStatusCode>())
        {
        }

        public ErrorInterceptorHandler(IRouteHandler routeHandler,
            Dictionary<Type, HttpStatusCode> exceptionToHttpStatusCode)
        {
            _routeHandler = routeHandler;
            _exceptionToHttpStatusCode = exceptionToHttpStatusCode;
        }

        public APIGatewayProxyResponse Handle(APIGatewayProxyRequest request)
        {
            try
            {
                return _routeHandler.Handle(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                var statusCode = HttpStatusCode.InternalServerError;

                if (_exceptionToHttpStatusCode.ContainsKey(e.GetType()))
                    statusCode = _exceptionToHttpStatusCode[e.GetType()];

                return new APIGatewayErrorResponse(e, statusCode);
            }
        }
    }
}
