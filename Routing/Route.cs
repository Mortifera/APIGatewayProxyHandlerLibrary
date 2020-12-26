using System.Collections.Generic;
using System.Linq;

namespace APIGatewayHandlerLibrary.Routing
{
    public class Route : IRoute
    {
        private readonly HttpMethod _httpMethod;
        private readonly string _pathPattern;

        /**
         * pathPattern must be the path, with path parameters in braces {}. 
         * E.g. "/api/users/{userID}/data" where {userID} is an expected path parameter. So "/api/user/user1/data" would be a match.
         */
        public Route(string baseUri, string pathPattern, HttpMethod httpMethod) : this(baseUri + pathPattern,
            httpMethod)
        {
        }

        public Route(string pathPattern, HttpMethod httpMethod)
        {
            _pathPattern = pathPattern;
            _httpMethod = httpMethod;
        }

        public bool Matches(string path, HttpMethod httpMethod, IDictionary<string, string> pathParameters)
        {
            if (pathParameters != null)
            {
                path = pathParameters.Aggregate(path,
                    (current, keyValuePair) => current.Replace(keyValuePair.Value, "{" + keyValuePair.Key + "}"));
            }

            return _pathPattern.Equals(path) && _httpMethod == httpMethod;
        }
    }
}
