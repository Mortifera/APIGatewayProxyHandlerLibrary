using System;
using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using APIGatewayHandlerLibrary.Responses;
using APIGatewayHandlerLibrary.Routing;

namespace APIGatewayHandlerLibrary
{
    public class APIGatewayHandler : IRouteHandler
    {
        private readonly Dictionary<IRoute, IRouteHandler> _routeHandlers;

        public APIGatewayHandler()
        {
            _routeHandlers = new Dictionary<IRoute, IRouteHandler>();
        }

        public APIGatewayHandler(Dictionary<IRoute, IRouteHandler> routeHandlers)
        {
            _routeHandlers = routeHandlers;
        }

        public APIGatewayProxyResponse Handle(APIGatewayProxyRequest request)
        {
            if (!Enum.TryParse(request.HttpMethod, out HttpMethod requestHttpMethod))
                return new APIGatewayErrorResponse($"Unrecognised http method {request.HttpMethod}",
                    HttpStatusCode.BadRequest);

            foreach (var route in _routeHandlers.Keys)
                if (route.Matches(request.Path, requestHttpMethod, request.PathParameters))
                    return _routeHandlers[route].Handle(request);

            return new APIGatewayErrorResponse(
                $"No route with path '{request.Path}' and method '{request.HttpMethod}' found",
                HttpStatusCode.NotFound);
        }

        public void AddRouteHandler(IRoute route, IRouteHandler routeHandler)
        {
            _routeHandlers.Add(route, routeHandler);
        }
    }
}