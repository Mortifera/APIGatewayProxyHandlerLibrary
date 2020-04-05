using System.Collections.Generic;
using System.Linq;

namespace APIGatewayHandlerLibrary.Routing
{
    public class MultiRoute : IRoute
    {
        private ICollection<IRoute> routes;

        public MultiRoute(params IRoute[] routes) {
            this.routes = routes.ToList();
        }

        public bool Matches(string path, HttpMethod httpMethod, IDictionary<string, string> pathParameters)
        {
            return routes.Any(route => route.Matches(path, httpMethod, pathParameters));
        }
    }
}