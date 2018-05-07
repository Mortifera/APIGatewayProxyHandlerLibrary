using System.Collections.Generic;

namespace APIGatewayHandlerLibrary.Routing
{
    public interface IRoute
    {
        bool Matches(string path, HttpMethod httpMethod, IDictionary<string, string> pathParameters);
    }
}