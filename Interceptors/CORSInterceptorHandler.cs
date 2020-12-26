using System.Collections.Generic;
using System.Text;
using Amazon.Lambda.APIGatewayEvents;
using APIGatewayHandlerLibrary.Routing;

namespace APIGatewayHandlerLibrary.Interceptors
{
    public class CORSInterceptorHandler : IRouteHandler
    {
        private readonly string[] _accessOrigins;

        private readonly IRouteHandler _routeHandler;

        public CORSInterceptorHandler(IRouteHandler routeHandler, params string[] accessOrigins)
        {
            _routeHandler = routeHandler;
            _accessOrigins = accessOrigins;
        }

        public APIGatewayProxyResponse Handle(APIGatewayProxyRequest request)
        {
            var response = _routeHandler.Handle(request);
            if (response.Headers == null)
            {
                response.Headers = new Dictionary<string, string>();
            }

            if (_accessOrigins == null || _accessOrigins.Length == 0)
            {
                return response;
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append(_accessOrigins[0]);
            for (var i = 1; i < _accessOrigins.Length; ++i)
            {
                stringBuilder.Append($",{_accessOrigins[i]}");
            }

            response.Headers.Add("Access-Control-Allow-Origin", stringBuilder.ToString());
            return response;
        }
    }
}
